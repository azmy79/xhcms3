using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// UserAppointment 的摘要说明
    /// </summary>
    public class UserAppointment : DTcms.Web.UI.BasePage_Ajax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //检查用户是否登录
            var userInfo = GetUserInfo();
            if (userInfo == null)
            {
                context.Response.Write(ReturnMsg("对不起，用户尚未登录或已超时！", false));
                return;
            }
            //操作类型
            switch (DTcms.Common.DTRequest.GetString("option"))
            {
                case "GetNotary":
                    var monthType = DateTime.Now.Month % 2;
                    var time = DateTime.Parse(DTcms.Common.DTRequest.GetString("Time"));
                    if (time.Month != DateTime.Now.Month) monthType = 1 - monthType;
                    var list = DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.manager>(new BLL.manager().GetList(0, "role_id=3 and is_lock=0 and id in(select ManagerID from Scheduling where Day=" + time.Day + " and MonthType=" + monthType + ")", "add_time Desc").Tables[0]);
                    var retList = new List<object>();
                    list.ForEach(p =>
                    {
                        retList.Add(new
                        {
                            ID = p.id,
                            Number = "GZY" + p.id,
                            HeadImg = p.HeadImg,
                            Name = p.real_name,
                            AptCount = new DTcms.BLL.Appointment().GetModelList("DateDiff(d,[Date],'" + time.ToShortDateString() + "')=0 and ManagerID=" + p.id).Count
                        });

                    });
                    context.Response.Write(jsSerializer.Serialize(retList));
                    break;
                case "Appointment":
                    //输出
                    var txtCode = DTcms.Common.DTRequest.GetString("txtCode");
                    //校检验证码
                    string result = verify_code(context, txtCode);
                    if (result != "success")
                    {
                        context.Response.Write(result);
                        return;
                    }
                    var txtName = DTcms.Common.DTRequest.GetString("txtName");
                    var txtContact = DTcms.Common.DTRequest.GetString("txtContact");
                    var txtContent = DTcms.Common.DTRequest.GetString("txtContent");
                    time = DateTime.Parse(DTcms.Common.DTRequest.GetString("Time"));
                    var id = DTcms.Common.DTRequest.GetInt("ID", 0);
                    monthType = DateTime.Now.Month % 2;
                    if (time.Month != DateTime.Now.Month) monthType = 1 - monthType;
                    if (new BLL.Scheduling().GetModelList("Day=" + time.Day + " and ManagerID=" + id + " and MonthType=" + monthType).Count == 0)
                    {
                        context.Response.Write(jsSerializer.Serialize(new
                        {
                            status = false,
                            msg = "数据异常，请重试"
                        }));
                        return;
                    }
                    //当日预约
                    var appointmentList = new DTcms.BLL.Appointment().GetModelList("DateDiff(d,[Date],'" + time.ToShortDateString() + "')=0");
                    //预约号生成规则：GZY4+年月日+两位顺序号，
                    //其中年月的位数不满两位的用零补齐，
                    //顺序号就直接按照实际的数字显示，
                    //比如：GZY4|15|08|01|2。
                    //其中GYZ4来源于后台添加的每一个公证员的系统代码。
                    //2是每一天中所有的预约的顺序号，
                    //每天从1开始排列，顺序显示。
                    var number = "GZY" + id + time.ToString("yyMMdd") + (appointmentList.Count + 1);
                    var model = new Model.Appointment
                    {
                        AddTime = DateTime.Now,
                        Contact = txtContact,
                        Content = txtContent,
                        Date = time,
                        ManagerID = id,
                        Name = txtName,
                        Number = number,
                        UserID = userInfo.id
                    };
                    if (new BLL.Appointment().Add(model) > 1)
                    {
                        var smsMsg = string.Empty;
                        var msgBLL = new BLL.sms_message();
                        var managerInfo = new DTcms.BLL.manager().GetModel(model.ManagerID);
                        //用户预约提醒
                        var userSMS = new BLL.sms_template().GetModel("UserAppointment"); //取得短信内容
                        msgBLL.Send(model.Contact, userSMS.content
                            .Replace("{name}", managerInfo.real_name)
                            .Replace("{time}", model.Date.ToString("yyyy-MM-dd"))
                            .Replace("{number}", model.Number), 1, out smsMsg);
                        //公证员预约提醒
                        var manageSMS = new BLL.sms_template().GetModel("ManageAppointment"); //取得短信内容
                        msgBLL.Send(managerInfo.telephone, manageSMS.content
                            .Replace("{name}", model.Name)
                            .Replace("{time}", model.Date.ToString("yyyy-MM-dd"))
                            .Replace("{number}", model.Number), 1, out smsMsg);
                        context.Response.Write(jsSerializer.Serialize(new
                        {
                            status = true
                        }));
                        return;
                    }
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        status = false,
                        msg = "数据添加失败"
                    }));
                    break;
                case "GetAppointment":
                    var pageSize = int.Parse(context.Request.QueryString["PageSize"]);//页大小
                    var pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);//页索引
                    //查询条件
                    string strWhere = "UserID=" + userInfo.id;
                    var bll = new DTcms.BLL.Appointment();
                    //总数
                    var totalCount = 0;
                    //结果集
                    var retAppointmentList = bll.GetModelList(pageSize, pageIndex + 1, strWhere, "AddTime desc", out totalCount);
                    //输出结果集
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        totalCount = totalCount,
                        list = retAppointmentList.Select(p => new
                        {
                            Number = p.Number,
                            Content = p.Content,
                            Date = p.Date,
                            ManagerName = new DTcms.BLL.manager().GetModel(p.ManagerID).real_name
                        })
                    }));
                    break;
            }
        }
    }
}