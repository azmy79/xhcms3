<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFindPwd_Step2.aspx.cs" Inherits="DTcms.Web.UserFindPwd_Step2" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/login.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
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
                    url: "ashx/UserHelper.ashx?option=UserFindPwd_Step2",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else
                            location.href = "UserFindPwd_Step3.aspx";
                    }
                });
            });
        });
    </script>
</head>
<body style="background: url(images/bg_user.jpg) no-repeat center 80px; background-attachment: fixed">
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->

    <!--顶部banner-->
    <div class="w">
        <div class="w_login">
            <h2>找回密码</h2>
            <div class="c">
                <form>
                    <div class="regist_step" style="background: url(images/mm.gif) no-repeat 0px -130px; margin: 0 auto 30px; width: 551px; height: 130px;"></div>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr></tr>
                        <tr>
                            <td align="right">新密码：</td>
                            <td>
                                <input id="txtPassword" name="txtPassword" class="style_test" type="password" placeholder='6-16个字符' maxlength="16"></td>
                        </tr>
                        <tr>
                            <td align="right">重复密码：</td>
                            <td>
                                <input id="txtPasswordAgain" name="txtPasswordAgain" class="style_test" type="password" placeholder='两次密码必须一致' maxlength="16"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input class="style_but2" type="button" id="btnTrue" value="提交"></td>
                        </tr>
                    </table>
                </form>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
