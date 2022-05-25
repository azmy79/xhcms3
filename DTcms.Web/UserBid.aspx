<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBid.aspx.cs" Inherits="DTcms.Web.UserBid" %>

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
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/jquery.pagination.js"></script>
    <script src="script/custom/PageDataHelper.js"></script>
    <script src="script/custom/Common.js"></script>
    <script src="script/easydialog.js"></script>
    <script>
        //初始化分页
        //PageIndex:页码
        var iniPager = function (PageIndex) {
            //分页查询
            $(".page").SetPagination({
                Container: "table tbody",//容器选择器
                Url: "Ashx/UserBid.ashx",//请求地址
                PageSize: 6,//页大小
                PageIndex: PageIndex || 0,
                Data: {
                    option: "GetBid"
                },//请求参数
                htmlJoinFunc: function (htmlArr, i, obj) {
                    htmlArr.push("<tr>");
                    htmlArr.push("    <td>" + obj.Number + "</td>");
                    htmlArr.push("    <td>" + obj.BidBusiness + "</td>");
                    htmlArr.push("    <td>￥" + obj.Price + "</td>");
                    htmlArr.push("    <td>" + function () {
                        switch (obj.Status) {
                            case 0: return "审核中";
                            case 1: return "审核通过";
                            case 3: return "已取消";
                        }
                    }() + "</td>");
                    htmlArr.push("    <td>" + obj.AddTime.ToDate().ToString("yyyy-MM-dd") + "</td>");
                    htmlArr.push("    <td>" + function () {
                        switch (obj.Status) {
                            case 0: return "<a href=\"Bid_Step1.aspx?cid=58&op=edit&id=" + obj.ID + "\">【修改】</a><a href=\"javascript:;\" onclick=\"cancelBid(" + obj.ID + ")\">【取消申办】</a>";
                            case 1: return "<a href=\"Bid_Step3.aspx?cid=58&id=" + obj.ID + "\">【立即支付】</a>";
                            case 3: return "";
                        }
                    }() + "</td>");
                    htmlArr.push("</tr>");
                }//HTML拼接回调(待拼接HTML数组,索引,对象)
            });
        };

        //取消申办
        var cancelBid = function (ID) {
            $.dialogConfirm("确定取消该申办信息？", "警告", function () {
                $.ajax({
                    type: "POST",//请求方式
                    url: "Ashx/UserBid.ashx?option=CancelBid",
                    dataType: "JSON",
                    cache: false,//禁止缓存
                    data: {
                        id: ID
                    }
                }).done(function (ret) {
                    if (!ret.status)
                        $.dialogAlert(ret.msg);
                    else {
                        $.dialogAlert("取消申办成功", "提示信息", function () {
                            //刷新数据
                            iniPager(parseInt($(".page a[class='num curr']").text()) - 1);
                        });
                    }
                });
            })
        };

        //加载运行
        $(function () {
            //初始化分页
            iniPager();
        });
    </script>
</head>
<body>
    <uc1:Header runat="server" ID="Header" />
    <div class="w_user">
        <!--#include file ="module/UserMenu.shtml" -->
        <div class="right_user">
            <div class="pad15">
                <div class="list_dd">
                    <ul>
                        <li>
                            <table width="100%" cellpadding="0" cellspacing="1" style="background: #ccc">
                                <thead>
                                    <tr bgcolor="#666666" style="color: #FFFFFF; font-weight: bold">
                                        <td width="10%">申办号</td>
                                        <td width="30%">申办内容</td>
                                        <td width="10%">价格</td>
                                        <td width="10%">状态</td>
                                        <td width="20%">申办时间</td>
                                        <td width="20%">操作</td>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </li>
                    </ul>
                    <div class="clear"></div>
                    <div class="page">
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
