<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateStyleEdit.aspx.cs" Inherits="DTcms.Web.admin.Bid.CertificateStyleEdit" %>

<!DOCTYPE>
<html>
<head>
    <title><%=IsEdit? "修改" : "新增"%>证书样式</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../script/easydialog.css" rel="stylesheet" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script src="../../scripts/swfupload/swfupload.js"></script>
    <script src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化批量上传
            $("#divUploadImg").InitSWFUpload({
                btntext: "上传",
                btnwidth: 66,
                filesize: "10240",
                sendurl: "../../tools/upload_ajax.ashx",
                flashurl: "../../scripts/swfupload/swfupload.swf",
                filetypes: "*.jpg;*.jpge;*.png;*.gif;",
                uploadSuccess: function (file, serverData) {
                    var serverData = eval("(" + serverData + ")");//返回值
                    $("#hidImgUrl").val(serverData.path);
                    $("#divPanoramicPicture").html("<img width=\"150px\" height=\"200px\" src=\"" + serverData.path + "\" /><input type=\"button\" value=\"删除\" onclick=\"$('#divPanoramicPicture').empty();hidImgUrl.value='';\" />");
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
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
                <dt>证书样式名称</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span></dd>
            </dl>
            <dl>
                <dt>证书样式</dt>
                <dd>
                    <input type="hidden" id="hidImgUrl" runat="server" datatype="*" nullmsg="请上传证书样式" />
                    <div class="upload-box upload-album" id="divUploadImg"></div>
                    <div id="divPanoramicPicture" runat="server">
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>排序</dt>
                <dd>
                    <asp:TextBox ID="txtSort" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                    <span class="Validform_checktip">*数字，越小越向前</span>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回" class="btn yellow" onclick="history.go(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
