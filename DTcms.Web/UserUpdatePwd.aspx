<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserUpdatePwd.aspx.cs" Inherits="DTcms.Web.UserUpdatePwd" %>

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
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtOldPassword").val()) {
                    $.dialogAlert("请输入原密码");
                    return false;
                }
                if (!/^\w{6,16}$/.test($("#txtPassword").val())) {
                    $.dialogAlert("密码不能为空且必须为6-16个字符");
                    return false;
                }
                if (!$("#txtPasswordAgain").val()) {
                    $.dialogAlert("请输入确认新密码");
                    return false;
                } else if ($("#txtPasswordAgain").val() != $("#txtPassword").val()) {
                    $.dialogAlert("两次密码输入不一致");
                    return false;
                }
                //提交
                $.ajax({
                    url: "tools/submit_ajax.ashx?action=user_password_edit",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        $.dialogAlert(ret.msg);
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
            <div class="tab_01">
                <form>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right"><span class="red">*</span>原密码：</td>
                            <td>
                                <input class="style_test" type="password" id="txtOldPassword" name="txtOldPassword" placeholder="原密码" maxlength="16"></td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span>新密码：</td>
                            <td>
                                <input class="style_test" type="password" id="txtPassword" name="txtPassword" placeholder='6-16个字符' maxlength="16"></td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span>再次密码：</td>
                            <td>
                                <input class="style_test" type="password" id="txtPasswordAgain" name="txtPasswordAgain" placeholder='两次密码必须一致' maxlength="16"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input class="style_but2" type="button" id="btnTrue" value="保存设置"></td>
                        </tr>
                    </table>
                </form>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
