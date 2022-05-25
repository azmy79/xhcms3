<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_item.aspx.cs" Inherits="DTcms.Web.admin.ad.ad_list" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>内容类别</title>
	<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
	<script type="text/javascript" src="../js/layout.js"></script>
	<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
</head>

<body class="mainbody">
	<form id="form1" runat="server">
		<!--导航栏-->
		<div class="location">
			<a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
			<a href="../center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>内容类别</span>
		</div>
		<!--/导航栏-->

		<!--工具栏-->
		<div class="toolbar-wrap">
			<div id="floatHead" class="toolbar">
				<div class="l-list">
					<ul class="icon-list">
						<li><a class="add" href="ad_item_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&ad_id=<%=this.ad_id %>"><i></i><span>新增</span></a></li>
						<%--<li>
							<asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>--%>
						<li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
						<li>
							<asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','本操作会删除本类别及下属子类别，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
					</ul>
				</div>
			</div>
		</div>
		<!--/工具栏-->

		<!--列表-->
		<asp:Repeater ID="rptList" runat="server">
			<HeaderTemplate>
				<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
					<tr>
						<th width="6%">选择</th>
						<th align="left" width="6%">编号</th>
						<th align="left">广告名称</th>
						<th align="left" width="12%">开始时间</th>
						<th align="left" width="12%">到期时间</th>
						<th width="6%">状态</th>
						<th width="12%">操作</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
						<asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
					</td>
					<td><%#Eval("id")%></td>
					<td>
						<a href="ad_item_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&ad_id=<%#this.ad_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
					<td><%#Eval("start_time") %></td>
					<td><%#Eval("end_time") %></td>
					<td align="center"><%#GetState(Eval("is_lock").ToString(),Eval("end_time").ToString()) %></td>
					<td align="center">
						<a href="ad_item_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&ad_id=<%#this.ad_id %>&id=<%#Eval("id")%>">修改</a>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				<%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%>
</table>
			</FooterTemplate>
		</asp:Repeater>
		<!--/列表-->
	</form>
</body>
</html>
