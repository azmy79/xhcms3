using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Configuration;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        public static Model.siteconfig config = new BLL.siteconfig().loadConfig();
        public static Model.userconfig uconfig = new BLL.userconfig().loadConfig();
        /// <summary>
        /// 父类的构造函数
        /// </summary>
        public BasePage()
        {
            //是否关闭网站
            if (config.webstatus == 0)
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode(config.webclosereason));
                return;
            }
            ShowPage();
        }

        /// <summary>
        /// 跳转首页
        /// </summary>
        public void SkipIndex()
        {
            Response.Redirect("/index.aspx");
        }

        /// <summary>
        /// 跳转登录页
        /// </summary>
        public void SkipLogin()
        {
            Response.Redirect("/UserLogin.aspx");
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetThumbImg(string path)
        {
            var pos1 = path.LastIndexOf('/');
            var pos2 = path.LastIndexOf('\\');
            var pos = Math.Max(pos1, pos2);
            if (pos < 0)
                return path;
            else
            {
                var fileName = path.Substring(pos + 1);
                return path.Replace(fileName, "thumb_" + fileName);
            }
        }

        /// <summary>
        /// 页面处理虚方法
        /// </summary>
        protected virtual void ShowPage()
        {
            //虚方法代码
        }

        #region 页面通用方法==========================================
        /// <summary>
        /// 返回URL重写统一链接地址
        /// </summary>
        public string linkurl(string _key, params object[] _params)
        {
            Hashtable ht = new BLL.url_rewrite().GetList(); //获得URL配置列表
            Model.url_rewrite model = ht[_key] as Model.url_rewrite; //查找指定的URL配置节点

            //如果不存在该节点则返回空字符串
            if (model == null)
            {
                return string.Empty;
            }

            string requestDomain = HttpContext.Current.Request.Url.Authority.ToLower(); //获得来源域名含端口号
            string requestFirstPath = GetFirstPath();//获得二级目录(不含站点安装目录)
            string linkStartString = string.Empty; //链接前缀

            //检查是否与绑定的域名或者与默认频道分类的目录匹配
            if (SiteDomains.GetSiteDomains().CategoryDirs.ContainsValue(requestDomain))
            {
                linkStartString = "/";
            }

            else if (requestFirstPath == string.Empty || requestFirstPath == SiteDomains.GetSiteDomains().DefaultPath)
            {
                linkStartString = config.webpath;
            }
            else
            {
                linkStartString = config.webpath + requestFirstPath + "/";
            }
            //如果URL字典表达式不需要重写则直接返回
            if (model.url_rewrite_items.Count == 0)
            {
                //检查网站重写状态
                if (config.staticstatus > 0)
                {
                    if (_params.Length > 0)
                    {
                        return linkStartString + GetUrlExtension(model.page, config.staticextension) + string.Format("{0}", _params);
                    }
                    else
                    {
                        return linkStartString + GetUrlExtension(model.page, config.staticextension);
                    }
                }
                else
                {
                    if (_params.Length > 0)
                    {
                        return linkStartString + model.page + string.Format("{0}", _params);
                    }
                    else
                    {
                        return linkStartString + model.page;
                    }
                }
            }
            //否则检查该URL配置节点下的子节点
            foreach (Model.url_rewrite_item item in model.url_rewrite_items)
            {
                //如果参数个数匹配
                if (IsUrlMatch(item, _params))
                {
                    //检查网站重写状态
                    if (config.staticstatus > 0)
                    {
                        return linkStartString + string.Format(GetUrlExtension(item.path, config.staticextension), _params);
                    }
                    else
                    {
                        string queryString = Regex.Replace(string.Format(item.path, _params), item.pattern, item.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                        if (queryString.Length > 0)
                        {
                            queryString = "?" + queryString;
                        }
                        return linkStartString + model.page + queryString;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回分页字符串
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="totalcount">记录总数</param>
        /// <param name="_key">URL映射Name名称</param>
        /// <param name="_params">传输参数</param>
        protected string get_page_link(int pagesize, int pageindex, int totalcount, string _key, params object[] _params)
        {
            return Utils.OutPageList(pagesize, pageindex, totalcount, linkurl(_key, _params), 8);
        }

        /// <summary>
        /// 返回分页字符串
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="totalcount">记录总数</param>
        /// <param name="linkurl">链接地址</param>
        protected string get_page_link(int pagesize, int pageindex, int totalcount, string linkurl)
        {
            return Utils.OutPageList(pagesize, pageindex, totalcount, linkurl, 8);
        }
        #endregion

        #region 会员用户方法==========================================
        /// <summary>
        /// 判断用户是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsUserLogin()
        {
            //如果Session为Null
            if (HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string username = Utils.GetCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms");
                string password = Utils.GetCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms");
                if (username != "" && password != "")
                {
                    BLL.users bll = new BLL.users();
                    Model.users model = bll.GetModel(username, password, 0, 0, false);
                    if (model != null)
                    {
                        HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得用户信息
        /// </summary>
        public Model.users GetUserInfo()
        {
            if (IsUserLogin())
            {
                Model.users model = HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] as Model.users;
                if (model != null)
                {
                    //为了能查询到最新的用户信息，必须查询最新的用户资料
                    model = new BLL.users().GetModel(model.id);
                    return model;
                }
            }
            return null;
        }
        #endregion

        #region 辅助方法(私有)========================================
        /// <summary>
        /// 获取访问的频道分类目录(不含安装目录)
        /// </summary>
        private string GetFirstPath()
        {
            //string requestPath = HttpContext.Current.Request.CurrentExecutionFilePath.ToLower();//获得当前页面虚拟路径
            string requestPath = HttpContext.Current.Request.RawUrl.ToLower();
            int indexNum = config.webpath.Length; //安装目录长度
            //如果包含安装目录和aspx目录也要过滤掉
            if (requestPath.StartsWith(config.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/"))
            {
                indexNum = (config.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/").Length;
            }
            string requestFirstPath = requestPath.Substring(indexNum);
            if (requestFirstPath.IndexOf("/") > 0)
            {
                requestFirstPath = requestFirstPath.Substring(0, requestFirstPath.IndexOf("/"));
            }
            if (requestFirstPath != string.Empty && SiteDomains.GetSiteDomains().CategoryDirs.ContainsKey(requestFirstPath))
            {
                return requestFirstPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 参数个数是否匹配
        /// </summary>
        private bool IsUrlMatch(Model.url_rewrite_item item, params object[] _params)
        {
            int strLength = 0;
            if (!string.IsNullOrEmpty(item.querystring))
            {
                strLength = item.querystring.Split('&').Length;
            }
            if (strLength == _params.Length)
            {
                //注意__id__代表分页页码，所以须替换成数字才成进行匹配
                if (Regex.IsMatch(string.Format(item.path, _params).Replace("__id__", "1"), item.pattern, RegexOptions.None | RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 替换扩展名
        /// </summary>
        private string GetUrlExtension(string urlPage, string staticExtension)
        {
            return Utils.GetUrlExtension(urlPage, staticExtension);
        }

        #endregion

        /// <summary>
        /// 获取首页友情链接
        /// </summary>
        /// <returns></returns>
        public static List<DTcms.Model.link> GetIndexLink()
        {
            var linkDs = new DTcms.BLL.link().GetList("1=1 and is_image=1 and is_lock=0 and is_red=1 Order by sort_id,add_time desc");
            return DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.link>(linkDs.Tables[0]);
        }

        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="cid">栏目ID</param>
        /// <param name="top">前几条(0代表全部)</param>
        /// <param name="strWhere">累加查询条件</param>
        /// <returns></returns>
        public static List<DTcms.Model.article> GetNews(int cid, int top = 0, string strWhere = null)
        {
            string _where = " status=0 and category_id in (select Id from dt_article_category where class_list like '%," + cid + ",%')";
            if (!string.IsNullOrEmpty(strWhere))
                _where += " and " + strWhere;
            var bll = new BLL.article();
            if (top == 0) top = bll.GetCount(_where);
            if (top == 0) return new List<Model.article>();
            string _order = "is_top Desc,sort_id desc,add_time desc";
            var ds = bll.GetList(top, _where, _order);
            return DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.article>(ds.Tables[0]);
        }

        /// <summary>
        /// 获取首页文章信息
        /// </summary>
        /// <param name="cid">栏目ID</param>
        /// <param name="top">前几条(0代表全部)</param>
        /// <returns></returns>
        public static List<DTcms.Model.article> GetIndexNews(int cid, int top = 0)
        {
            return GetNews(cid, top, "is_red=1");
        }

        /// <summary>
        /// 获取导航JSON
        /// </summary>
        /// <param name="channel_id">板块ID</param>
        /// <param name="topNum">前几条</param>
        /// <returns></returns>
        public static string GetTopFullNav(int channel_id, int topNum)
        {
            //获取所有根节点
            var baseDt = new DTcms.BLL.article_category().GetChildList(0, channel_id);
            int fullLevel = 1;//总深度
            //所有菜单集合
            var navList = new List<Dictionary<string, object>>();
            foreach (DataRow item in baseDt.Rows)
            {
                if (baseDt.Rows.IndexOf(item) > topNum - 1)
                    break;
                //新项
                var subDicObj = new Dictionary<string, object>
                {
                    {"ID" , Convert.ToInt32(item["id"])},//编号
                    {"Title" ,item["title"].ToString()},//标题 
                    {"EnTitle" , item["call_index"].ToString()},//英文标题
                    {"ParentId" ,Convert.ToInt32(item["parent_id"])},//父节点ID
                    {"ClassLayer" , Convert.ToInt32(item["class_layer"])},//节点深度
                    {"LinkUrl" , item["link_url"].ToString()}//链接
                };
                //添加子节点
                AddSubNav(subDicObj, channel_id, Convert.ToInt32(item["Id"]), ref fullLevel);
                //装载
                navList.Add(subDicObj);
            }
            //输出
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new
            {
                FullLevel = fullLevel,//总深度
                NavList = navList//所有节点
            });
        }

        /// <summary>
        /// 获取菜单JSON
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public static string GetLeftFullNav(int id)
        {
            //获取根节点
            var baseModel = GetBaseNavID(id);
            var baseDicObj = new Dictionary<string, object> {
                {"ID" , baseModel.id},//编号
                {"Title" ,baseModel.title},//标题
                {"EnTitle" , baseModel.call_index},//英文标题
                {"ParentId" , baseModel.parent_id},//父节点ID
                {"ClassLayer" , baseModel.class_layer},//节点深度
                {"LinkUrl" , baseModel.link_url}
            };
            int fullLevel = 1;//总深度
            //添加子节点
            AddSubNav(baseDicObj, baseModel.channel_id, baseModel.id, ref fullLevel);
            //输出
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new
            {
                FullLevel = fullLevel,//总深度
                Nav = baseDicObj//所有节点
            });
        }

        /// <summary>
        /// 递归添加子节点
        /// </summary>
        /// <param name="dicObj">当前节点</param>
        /// <param name="channel_id">板块ID</param>
        /// <param name="pid">父节点ID</param>
        /// <param name="fullLevel">总深度</param>
        public static void AddSubNav(Dictionary<string, object> dicObj, int channel_id, int pid, ref int fullLevel)
        {
            //获取子节点
            var dt = new BLL.article_category().GetChildList(pid, channel_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                var subList = new List<object>();//子节点集合
                foreach (DataRow item in dt.Rows)
                {
                    var ClassLayer = Convert.ToInt32(item["class_layer"]);//深度
                    if (fullLevel < ClassLayer)
                        fullLevel = ClassLayer;//总深度赋值
                    //新项
                    var subDicObj = new Dictionary<string, object>
                    {
                        {"ID" , Convert.ToInt32(item["id"])},//编号
                        {"Title" ,item["title"].ToString()},//标题
                        {"EnTitle" , item["call_index"].ToString()},//英文标题
                        {"ParentId" ,Convert.ToInt32(item["parent_id"])},//父节点ID
                        {"ClassLayer" , ClassLayer},//节点深度
                        {"LinkUrl" , item["link_url"].ToString()}
                    };
                    //递归添加子节点
                    AddSubNav(subDicObj, channel_id, Convert.ToInt32(item["Id"]), ref fullLevel);
                    //装载项
                    subList.Add(subDicObj);
                }
                dicObj.Add("SubNav", subList);//添加
            }
        }

        /// <summary>
        /// 递归获取根节点栏目
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public static DTcms.Model.article_category GetBaseNavID(int id)
        {
            var model = new DTcms.BLL.article_category().GetModel(id);
            if (model.parent_id == 0)
                return model;
            return GetBaseNavID(model.parent_id);
        }

        /// <summary>
        /// 递归添加面包屑导航
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <param name="dicObj">所有子节点</param>
        public static void AddSiteMap(int id, ref Dictionary<string, object> dicObj)
        {
            var model = new DTcms.BLL.article_category().GetModel(id);
            var dic = new Dictionary<string, object>
            {
                {"Title",model.title},
                {"LinkUrl",model.link_url}
            };
            //装载
            dic.Add("SubNav", dicObj);
            dicObj = dic;//重新赋值
            //递归装载
            if (model.parent_id != 0)
                AddSiteMap(model.parent_id, ref dic);
        }

        /// <summary>
        /// 获取面包屑导航
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <returns></returns>
        public static string GetFullSiteMap(int id)
        {
            var model = new DTcms.BLL.article_category().GetModel(id);
            var dic = new Dictionary<string, object>
            {
                {"Title",model.title},
                {"LinkUrl",model.link_url}
            };
            if (model.parent_id != 0)
                //装载
                AddSiteMap(model.parent_id, ref dic);
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dic);
        }

        /// <summary>
        /// 获取图片广告内容
        /// </summary>
        /// <param name="id">广告位ID</param>
        /// <returns></returns>
        public static List<DTcms.Model.ad_item> GetAdvertising(int id)
        {
            var aModel = new BLL.ad().GetModel(id);
            if (aModel != null && aModel.adtype == 2)
            {
                //输出该广告位下的广告条,不显示未开始、过期、暂停广告
                var ds = new BLL.ad_item().GetList("datediff(d,start_time,getdate()) >= 0 and datediff(d,end_time,getdate())<=0 and is_lock=0 and ad_id=" + id + " order by sort_id,add_time desc");
                if (ds != null && ds.Tables.Count > 0)
                {
                    return DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.ad_item>(ds.Tables[0]);
                }
            }
            return new List<DTcms.Model.ad_item>();
        }

        /// <summary>
        /// 获取文章详情链接
        /// </summary>
        /// <param name="article">文章对象</param>
        /// <returns></returns>
        public static string GetNews_ViewUrl(DTcms.Model.article article)
        {
            return "News_View.aspx?cid=" + article.category_id + "&id=" + article.id;
        }

        /// <summary>
        /// 获取栏目对象
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static DTcms.Model.article_category GetCategory(int cid)
        {
            return new DTcms.BLL.article_category().GetModel(cid);
        }

        /// <summary>
        /// 阅读新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns></returns>
        public static DTcms.Model.article ReadArticle(int id)
        {
            var bll = new DTcms.BLL.article();
            var model = bll.GetModel(id);
            if (model != null)
            {
                bll.UpdateField(id, "click=" + (model.click + 1));
                model.click++;
                return model;
            }
            return null;
        }

        /// <summary>
        /// 获取短字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetShortString(string str, int len)
        {
            return str.Length > len ? str.Substring(0, len - 3) + "..." : str;
        }
    }
}
