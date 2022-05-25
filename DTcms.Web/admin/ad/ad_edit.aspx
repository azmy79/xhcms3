<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_edit.aspx.cs" Inherits="DTcms.Web.admin.ad.ad_edit" ValidateRequest="false" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

	<title>编辑类别</title>
	<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
	<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
	<script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
	<script type='text/javascript' src="../../scripts/swfupload/swfupload.js"></script>
	<script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
	<script type="text/javascript" src="../js/layout.js"></script>
	<script type="text/javascript" src="../js/pinyin.js"></script>
	<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		$(function () {
			//初始化表单验证
			$("#form1").initValidform();

			$("#btnVarAdd").click(function () {
				varHtml = createVarHtml();
				$("#tr_box").append(varHtml);

			});



			//初始化上传控件
			$(".upload-img").each(function () {
				$(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
			});
			////初始化编辑器
			//var editorMini = KindEditor.create('#txtContent', {
			//	width: '98%',
			//	height: '250px',
			//	resizeType: 1,
			//	uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
			//	items: [
			//		'source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
			//		'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
			//		'insertunorderedlist', '|', 'image', 'link', 'fullscreen']
			//});
		});

		function change2cn(en, cninput) {
			cninput.value = getSpell(en, "");
		}

	</script>
</head>

<body class="mainbody">
	<form id="form1" runat="server">
		<!--导航栏-->
		<div class="location">
			<a href="category_list.aspx" class="back"><i></i><span>返回列表页</span></a>
			<a href="../center.aspx" class="home"><i></i><span>首页</span></a>
			<i class="arrow"></i>
			<a href="category_list.aspx"><span>内容类别</span></a>
			<i class="arrow"></i>
			<span>编辑分类</span>
		</div>
		<div class="line10"></div>
		<!--/导航栏-->

		<!--内容-->
		<div class="content-tab-wrap">
			<div id="floatHead" class="content-tab">
				<div class="content-tab-ul-wrap">
					<ul>
						<li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
					</ul>
				</div>
			</div>
		</div>

		<div class="tab-content">
			<dl>
				<dt>广告类型</dt>
				<dd>
					<div class="rule-multi-radio">
						<asp:RadioButtonList ID="rblAdType" runat="server" RepeatDirection="Horizontal"
							RepeatLayout="Flow">
							<asp:ListItem Selected="True" Value="1">文字 </asp:ListItem>
							<asp:ListItem Value="2">图片 </asp:ListItem>
							<asp:ListItem Value="3">幻灯片 </asp:ListItem>
							<asp:ListItem Value="4">动画 </asp:ListItem>
							<asp:ListItem Value="5">FLV视频 </asp:ListItem>
							<asp:ListItem Value="6">代码 </asp:ListItem>
						</asp:RadioButtonList>
					</div>
				</dd>
			</dl>
			<dl>
				<dt>广告名称</dt>
				<dd>
					<asp:TextBox ID="txtTitle" runat="server" onBlur="change2cn(this.value, this.form.txtCallIndex)" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
					<span class="Validform_checktip">*类别中文名称，100字符内</span></dd>
			</dl>
			<dl>
				<dt>尺寸</dt>
				<dd>
					<asp:TextBox ID="txtAdWidth" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位的宽度" HintInfo="请填写该广告位的宽度，只能输入正整数，不能小于零。"></asp:TextBox> x <asp:TextBox ID="txtAdHeight" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位的高度" HintInfo="请填写该广告位的高度，只能输入正整数，不能小于零。"></asp:TextBox>
           &nbsp;px
				</dd>
			</dl>
			<dl>
				<dt>广告数量</dt>
				<dd>
					<asp:TextBox ID="txtNum" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
					<span class="Validform_checktip">*数字</span>
				</dd>
			</dl>
			<dl>
				<dt>链接目标</dt>
				<dd>
					<asp:RadioButtonList ID="rblAdTarget" runat="server" 
                 RepeatDirection="Horizontal" RepeatLayout="Flow">
                 <asp:ListItem Selected="True" Value="_blank">新窗口 </asp:ListItem>
                 <asp:ListItem Value="_self">原窗口 </asp:ListItem>
             </asp:RadioButtonList>
				</dd>
			</dl>
			<dl>
				<dt>排序数字</dt>
				<dd>
					<asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
					<span class="Validform_checktip">*数字，越小越向前</span>
				</dd>
			</dl>
			<dl>
				<dt>备注说明</dt>
				<dd>
					<%-- class="editor" style="visibility: hidden;"--%>
					<textarea id="txtContent" runat="server" rows="2" cols="20" class="input"></textarea>
				</dd>
			</dl>
			<%--<dl>
				<dt>链接目标</dt>
				<dd>
					<asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
					<div class="upload-box upload-img"></div>
				</dd>
			</dl>--%>
		</div>
		<!--/内容-->

		<!--工具栏-->
		<div class="page-footer">
			<div class="btn-list">
				<asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
				<input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
			</div>
			<div class="clear"></div>
		</div>
		<!--/工具栏-->

	</form>
</body>
</html>
