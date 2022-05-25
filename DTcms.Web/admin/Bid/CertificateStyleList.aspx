<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateStyleList.aspx.cs" Inherits="DTcms.Web.admin.Bid.CertificateStyleList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>证书样式管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="CertificateStyleEdit.aspx?bbsid=<%=DTcms.Common.DTRequest.GetQueryString("bbsid") %>"><i></i><span>新增</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton>
                        </li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','本操作会删除本类别及下属子类别，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th width="6%">选择</th>
                        <th align="left">名称</th>
                        <th align="left" width="12%">排序</th>
                        <th width="12%">操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("ID")%>' runat="server" />
                        </td>
                        <td><a href="CertificateStyleEdit.aspx?option=edit&id=<%#Eval("ID")%>&bbsid=<%#Eval("BidBusinessID") %>"><%#Eval("Title")%></a>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("Sort")%>' CssClass="sort" onkeydown="return checkNumber(event);" />
                        </td>
                        <td align="center">
                            <a href="CertificateStyleEdit.aspx?option=edit&id=<%#Eval("ID")%>&bbsid=<%#Eval("BidBusinessID") %>">修改</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"4\">暂无记录</td></tr>" : ""%>
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
