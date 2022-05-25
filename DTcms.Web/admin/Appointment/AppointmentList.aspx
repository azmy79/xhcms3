<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentList.aspx.cs" Inherits="DTcms.Web.admin.Appointment.AppointmentList" %>


<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>预约信息管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>预约信息管理</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th>预约号</th>
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>预约时间</th>
                        <th>公证员</th>
                        <th>创建时间</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center"><%#Eval("Number") %></td>
                        <td align="center"><%#Eval("Name") %></td>
                        <td align="center"><%#Eval("Contact") %></td>
                        <td align="center"><%#Convert.ToDateTime(Eval("Date")).ToString("yyyy-MM-dd") %></td>
                        <td align="center"><%#new DTcms.BLL.manager().GetModel(Convert.ToInt32(Eval("ManagerID"))).real_name %></td>
                        <td align="center"><%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td align="center"><a href="AppointmentDetail.aspx?id=<%#Eval("ID") %>">查看详细</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
