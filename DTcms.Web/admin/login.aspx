<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DTcms.Web.admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>新网网站信息管理系统</title>
	<link href="css2/master.css" rel="stylesheet" type="text/css" />
	<link href="css2/index.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript">
		$(function () {
			//检测IE
			if ('undefined' == typeof (document.body.style.maxHeight)) {
				window.location.href = 'ie6update.html';
			}
		});
	</script>
</head>
<body>
	<form id="login_form" runat="server">
		<div id="demo_top"></div>
		<div id="demo_head">
			<div id="demo_home" class="fB"><a href="http://www.whnewnet.com" target="_blank">&nbsp;新网首页</a> </div>
		</div>
		<div class="h210px"></div>
		<div id="demo_con">
			<div class="demo_con_l">
				<div class="logo">
					<img src="images2/logo.gif" width="148" height="34" alt="" />
				</div>
				<div class="h20px"></div>
				<div class="h10px"></div>
				<div class="logo">
					<img src="images2/glxt_icon.jpg" width="144" height="34" alt="" />
				</div>
			</div>
			<div class="demo_con_r">
				<div class="demo_user">
					<div class="input_font">管理员帐号：</div>
					<div class="input_bd">
						<div class="input_l"></div>
						<asp:TextBox ID="txtUserName" class="bd" runat="server"></asp:TextBox><div class="input_r"></div>
					</div>
				</div>
				<div class="h20px"></div>
				<div class="demo_user">
					<div class="input_font">管理密码：</div>
					<div class="input_bd">
						<div class="input_l"></div>
						<asp:TextBox ID="txtPassword" class="bd" runat="server" TextMode="Password"></asp:TextBox><div class="input_r"></div>
					</div>
				</div>
				<div class="h5px"></div>
				<div class="tis">
					<span>
						<img src="images2/dp.gif" width="11" height="13" alt="" /></span>&nbsp;提示：<asp:Label ID="msgtip" runat="server" Text="登录失败3次，需关闭后才能重新登录"></asp:Label>
				</div>
				<div class="h5px"></div>
				<div class="Submit">
					<asp:Button ID="btnSubmit" runat="server" Text="" class="btn02"
						onfocus="this.blur()" onmouseover="this.className='btn04'"
						onmouseout="this.className='btn02'" OnClick="btnSubmit_Click" />
				</div>
			</div>
		</div>
		<div id="demo_bq">武汉新网科技发展有限公司 版权所有：武汉新网 版本号：BETA1.0</div>
	</form>

</body>
</html>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>管理员登录</title>
	<link href="skin/default/style.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
	<script type="text/javascript">
		$(function () {
			//检测IE
			if ('undefined' == typeof (document.body.style.maxHeight)) {
				window.location.href = 'ie6update.html';
			}
		});
	</script>
</head>

<body class="loginbody">
	<form id="form1" runat="server">
		<div class="login-screen">
			<div class="login-icon">LOGO</div>
			<div class="login-form">
				<h1>系统管理登录</h1>
				<div class="control-group">
					<asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" placeholder="用户名" title="用户名"></asp:TextBox>
					<label class="login-field-icon user" for="txtUserName"></label>
				</div>
				<div class="control-group">
					<asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="密码" title="密码"></asp:TextBox>
					<label class="login-field-icon pwd" for="txtPassword"></label>
				</div>
				<div>
					<asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" OnClick="btnSubmit_Click" /></div>
				<span class="login-tips"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span>
			</div>
			<i class="arrow">箭头</i>
		</div>
	</form>
</body>
</html>--%>
