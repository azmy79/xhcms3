<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="DTcms.Web.UserRegister" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/login.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="scripts/datepicker/WdatePicker.js"></script>
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!/^[A-Za-z]+\w*\d+\w*$/.test($("#txtUserName").val())) {
                    $.dialogAlert("用户名不能为空且必须包括英文和数字,以英文开头");
                    return false;
                }
                if (!/^\w{6,16}$/.test($("#txtPassword").val())) {
                    $.dialogAlert("密码不能为空且必须为6-16个字符");
                    return false;
                }
                if (!$("#txtPasswordAgain").val()) {
                    $.dialogAlert("请输入确认密码");
                    return false;
                } else if ($("#txtPasswordAgain").val() != $("#txtPassword").val()) {
                    $.dialogAlert("两次密码输入不一致");
                    return false;
                }
                if (!/^[\u4e00-\u9fa5]+$/.test($("#txtCnName").val())) {
                    $.dialogAlert("姓名不能为空且必须为中文");
                    return false;
                }
                if (!$("#txtMobile").val()) {
                    $.dialogAlert("请输入手机号码");
                    return false;
                } else if (!/^[0-9]{11}$/.test($("#txtMobile").val())) {
                    $.dialogAlert("手机号码格式输入不正确");
                    return false;
                }
                if (!$("#txtMobileCode").val()) {
                    $.dialogAlert("请输入短信验证码");
                    return false;
                }
                if (!$("#cbAgree")[0].checked) {
                    $.dialogAlert("请接受\"长江公证处用户注册协议\"");
                    return false;
                }
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
                    url: "ashx/UserHelper.ashx?option=UserRegister",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            location.href = "RegisterSuccess.aspx";
                        }
                    }
                });
            });

            var timeNum = 0;

            //发送手机验证码
            $("#btnSendMobileCode").click(function () {
                if (!$("#txtMobile").val()) {
                    $.dialogAlert("请输入手机号码");
                    return;
                } else if (!/^[0-9]{11}$/.test($("#txtMobile").val())) {
                    $.dialogAlert("手机号码格式输入不正确");
                    return;
                }
                if (timeNum) return;//时间间隔未到
                var $a = $(this).find("a");
                //提交
                $.ajax({
                    url: "ashx/UserHelper.ashx?option=SendMobileCode",
                    data: $("#txtMobile").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            timeNum = 120;
                            $a.text("立即发送验证码(" + timeNum + "s)");
                            var sl = setInterval(function () {
                                timeNum--;
                                if (!timeNum) {
                                    clearInterval(sl);
                                    $a.text("立即发送验证码");
                                } else
                                    $a.text("立即发送验证码(" + timeNum + "s)");
                            }, 1000);
                        };
                    }
                });
            });
        });
    </script>
</head>
<body style="background: url(images/bg_user.jpg) no-repeat center 80px; background-attachment: fixed">
    <uc1:Header runat="server" ID="Header" />
    <form>
        <!--顶部banner-->
        <div class="w">
            <div class="w_login">
                <h2>用户注册</h2>
                <div class="c">
                    <h3>填写您的注册信息</h3>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right"><span class="red">*</span> 用户名：</td>
                            <td>
                                <input id="txtUserName" name="txtUserName" class="style_test" type="text">
                                <p class="red">包括英文和数字，以英文开头</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span>密码：</td>
                            <td>
                                <input maxlength="16" id="txtPassword" name="txtPassword" class="style_test" type="password">
                                <p class="red">6-16个字符</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span>重复：</td>
                            <td>
                                <input maxlength="16" id="txtPasswordAgain" name="txtPasswordAgain" class="style_test" type="password">
                                <p class="red">两次密码必须一致</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span> 姓名：</td>
                            <td>
                                <input maxlength="20" id="txtCnName" name="txtCnName" class="style_test" type="text">
                                <p class="red">不能超过20个字符，中文名</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span> 手机：</td>
                            <td>
                                <input maxlength="11" id="txtMobile" name="txtMobile" class="style_test" type="text">
                                <p class="red">11位手机号</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><span class="red">*</span> 短信验证码：</td>
                            <td>
                                <div class="f_l">
                                    <input id="txtMobileCode" name="txtMobileCode" class="style_test2" type="text">
                                </div>
                                <div id="btnSendMobileCode" style="padding: 2px 0px 0px 8px;" class="f_l"><a class="style_but2" href="javascript:;">立即发送验证码</a></div>
                                <br />
                                <br />
                                <br />
                                <p class="red">2分钟后失效</p>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input id="cbAgree" name="cbAgree" style="vertical-align: middle" name="" type="checkbox" value="">
                                我接受<a class="blue" href="Single.aspx?cid=77" target="_blank"> 长江公证处用户注册协议</a></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <input class="style_but2" type="button" id="btnTrue" value="接受协议并注册"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>如果在注册过程中无法获取短信验证码，请与我处技术人员联系。
                            <br />
                                联系电话：<span class="red"><%=config.webtel %></span></td>
                        </tr>
                    </table>
                </div>
                <div class="line1"></div>
                <div class="air"></div>
                <div class="c">
                    <h3>填写您的个人信息</h3>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">证件类型：</td>
                            <td>
                                <%
                                    var isFirst = true;
                                    foreach (var item in Enum.GetValues(typeof(DTcms.Common.DTEnums.证件类型)))
                                    {
                                        var enumObj = (DTcms.Common.DTEnums.证件类型)item;
                                        Response.Write("<span class=\"k\">");
                                        Response.Write("    <input " + (isFirst ? "checked=\"checked\"" : string.Empty) + " name=\"radCartType\" type=\"radio\" value=\"" + enumObj.GetHashCode() + "\"/>");
                                        Response.Write("    " + enumObj.ToString() + "</span>");
                                        isFirst = false;
                                    }
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">证件号码：</td>
                            <td>
                                <input maxlength="20" id="txtCartNum" name="txtCartNum" class="style_test" type="text" placeholder='不能超过20个字符，真实资料'></td>
                        </tr>
                        <tr>
                            <td align="right">邮箱：</td>
                            <td>
                                <input maxlength="100" id="txtEmail" name="txtEmail" class="style_test" type="text" placeholder='不能超过100个字符'></td>
                        </tr>
                        <tr>
                            <td align="right">英文名：</td>
                            <td>
                                <input maxlength="50" id="txtEnName" name="txtEnName" class="style_test" type="text" placeholder='不能超过50个字符，英文名'></td>
                        </tr>
                        <tr>
                            <td align="right">性别：</td>
                            <td><span class="k">
                                <input name="radSex" type="radio" value="男" checked="checked">
                                男</span><span class="k">
                                    <input name="radSex" type="radio" value="女">
                                    女</span></td>
                        </tr>
                        <tr>
                            <td align="right">出生年月：</td>
                            <td>
                                <input id="txtBirthday" name="txtBirthday" class="style_test" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></td>
                        </tr>
                        <tr>
                            <td align="right">户籍所在地：</td>
                            <td>
                                <input maxlength="200" id="txtCRAddress" name="txtCRAddress" class="style_test" type="text" placeholder='不能超过200个字符'></td>
                        </tr>
                        <tr>
                            <td align="right">现住地：</td>
                            <td>
                                <input maxlength="200" id="txtAddress" name="txtAddress" class="style_test" type="text" placeholder='不能超过200个字符'></td>
                        </tr>
                        <tr>
                            <td align="right">联系电话：</td>
                            <td>
                                <input maxlength="20" id="txtTelphone" name="txtTelphone" class="style_test" type="text" placeholder='不能超过20个字符'></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
