<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_code.aspx.cs" Inherits="DTcms.Web.admin.ad.ad_code" ValidateRequest="false" %>

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
				<dt>广告代码</dt>
				<dd>
					<asp:TextBox runat="server" ID="txtCode" CssClass="input normal" TextMode="MultiLine"></asp:TextBox>
				</dd>
			</dl>
			<dl>
				<dt>广告预览</dt>
				<dd>
					<asp:Literal runat="server" ID="ltView"></asp:Literal></dd>
			</dl>
		</div>
		<!--/内容-->

		<!--工具栏-->
		<div class="page-footer">
			<div class="btn-list">
				<input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
			</div>
			<div class="clear"></div>
		</div>
		<!--/工具栏-->

	</form>
</body>
</html>
