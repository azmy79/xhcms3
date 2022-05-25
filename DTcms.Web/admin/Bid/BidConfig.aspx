<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidConfig.aspx.cs" Inherits="DTcms.Web.admin.Bid.BidConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申办配置</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
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
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>申办配置</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">值班公证员</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <!--值班公证员信息-->
        <div class="tab-content">
            <dl>
                <dt>姓名</dt>
                <dd>
                    <asp:TextBox ID="txtJusticeName" runat="server" CssClass="input normal" datatype="*2-255" sucmsg=" " />
                    <span class="Validform_checktip">*任意字符，控制在255个字符内</span>
                </dd>
            </dl>
            <dl>
                <dt>手机号码</dt>
                <dd>
                    <asp:TextBox ID="txtJusticeTel" runat="server" CssClass="input normal" datatype="m" sucmsg=" " MaxLength="11"/>
                    <span class="Validform_checktip">*接收公证短信提醒的手机号码</span>
                </dd>
            </dl>
        </div>
        <!--/值班公证员信息-->

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
