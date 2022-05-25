<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="DTcms.Web.FeedBack" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/doc.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/jquery.pagination.js"></script>
    <script src="script/custom/PageDataHelper.js"></script>
    <script src="script/custom/Common.js"></script>
    <script src="script/easydialog.js"></script>
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
                if (!$("#txtCode").val()) {
                    $.dialogAlert("请输入验证码");
                    return false;
                }
                //提交
                $.ajax({
                    url: "ashx/FeedBack.ashx?option=FeedBack",
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
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <%
        var model = GetCategory(DTcms.Common.DTRequest.GetQueryInt("cid"));
        if (model == null) SkipIndex();
    %>
    <!--顶部banner-->
    <div class="w">
        <div class="style_but3"><%=model.title %></div>
        <div class="doc_message">
            <div class="change_034"><span class="curr" lang="a1">在线留言</span> <span lang="a2">查看留言</span></div>
            <div class="clear"></div>
            <div class="change_034c">
                <div id="a1" class="wrap dis">
                    <p>
                        <%=model.seo_description %>
                    </p>
                    <form>
                        <table class="tab_01" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="120"><span class="red">*</span> 姓名：</td>
                                <td>
                                    <input id="txtName" name="txtName" class="style_test" type="text" placeholder='请填写您的真实姓名'></td>
                            </tr>
                            <tr>
                                <td><span class="red">*</span> 联系方式：</td>
                                <td>
                                    <input id="txtTel" name="txtTel" class="style_test" type="text" placeholder='请填写您的有效联系方式'></td>
                            </tr>
                            <tr>
                                <td><span class="red">*</span> 服务要求：</td>
                                <td>
                                    <textarea id="txtContent" name="txtContent" class="style_testarea" placeholder="请填写您的服务要求"></textarea></td>
                            </tr>

                            <tr>
                                <td><span class="red">*</span> 验证码：</td>
                                <td>
                                    <input name="txtCode" id="txtCode" class="style_test2" style="background: url(images/ico_u5.png) no-repeat 10px center #ffffff" type="text" placeholder='请输入验证码'>
                                    <img style="cursor: pointer" align="absmiddle" src="tools/verify_code.ashx" width="100" height="42" onclick="this.src='tools/verify_code.ashx?'+Math.random()">
                                    看不清，点击图片切换
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><span class="k">
                                    <input class="style_but2" type="button" value="提交" id="btnTrue">
                                </span><span class="k">
                                    <input class="style_but2" type="reset" value="重置">
                                </span></td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div id="a2" class="undis">
                    <div class="list_message2">
                        <div class="cls_news">
                            <ul>
                            </ul>
                            <div class="clear"></div>
                            <div class="page">
                                <script>
                                    //分页查询
                                    $(".page").SetPagination({
                                        Container: ".cls_news ul",//容器选择器
                                        Url: "Ashx/FeedBack.ashx",//请求地址
                                        PageSize: 6,//页大小
                                        Data: {
                                            option: "GetFeedBackData",
                                        },//请求参数
                                        htmlJoinFunc: function (htmlArr, i, obj) {
                                            htmlArr.push("<li>");
                                            htmlArr.push("    <h2><span>【<em>" + obj.UserName + "</em> 的留言】</span><span>时间：" + obj.AddTime + "</span>  " + obj.Content + "</h2>");
                                            htmlArr.push("    <p class=\"down\"><span class=\"red\">官方回复：</span>" + obj.ReContent + " </p>");
                                            htmlArr.push("</li>");
                                        }//HTML拼接回调(待拼接HTML数组,索引,对象)
                                    });
                                </script>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <!--翻页-->
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>

