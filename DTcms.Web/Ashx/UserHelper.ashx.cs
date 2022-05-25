using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Ashx
{
    /// <summary>
    /// UserHelper 的摘要说明
    /// </summary>
    public class UserHelper : DTcms.Web.UI.BasePage_Ajax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //操作类型
            switch (DTcms.Common.DTRequest.GetString("option"))
            {
                //发送注册手机验证码
                case "SendMobileCode":
                    var txtMobile = DTcms.Common.DTRequest.GetFormString("txtMobile");
                    if (!string.IsNullOrEmpty(txtMobile))
                        context.Response.Write(SendMobileCode(txtMobile, 2));
                    break;
                //注册
                case "UserRegister":
                    var radCartType = DTcms.Common.DTRequest.GetInt("radCartType", 0);
                    var radSex = DTcms.Common.DTRequest.GetString("radSex");
                    var txtAddress = DTcms.Common.DTRequest.GetString("txtAddress");
                    var txtBirthday = DTcms.Common.DTRequest.GetString("txtBirthday");
                    var txtMobileCode = DTcms.Common.DTRequest.GetString("txtMobileCode");
                    var txtCRAddress = DTcms.Common.DTRequest.GetString("txtCRAddress");
                    var txtCartNum = DTcms.Common.DTRequest.GetString("txtCartNum");
                    var txtCnName = DTcms.Common.DTRequest.GetString("txtCnName");
                    var txtEmail = DTcms.Common.DTRequest.GetString("txtEmail");
                    var txtEnName = DTcms.Common.DTRequest.GetString("txtEnName");
                    txtMobile = DTcms.Common.DTRequest.GetString("txtMobile");
                    var txtPassword = DTcms.Common.DTRequest.GetString("txtPassword");
                    var txtTelphone = DTcms.Common.DTRequest.GetString("txtTelphone");
                    var txtUserName = DTcms.Common.DTRequest.GetString("txtUserName");
                    //校检验证码
                    if (!CheckMobileCode(txtMobile, txtMobileCode, 2))
                    {
                        context.Response.Write(jsSerializer.Serialize(new
                        {
                            status = false,
                            msg = "验证码已过期或无效！"
                        })); return;
                    }
                    //检查用户输入信息是否为空
                    if (txtUserName == "" || txtPassword == "")
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"错误：用户名和密码不能为空！\"}");
                        return;
                    }
                    //检查手机号码
                    if (txtMobile == "")
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"错误：手机号码不能为空！\"}");
                        return;
                    }
                    //检查用户名
                    BLL.users bll = new BLL.users();
                    Model.users model = new Model.users();
                    if (bll.Exists(txtUserName))
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"对不起，该用户名已经存在！\"}");
                        return;
                    }
                    //不允许同一Email注册不同用户
                    if (!string.IsNullOrEmpty(txtEmail) && bll.ExistsEmail(txtEmail))
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"对不起，该邮箱已被使用！\"}");
                        return;
                    }
                    //不允许同一手机号码注册不同用户
                    if (bll.ExistsMobile(txtMobile))
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"对不起，该手机号码已被注册！\"}");
                        return;
                    }
                    //检查默认组别是否存在
                    Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
                    if (modelGroup == null)
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"用户尚未分组，请联系网站管理员！\"}");
                        return;
                    }
                    //保存注册信息
                    model.group_id = modelGroup.id;
                    model.user_name = txtUserName;
                    model.salt = Utils.GetCheckCode(6); //获得6位的salt加密字符串
                    model.password = DESEncrypt.Encrypt(txtPassword, model.salt);
                    model.email = txtEmail;
                    model.mobile = txtMobile;
                    model.reg_ip = DTRequest.GetIP();
                    model.reg_time = DateTime.Now;
                    model.sex = radSex;
                    model.address = txtAddress;
                    if (!string.IsNullOrEmpty(txtBirthday))
                        model.birthday = DateTime.Parse(txtBirthday);
                    model.telphone = txtTelphone;
                    //设置对应的状态
                    model.status = 0; //正常
                    int newId = bll.Add(model);
                    //保存拓展信息
                    if (newId < 1 || !new BLL.UserExt().Add(new Model.UserExt
                    {
                        CartNum = txtCartNum,
                        CartType = radCartType,
                        CnName = txtCnName,
                        CRAddress = txtCRAddress,
                        EnName = txtEnName,
                        UserID = newId
                    }))
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"系统故障，请联系网站管理员！\"}");
                        return;
                    }
                    model = bll.GetModel(newId);
                    context.Session[DTKeys.SESSION_USER_INFO] = model;
                    context.Session.Timeout = 45;
                    //防止Session提前过期
                    Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
                    Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
                    //写入登录日志
                    new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        status = true
                    }));
                    break;
                //找回密码步骤1
                case "UserFindPwd_Step1":
                    txtMobile = DTcms.Common.DTRequest.GetFormString("txtMobile");
                    //校检验证码
                    if (!CheckMobileCode(txtMobile, DTcms.Common.DTRequest.GetString("txtMobileCode"), 2))
                    {
                        context.Response.Write(jsSerializer.Serialize(new
                        {
                            status = false,
                            msg = "验证码已过期或无效！"
                        })); return;
                    }
                    //写入SESSION
                    context.Session["UserFindPwd"] = txtMobile;
                    context.Response.Write(jsSerializer.Serialize(new
                    {
                        status = true
                    }));
                    break;
                //找回密码步骤2
                case "UserFindPwd_Step2":
                    txtMobile = context.Session["UserFindPwd"].ToString();
                    if (string.IsNullOrEmpty(txtMobile))
                    {
                        context.Response.Write(jsSerializer.Serialize(new
                        {
                            status = false,
                            msg = "非法操作！"
                        })); return;
                    }
                    string password = context.Request.Form["txtPassword"];
                    //检查用户信息
                    bll = new BLL.users();
                    var list = DataConvertHelper.DataTableToList<DTcms.Model.users>(bll.GetList(1, "mobile='" + txtMobile + "'", "id").Tables[0]);
                    if (list.Count == 0)
                    {
                        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户不存在！\"}");
                        return;
                    }
                    model = list[0];
                    //执行修改操作
                    model.password = DESEncrypt.Encrypt(password, model.salt);
                    bll.Update(model);
                    //清除SESSION
                    context.Session["UserFindPwd"] = null;
                    context.Response.Write("{\"status\":1, \"msg\":\"修改密码成功，请记住新密码！\"}");
                    break;
            }
        }
    }
}