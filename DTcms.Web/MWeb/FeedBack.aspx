<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="DTcms.Web.MWeb.FeedBack" %>

<!DOCTYPE html>
<html>
<head>
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="style/common2.css" media="all">
    <link href="../script/easydialog.css" rel="stylesheet" />
    <script src="../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="../script/easydialog.js"></script>
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtName").val()) {
                    $.dialogAlert("请输入姓名");
                    return false;
                }
                if (!$("#txtTel").val()) {
                    $.dialogAlert("请输入联系方式");
                    return false;
                }
                if (!$("#txtContent").val()) {
                    $.dialogAlert("请输入服务要求");
                    return false;
                }
                //提交
                $.ajax({
                    url: "../ashx/FeedBack.ashx?option=MWebFeedBack",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            $.dialogAlert("留言成功", "提示信息", function () {
                                location.href = "index.aspx";
                            });
                        }
                    }
                });
            });
        });
    </script>
</head>
<body>
    <!--#include file ="module/header.shtml" -->
    <!--#include file ="module/sitemap.shtml" -->
    <form>
        <table class="tab_01" width="100%">
            <tr>
                <td align="right" width="25%" style="font-weight: bold">姓名：</td>
                <td>
                    <input id="txtName" name="txtName" class="txt_02" type="text" placeholder='请填写您的真实姓名'>
                    <span class="red">*</span></td>
            </tr>
            <tr>
                <td align="right" style="font-weight: bold">联系方式：</td>
                <td>
                    <input id="txtTel" name="txtTel" class="txt_02" type="text" placeholder='请填写您的有效联系方式'>
                    <span class="red">*</span></td>
            </tr>

            <tr>
                <td align="right" style="font-weight: bold">服务要求：</td>
                <td>
                    <textarea id="txtContent" name="txtContent" class="txt_05"></textarea>
                    <span class="red">*</span> </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><strong>以上红色<span class="red">*</span>为必填项</strong></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <input class="but_02" type="button" value="提交" id="btnTrue">
            </tr>
        </table>
    </form>
    <!--#include file ="module/foot.shtml" -->
</body>
</html>
