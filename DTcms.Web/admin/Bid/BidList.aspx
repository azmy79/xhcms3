<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidList.aspx.cs" Inherits="DTcms.Web.admin.Bid.BidList" %>


<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>申办信息管理</title>
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
            <span>申办信息管理</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                <HeaderTemplate>
                    <tr>
                        <th width="6%">申办号</th>
                        <th>姓名</th>
                        <th>电话</th>
                        <th width="30%">申办内容</th>
                        <th width="12%">价格</th>
                        <th width="12%">申办时间</th>
                        <th width="12%">状态</th>
                        <th width="12%">操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center"><%#Eval("Number")%>
                        </td>
                        <td align="center"><%#Eval("CnName") %></td>
                        <td align="center"><%#Eval("Tel") %></td>
                        <td align="center"><%#Eval("BidBusiness") %></td>
                        <td align="center">￥<%#Convert.ToInt32(Eval("Price")) %></td>
                        <td align="center"><%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td align="center"><%#Eval("Status").ToString()=="0"?"审核中":Eval("Status").ToString()=="1"?"审核通过":"已取消"%></td>
                        <td align="center">
                            <span <%#Eval("Status").ToString()=="3"?"style=\"display:none\"":string.Empty %>>
                                <a href="BidAudit.aspx?id=<%#Eval("ID")%>">审核</a>
                                <asp:LinkButton ID='lbnCancelBid' runat='server' CommandName='CancelBid' CommandArgument='<%#Eval("ID") %>' Visible='<%#Convert.ToInt32(Eval("Status"))!=3?true:false %>' OnClientClick="return confirm('确定取消该申办信息？')">取消申办</asp:LinkButton>
                            </span>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
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
