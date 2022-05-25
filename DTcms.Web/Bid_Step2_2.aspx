<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bid_Step2_2.aspx.cs" Inherits="DTcms.Web.Bid_Step2_2" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/doc.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <link href="css/swfupload_cus.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/jquery.cookie.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.handlers.js"></script>
    <script>
        $(function () {
            //初始化上传控件
            $(".upload-img").each(function () {
                var $this = $(this);
                //初始化批量上传
                $this.InitSWFUpload({
                    btntext: "上传",
                    btnwidth: 66,
                    filesize: "10240",
                    sendurl: "tools/upload_ajax.ashx?filetype=documentfile",
                    flashurl: "scripts/swfupload/swfupload.swf",
                    filetypes: "*.zip;*.rar;",
                    uploadSuccess: function (file, serverData) {
                        var serverData = eval("(" + serverData + ")");//返回值
                        $this.next(".pshow").html("<a href=\"javascript:;\" onclick=\"$(this).parent().empty();\">[删除]</a> <strong class=\"file\" style=\"color:green;\" tagsrc=\"" + serverData.path + "\">" + serverData.path.substring(serverData.path.lastIndexOf("/") + 1) + "</strong>");
                    }
                });
            });

            //保存
            $("#btnTrue").click(function () {
                var Document = [];
                var ret = true;
                $.each($(".upload-box"), function (i, obj) {
                    if (!$(obj).next(".pshow").html()) {
                        $.dialogAlert("请" + $(obj).parents("tr").find("td").eq(0).text().trim().replace("：", ""));
                        ret = false;
                        return false;
                    }
                    Document.push({
                        DocumentTypeID: $(obj).attr("tagdata"),
                        Path: $(obj).next(".pshow").find(".file").attr("tagsrc")
                    });
                });
                if (!ret) return false;
                var BidData = eval("(" + $.cookie("BidData") + ")");
                $.extend(BidData, {
                    Document: Document
                });
                //装载
                $.cookie("BidData", JSON.stringify(BidData), {
                    path: "/"
                });
                return true;
            });

            //绑定数据
            //数据绑定
            if ($.cookie("BidData")) {
                //申办数据
                var bidData = eval("(" + $.cookie("BidData") + ")");
                if (bidData.Document) {
                    $.each(bidData.Document, function (i, obj) {
                        $("div[tagdata=" + obj.DocumentTypeID + "]").next(".pshow").html("<a href=\"javascript:;\" onclick=\"$(this).parent().empty();\">[删除]</a> <strong class=\"file\" style=\"color:green;\" tagsrc=\"" + obj.Path + "\">" + obj.Path.substring(obj.Path.lastIndexOf("/") + 1) + "</strong>");
                    });
                }
            }
        });
    </script>
</head>
<body>
    <form runat="server">
        <!--综合头部开始-->
        <uc1:Header runat="server" ID="Header" />
        <!--综合头部结束-->
        <uc1:DocBanner runat="server" ID="DocBanner" />
        <!--顶部banner-->
        <div class="w">
            <div class="style_but3">在线申办流程指南</div>
            <div class="step_bz" style="background-position: 0px -138px"></div>
            <div class="w_bz">
                <div class="title">
                    <h3 class="f_l">上传相关资料</h3>
                </div>
                <div class="c">
                    <table class="tab_02" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2"><span class="k red">1、此处仅允许上传zip和rar格式的文件，为便于公证书制作，请将清晰不影响阅读的扫描文件压缩后再上传，文件大小小于10M。
                                <br />
                                2、如需重新上传文件，请务必先删除原来已经上传的文件。
                            </span></td>
                        </tr>
                        <%
                            DocumentType.ForEach(p =>
                            {
                                Response.Write("<tr>");
                                Response.Write("    <td align=\"left\" width=\"180\">上传" + p.Name + "：</td>");
                                Response.Write("    <td>");
                                Response.Write("        <div tagdata=\"" + p.ID + "\" class=\"upload-box upload-img\"></div>");
                                Response.Write("        <div class=\"pshow\"></div>");
                                Response.Write("</tr>");
                            });
                        %>
                    </table>
                </div>
                <asp:Button ID="btnTrue" CssClass="style_but2" runat="server" Text="确认已上传资料" OnClick="btnTrue_Click" />
            </div>
            <div class="clear"></div>
        </div>
        <uc1:Footer runat="server" ID="Footer" />
    </form>
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>

    <!--浮动框 -->
    <ul class="float-rig">
        <li class="l4 amn3"><b class="amn2 b4 png"></b></li>
        <li class="l5"><b class="amn2 b5 png"><a class="yuyue" style="width: 56px; height: 56px; display: block;"
            href="Single.aspx?cid=82" target="_blank"></a></b></li>
    </ul>
    <script>
        $(function () {

            $(".float-rig li").hover(function () {
                $(this).addClass("h");
            }, function () {
                $(this).removeClass("h");
            })
            $(".float-rig .b4").click(function () {
                $('body,html').animate({ scrollTop: 0 }, 500);
            })
        })

        $(window).scroll(function (e) {
            lk = location.pathname;
            if ($(this).scrollTop() >= 200) {
                $(".l4").fadeIn();

            } else {
                $(".l4").fadeOut();

            }

        });
    </script>
    <!--浮动框End -->
</body>
</html>
