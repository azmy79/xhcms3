<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="DTcms.Web.UserCenter" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/user.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="scripts/datepicker/WdatePicker.js"></script>
    <script>
        $(function () {
            $("#btnEdit").click(function () {
                $("table").eq(0).hide().end().eq(1).show();
            });
            $("#btnFalse").click(function () {
                $("table").eq(1).hide().end().eq(0).show();
            });
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if ($("#txtEmail").val() && !/^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/.test($("#txtEmail").val())) {
                    $.dialogAlert("邮箱格式输入不正确");
                    return false;
                }
                if ($("#txtTelphone").val() && !/^\d+$/.test($("#txtTelphone").val())) {
                    $.dialogAlert("联系电话格式输入不正确");
                    return false;
                }
                //提交
                $.ajax({
                    url: "ashx/UserCenter.ashx?option=UpdateUserInfo",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            $.dialogAlert("保存成功", "提示信息", function () {
                                location = location;
                            });
                        }
                    }
                });
            });
        });
    </script>
</head>
<body>
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->
    <div class="w_user">
        <!--#include file ="module/UserMenu.shtml" -->
        <div class="right_user">
            <div class="pad15">
                <div class="w_zl">
                    <div class="c">
                        <form>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="10%" align="right">证件类型：</td>
                                    <td><%=Enum.Parse(typeof(DTcms.Common.DTEnums.证件类型),UserExt.CartType.ToString()).ToString() %></td>
                                </tr>
                                <tr>
                                    <td align="right">证件号码：</td>
                                    <td><%=UserExt.CartNum %></td>
                                </tr>
                                <tr>
                                    <td align="right">邮箱：</td>
                                    <td><%=UserInfo.email %></td>
                                </tr>
                                <tr>
                                    <td align="right">英文名：</td>
                                    <td><%=UserExt.EnName %></td>
                                </tr>
                                <tr>
                                    <td align="right">性别：</td>
                                    <td><%=UserInfo.sex %></td>
                                </tr>
                                <tr>
                                    <td align="right">出生年月：</td>
                                    <td><%=!string.IsNullOrEmpty(UserInfo.birthday.ToString()) ? Convert.ToDateTime(UserInfo.birthday.ToString()).ToString("yyyy-MM-dd") : string.Empty %></td>
                                </tr>
                                <tr>
                                    <td align="right">户籍所在地：</td>
                                    <td><%=UserExt.CRAddress %></td>
                                </tr>
                                <tr>
                                    <td align="right">现住地：</td>
                                    <td><%=UserInfo.address %></td>
                                </tr>
                                <tr>
                                    <td align="right">联系电话：</td>
                                    <td><%=UserInfo.telphone %></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <input type="button" id="btnEdit" class="style_but2" value="修改资料"></td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" width="100%" style="display: none">
                                <tr>
                                    <td align="right" width="10%">证件类型：</td>
                                    <td>
                                        <%
                                            foreach (var item in Enum.GetValues(typeof(DTcms.Common.DTEnums.证件类型)))
                                            {
                                                var enumObj = (DTcms.Common.DTEnums.证件类型)item;
                                                Response.Write("<span class=\"k\">");
                                                Response.Write("    <input " + (enumObj.GetHashCode() == UserExt.CartType ? "checked=\"checked\"" : string.Empty) + " name=\"radCartType\" type=\"radio\" value=\"" + enumObj.GetHashCode() + "\"/>");
                                                Response.Write("    " + enumObj.ToString() + "</span>");
                                            }
                                        %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">证件号码：</td>
                                    <td>
                                        <input value="<%=UserExt.CartNum %>" maxlength="20" id="txtCartNum" name="txtCartNum" class="style_test" type="text" placeholder='不能超过20个字符，真实资料'></td>
                                </tr>
                                <tr>
                                    <td align="right">邮箱：</td>
                                    <td>
                                        <input value="<%=UserInfo.email %>" maxlength="100" id="txtEmail" name="txtEmail" class="style_test" type="text" placeholder='不能超过100个字符'></td>
                                </tr>
                                <tr>
                                    <td align="right">英文名：</td>
                                    <td>
                                        <input value="<%=UserExt.EnName %>" maxlength="50" id="txtEnName" name="txtEnName" class="style_test" type="text" placeholder='不能超过50个字符，英文名'></td>
                                </tr>
                                <tr>
                                    <td align="right">性别：</td>
                                    <td><span class="k">
                                        <input name="radSex" type="radio" value="男" <%=UserInfo.sex=="男"?"checked=\"checked\"":string.Empty %>>
                                        男</span><span class="k">
                                            <input name="radSex" type="radio" value="女" <%=UserInfo.sex=="女"?"checked=\"checked\"":string.Empty %>>
                                            女</span></td>
                                </tr>
                                <tr>
                                    <td align="right">出生年月：</td>
                                    <td>
                                        <input value="<%=!string.IsNullOrEmpty(UserInfo.birthday.ToString()) ? Convert.ToDateTime(UserInfo.birthday.ToString()).ToString("yyyy-MM-dd") : string.Empty %>" id="txtBirthday" name="txtBirthday" class="style_test" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></td>
                                </tr>
                                <tr>
                                    <td align="right">户籍所在地：</td>
                                    <td>
                                        <input value="<%=UserExt.CRAddress %>" maxlength="200" id="txtCRAddress" name="txtCRAddress" class="style_test" type="text" placeholder='不能超过200个字符'></td>
                                </tr>
                                <tr>
                                    <td align="right">现住地：</td>
                                    <td>
                                        <input value="<%=UserInfo.address %>" maxlength="200" id="txtAddress" name="txtAddress" class="style_test" type="text" placeholder='不能超过200个字符'></td>
                                </tr>
                                <tr>
                                    <td align="right">联系电话：</td>
                                    <td>
                                        <input value="<%=UserInfo.telphone %>" maxlength="20" id="txtTelphone" name="txtTelphone" class="style_test" type="text" placeholder='不能超过20个字符'></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <input type="button" class="style_but2" value="保 存" id="btnTrue">
                                        <input type="button" class="style_but2" value="取消" id="btnFalse">
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
