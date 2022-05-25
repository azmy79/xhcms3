using DTcms.Common;
using DTcms.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DTcms.Web.Ashx
{
    /// <summary>
    /// 用户中心涉及一般处理程序
    /// </summary>
    public class UserCenter : BasePage_Ajax
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
            var option = DTRequest.GetQueryString("option");
            switch (option)
            {
                // 修改用户信息
                case "UpdateUserInfo":
                    var radCartType = DTcms.Common.DTRequest.GetInt("radCartType", 0);
                    var radSex = DTcms.Common.DTRequest.GetString("radSex");
                    var txtAddress = DTcms.Common.DTRequest.GetString("txtAddress");
                    var txtBirthday = DTcms.Common.DTRequest.GetString("txtBirthday");
                    var txtCRAddress = DTcms.Common.DTRequest.GetString("txtCRAddress");
                    var txtCartNum = DTcms.Common.DTRequest.GetString("txtCartNum");
                    var txtEmail = DTcms.Common.DTRequest.GetString("txtEmail");
                    var txtEnName = DTcms.Common.DTRequest.GetString("txtEnName");
                    var txtTelphone = DTcms.Common.DTRequest.GetString("txtTelphone");
                    var userBll = new BLL.users();
                    //检查Email
                    if (userInfo.email != txtEmail && userBll.ExistsEmail(txtEmail))
                    {
                        context.Response.Write(ReturnMsg("对不起，该邮箱已经被使用", false));
                        return;
                    }
                    //赋值
                    userInfo.sex = radSex;
                    userInfo.address = txtAddress;
                    userInfo.email = txtEmail;
                    userInfo.telphone = txtTelphone;
                    if (!string.IsNullOrEmpty(txtBirthday))
                        userInfo.birthday = DateTime.Parse(txtBirthday);
                    if (userBll.Update(userInfo))
                    {
                        var userExtBll = new DTcms.BLL.UserExt();
                        var userExt = userExtBll.GetModel(userInfo.id);
                        var isEdit = true;//默认修改操作
                        //无记录
                        if (userExt == null)
                        {
                            isEdit = false;
                            userExt = new Model.UserExt();
                            userExt.UserID = userInfo.id;
                        }
                        userExt.CartType = radCartType;
                        userExt.CartNum = txtCartNum;
                        userExt.CRAddress = txtCRAddress;
                        userExt.EnName = txtEnName;
                        var ret = false;//最终返回结果
                        if (isEdit)
                            ret = userExtBll.Update(userExt);//修改操作
                        else
                            ret = userExtBll.Add(userExt);//新增操作
                        if (ret)
                            context.Response.Write(ReturnMsg());
                        else
                            context.Response.Write(ReturnMsg("保存失败", false));
                    }
                    break;
            }
        }
    }
}