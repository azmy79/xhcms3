﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidComment.aspx.cs" Inherits="DTcms.Web.admin.Bid.BidComment" %>
<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>在线留言管理</title>
	<link type="text/css" rel="stylesheet" href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" />
	<link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
	<script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
	<script type="text/javascript" src="../../../<%=siteConfig.webmanagepath %>/js/layout.js"></script>
</head>

<body class="mainbody">
	<form id="form1" runat="server">
		<!--导航栏-->
		<div class="location">
			<a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
			<a href="../../../<%=siteConfig.webmanagepath %>/center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>申办管理</span>
			<i class="arrow"></i>
			<span>服务评论</span>
		</div>
		<!--/导航栏-->

		<!--工具栏-->
		<div class="toolbar-wrap">
			<div id="floatHead" class="toolbar">
				<div class="l-list">
					<ul class="icon-list">
						<li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
						<li>
							<asp:LinkButton ID="lbtnUnLock" runat="server" CssClass="folder" OnClientClick="return ExePostBack('lbtnUnLock','审核后前台将显示该信息，确定继续吗？');" OnClick="lbtnUnLock_Click"><i></i><span>审核</span></asp:LinkButton></li>
						<li>
							<asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
					</ul>
				</div>
				<div class="r-list">
					<asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
					<asp:LinkButton ID="btnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
				</div>
			</div>
		</div>
		<!--/工具栏-->

		<!--列表-->
		<asp:Repeater ID="rptList" runat="server">
			<HeaderTemplate>
				<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
					<tr>
						<th width="8%">选择</th>
						<th align="left" style="display:none">标题</th>
						<th width="16%" align="left">用户</th>
						<th width="16%" align="left">发布时间</th>
						<th width="8%">状态</th>
						<th width="10%">操作</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
						<asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
					</td>
					<td  style="display:none"><a href="reply.aspx?id=<%#Eval("id")%>"><%#Eval("title")%><%# Convert.ToInt32(Eval("is_lock")) == 1 ? "(未审核)" : ""%></a></td>
					<td><%#Eval("user_name")%></td>
					<td><%#string.Format("{0:g}",Eval("add_time"))%></td>
					<td align="center">
						<%#Eval("reply_content").ToString() == "" ? "未回复" : "已回复"%>
					</td>
					<td align="center"><a href="BidCommentReply.aspx?id=<%#Eval("id")%>">回复</a></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				<%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%>
</table>
			</FooterTemplate>
		</asp:Repeater>
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