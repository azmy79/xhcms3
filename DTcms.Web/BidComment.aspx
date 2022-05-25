<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidComment.aspx.cs" Inherits="DTcms.Web.BidComment" %>

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
            <div class="change_034"><span class="curr" lang="a1">查看评论</span></div>
            <div class="clear"></div>
            <div class="change_034c">
                <div id="a2">
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
                                            MsgType: 1
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

