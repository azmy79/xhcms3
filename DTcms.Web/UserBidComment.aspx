<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBidComment.aspx.cs" Inherits="DTcms.Web.UserBidComment" %>

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
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtContent").val()) {
                    $.dialogAlert("请输入服务评论");
                    return false;
                }
                if (!$("#txtCode").val()) {
                    $.dialogAlert("请输入验证码");
                    return false;
                }
                //提交
                $.ajax({
                    url: "ashx/FeedBack.ashx?option=BidComment",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            $.dialogAlert("评论成功", "提示信息", function () {
                                history.go(-1);
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
                                    <td width="10%" align="right"><span class="red">*</span> 服务评论：</td>
                                    <td>
                                        <textarea id="txtContent" name="txtContent" class="style_testarea" placeholder="请填写您的服务评论"></textarea></td>
                                </tr>

                                <tr>
                                    <td align="right"><span class="red">*</span> 验证码：</td>
                                    <td>
                                        <input name="txtCode" id="txtCode" class="style_test2" style="background: url(images/ico_u5.png) no-repeat 10px center #ffffff" type="text" placeholder='请输入验证码'>
                                        <img style="cursor: pointer" align="absmiddle" src="tools/verify_code.ashx" width="100" height="42" onclick="this.src='tools/verify_code.ashx?'+Math.random()">
                                        看不清，点击图片切换
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <input class="style_but2" type="button" value="提交" id="btnTrue">
                                        <input class="style_but2" type="reset" value="重置">
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
