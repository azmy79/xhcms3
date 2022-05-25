using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// 在线咨询涉及一般处理程序
    /// </summary>
    public class FeedBack : DTcms.Web.UI.BasePage_Ajax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //操作类型
            switch (DTcms.Common.DTRequest.GetString("option"))
            {
                //留言
                case "FeedBack":
                    //输出
                    var txtCode = DTcms.Common.DTRequest.GetString("txtCode");
                    //校检验证码
                    string result = verify_code(context, txtCode);
                    if (result != "success")
                    {
                        context.Response.Write(result);
                        return;
                    }
                    DoFeedBack(context);
                    break;
                case "MWebFeedBack":
                    DoFeedBack(context);
                    break;
                case "BidComment":
                    //输出
                    txtCode = DTcms.Common.DTRequest.GetString("txtCode");
                    //校检验证码
                    result = verify_code(context, txtCode);
                    if (result != "success")
                    {
                        context.Response.Write(result);
                        return;
                    }
                    DoBidComment(context);
                    break;
                case "GetFeedBackData":
                    var pageSize = int.Parse(context.Request.QueryString["PageSize"]);//页大小
                    var pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);//页索引
                    //类型
                    var msgType = DTcms.Common.DTRequest.GetInt("msgType", 0);
                    //js序列化实例
                    var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //查询条件
                    string strWhere = "is_lock=0 and msgType=" + msgType;
                    //会员用户查看
                    if (DTcms.Common.DTRequest.GetString("FromUser").ToLower() == "true")
                    {
                        var user = GetUserInfo();
                        if (user != null)
                            strWhere += " and (UserID=" + user.id + " or user_tel='" + user.mobile + "')";
                    }
                    var bll = new DTcms.BLL.feedback();
                    //总数
                    var totalCount = 0;
                    //结果集
                    var ds = bll.GetList(pageSize, pageIndex + 1, strWhere, "add_time desc,reply_time desc", out totalCount);
                    //最终返回数据集
                    var retList = new List<object>();
                    //拼接结果集
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow item in ds.Tables[0].Rows)
                        {
                            retList.Add(new
                            {
                                ID = item["id"].ToString(),
                                UserName = item["user_name"].ToString(),
                                AddTime = Convert.ToDateTime(item["add_time"]).ToString("yyyy-MM-dd"),
                                ReTime = Convert.ToDateTime(item["reply_time"]).ToString("yyyy-MM-dd"),
                                Content = item["content"].ToString(),
                                ReContent = item["reply_content"].ToString(),
                            });
                        }
                    }
                    //输出结果集
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        totalCount = totalCount,
                        list = retList
                    }));
                    break;
            };
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="context"></param>
        void DoFeedBack(HttpContext context)
        {
            var txtName = DTcms.Common.DTRequest.GetString("txtName");
            var txtTel = DTcms.Common.DTRequest.GetString("txtTel");
            var txtContent = DTcms.Common.DTRequest.GetString("txtContent");
            var ret = new DTcms.BLL.feedback().Add(new DTcms.Model.feedback
            {
                user_name = txtName,
                UserID = (GetUserInfo() ?? new DTcms.Model.users()).id,
                content = txtContent,
                user_tel = txtTel,
                is_lock = 1,
                add_time = DateTime.Now,
                MsgType = 0,
                reply_time = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MinValue.ToString())
            }) > 0;
            context.Response.Write(ReturnMsg("留言失败", ret));
        }


        /// <summary>
        /// 申办评论
        /// </summary>
        /// <param name="context"></param>
        void DoBidComment(HttpContext context)
        {
            var txtContent = DTcms.Common.DTRequest.GetString("txtContent");
            var userInfo = GetUserInfo();
            var ret = new DTcms.BLL.feedback().Add(new DTcms.Model.feedback
            {
                user_name = userInfo.user_name,
                UserID = userInfo.id,
                content = txtContent,
                user_tel = userInfo.mobile,
                is_lock = 1,
                add_time = DateTime.Now,
                MsgType = 1,
                reply_time = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MinValue.ToString())
            }) > 0;
            context.Response.Write(ReturnMsg("留言失败", ret));
        }
    }
}