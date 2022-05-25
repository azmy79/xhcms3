<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAppointment.aspx.cs" Inherits="DTcms.Web.UserAppointment" %>

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
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script src="script/jquery.pagination.js"></script>
    <script src="script/custom/PageDataHelper.js"></script>
    <script src="script/custom/Common.js"></script>
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
                                        <td width="10%">预约号</td>
                                        <td width="10%">公证员</td>
                                        <td width="60%">办理公证事项</td>
                                        <td width="20%">预约时间</td>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </li>
                    </ul>
                    <div class="clear"></div>
                    <div class="page">
                        <script>
                            //分页查询
                            $(".page").SetPagination({
                                Container: "table tbody",//容器选择器
                                Url: "Ashx/UserAppointment.ashx",//请求地址
                                PageSize: 6,//页大小
                                Data: {
                                    option: "GetAppointment"
                                },//请求参数
                                htmlJoinFunc: function (htmlArr, i, obj) {
                                    htmlArr.push("<tr>");
                                    htmlArr.push("    <td>" + obj.Number + "</td>");
                                    htmlArr.push("    <td>" + obj.ManagerName + "</td>");
                                    htmlArr.push("    <td>" + obj.Content + "</td>");
                                    htmlArr.push("    <td>" + obj.Date.ToDate().ToString("yyyy-MM-dd") + "</td>");
                                    htmlArr.push("</tr>");
                                }//HTML拼接回调(待拼接HTML数组,索引,对象)
                            });
                        </script>
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
