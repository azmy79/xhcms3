using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// 文章涉及一般处理程序
    /// </summary>
    public class News : DTcms.Web.UI.BasePage_Ajax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //操作类型
            switch (context.Request.QueryString["option"])
            {
                //分页方式获取新闻数据
                case "GetNewsData":
                    //输出
                    context.Response.Write(GetNewsData(context));
                    break;
            };
        }

        /// <summary>
        /// 分页方式获取新闻数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public string GetNewsData(HttpContext context)
        {
            var pageSize = int.Parse(context.Request.QueryString["PageSize"]);//页大小
            var pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);//页索引
            var cid = int.Parse(context.Request.QueryString["cid"]);//板块ID
            var dataType = context.Request.QueryString["dataType"];//数据类型
            //js序列化实例
            var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //查询条件
            string strWhere = string.Empty;
            if (dataType == "search")//搜索页
            {
                string kw = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["keyWord"]);
                //过滤下载页内容
                strWhere = "channel_id=1 and status=0 and (a.Title like'%" + kw + "%' or a.Content like'%" + kw + "%') and a.category_id not in (select Id from dt_article_category where link_url like '%TableDown.aspx%')";
            }
            else//非搜索页
                strWhere = "status=0 and a.category_id in (select Id from dt_article_category where class_list like '%," + cid + ",%')";
            var bll = new DTcms.BLL.article();
            //总数
            var totalCount = 0;
            //结果集
            var ds = bll.GetList(pageSize, pageIndex + 1, strWhere, "is_top Desc,sort_id desc,add_time desc", out totalCount);
            //最终返回数据集
            var retList = new List<object>();
            //拼接结果集
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    retList.Add(new
                    {
                        ID = item["id"].ToString(),
                        CID = item["category_id"].ToString(),
                        AddTime = Convert.ToDateTime(item["add_time"]).ToString("yyyy-MM-dd"),
                        Title = item["title"].ToString(),
                        ZhaoYao = item["zhaiyao"].ToString()
                    });
                }
            }
            //输出结果集
            return jsSerializer.Serialize(new
            {
                totalCount = totalCount,
                list = retList
            });
        }
    }
}