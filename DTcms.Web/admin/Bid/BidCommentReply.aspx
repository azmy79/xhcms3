﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidCommentReply.aspx.cs" Inherits="DTcms.Web.admin.Bid.BidCommentReply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>回复评论信息</title>
	<link type="text/css" rel="stylesheet" href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" />
	<script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
	<script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
	<script type="text/javascript" src="../../../<%=siteConfig.webmanagepath %>/js/layout.js"></script>
	<script type="text/javascript">
	    $(function () {
	        //初始化表单验证
	        $("#form1").initValidform();
	    });
	</script>
</head>
<body class="mainbody">
	<form id="form1" runat="server">
		<!--导航栏-->
		<div class="location">
			<a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
			<a href="../../../<%=siteConfig.webmanagepath %>/center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<span>申办管理</span>
            <i class="arrow"></i>
			<span>服务评论</span>
			<i class="arrow"></i>
			<span>回复评论</span>
		</div>
		<div class="line10"></div>
		<!--/导航栏-->

		<!--内容-->
		<div class="content-tab-wrap">
			<div id="floatHead" class="content-tab">
				<div class="content-tab-ul-wrap">
					<ul>
						<li><a href="javascript:;" onclick="tabs(this);" class="selected">查看评论</a></li>
					</ul>
				</div>
			</div>
		</div>

		<div class="tab-content">
			<dl>
				<dt>用户</dt>
				<dd><%=model.user_name %></dd>
			</dl>
			<dl>
				<dt>联系电话</dt>
				<dd><%=model.user_tel %></dd>
			</dl>
			<dl style="display:none">
				<dt>联系QQ</dt>
				<dd><%=model.user_qq %></dd>
			</dl>
			<dl style="display:none">
				<dt>电子邮箱</dt>
				<dd><%=model.user_email %></dd>
			</dl>
			<dl>
				<dt>评论时间</dt>
				<dd><%=model.add_time %></dd>
			</dl>
			<dl style="display:none">
				<dt>评论标题</dt>
				<dd><%=model.title %></dd>
			</dl>
			<dl>
				<dt>评论内容</dt>
				<dd><%=model.content %></dd>
			</dl>
			<dl>
				<dt>显示状态</dt>
				<dd>
					<div class="rule-multi-radio">
						<asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
							<asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
							<asp:ListItem Value="1">下线</asp:ListItem>
						</asp:RadioButtonList>
					</div>
				</dd>
			</dl>
			<dl>
				<dt>回复评论</dt>
				<dd>
					<asp:TextBox ID="txtReContent" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" " /></dd>
			</dl>
		</div>
		<!--/内容-->

		<!--工具栏-->
		<div class="page-footer">
			<div class="btn-list">
				<asp:Button ID="btnSubmit" runat="server" Text="回复评论" CssClass="btn" OnClick="btnSubmit_Click" />
				<input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
			</div>
			<div class="clear"></div>
		</div>
		<!--/工具栏-->
	</form>
</body>
</html>
