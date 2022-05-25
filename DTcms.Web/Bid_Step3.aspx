<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bid_Step3.aspx.cs" Inherits="DTcms.Web.Bid_Step3" %>

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
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/jquery.cookie.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("#btnTrue").click(function () {
                if (!$("#cbkAgree")[0].checked) {
                    $.dialogAlert("请先同意《受理告知协议》");
                    return false;
                }
                if (!$("#radChinapay")[0].checked) {
                    $.dialogAlert("请选择支付方式");
                    return false;
                }
                return true;
            });
            //样式预览弹框隐藏
            $(".yuyue_box .bg").click(function () {
                $(".yuyue_box").hide();
            });
            //样式预览弹框显示
            $("#tabBidBusiness .tc").click(function () {
                $(".yuyue_box").show().find("img").attr("src", $(this).attr("tagsrc"));
            });
        });
    </script>
</head>
<body>
    <form runat="server">
        <uc1:Header runat="server" ID="Header" />
        <uc1:DocBanner runat="server" ID="DocBanner" />
        <!--顶部banner-->
        <div class="w">
            <div class="style_but3">在线申办流程指南</div>
            <div class="step_bz" style="background-position: 0px -207px"></div>
            <div class="w_bz">
                <div class="title">
                    <h3 class="f_l">在线支付：</h3>
                    <div class="f_r">尊敬的用户，您的支付费用为：<em id="totalPrice"><%=Convert.ToInt32(Bid.Price) %></em> 元人民币。</div>
                </div>
                <div class="c">
                    <table id="tabBidBusiness" class="tab_02" cellpadding="0" cellspacing="1" width="100%" bgcolor="#CCCCCC">
                        <thead>
                            <tr style="background: #f9f9f9">
                                <td width="25%"></td>
                                <td width="25%">
                                    <label>公证费用</label></td>
                                <td width="25%">
                                    <label>公证证词翻译费用</label></td>
                                <td width="25%">
                                    <label>副本费用</label></td>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                var copyCount = Bid.CopyCount;//副本数量
                                var country = new DTcms.BLL.Country().GetModel(Bid.CountryID);//前往国家
                                Response.Write(WriteBidTable(country, copyCount, TRLanguage, BidBusiness));
                            %>
                            <tr>
                                <td style="background: #f9f9f9">
                                    <label>所需公证文本翻译费用</label></td>
                                <td colspan="4">
                                    ￥<%
                                        var BidSourceFileList = new DTcms.BLL.BidSourceFile().GetModelList("BidID=" + Bid.ID);
                                        if (BidSourceFileList.Count > 0)
                                            Response.Write(Convert.ToInt32(BidSourceFileList[0].TranslationPrice));
                                        else
                                            Response.Write(0);
                                    %></td>
                            </tr>
                            <tr>
                                <td style="background: #f9f9f9">
                                    <label>说明</label></td>
                                <td class="red" colspan="4">上述费用为预收费用，若与实际产生的费用不一致，将实行多退少补，在申请人领取公证书时办理补费或退费手续，取证时请在领证窗口领取相关费用正式票据。</td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="air"></div>
                    <div>
                        <input name="cbkAgree" id="cbkAgree" type="checkbox">
                        同意 <a class="blue" target="_blank" href="Single.aspx?cid=76">《受理告知协议》</a>
                    </div>
                    <div class="pay_change">
                        <p>选择支付方式：目前支持银联支付</p>
                        <div class="s">
                            <input name="radChinapay" type="radio" id="radChinapay">
                            <img src="images/Chinapay.png">
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
                <asp:Button ID="btnTrue" runat="server" Text="立即支付" CssClass="style_but2" OnClick="btnTrue_Click" />

            </div>
            <div class="clear"></div>
        </div>
        <uc1:Footer runat="server" ID="Footer" />
        <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
    </form>
    <div class="yuyue_box" style="display: none;">
        <div class="bg"></div>
        <div class="yuyue_con">
            <img src="">
        </div>
    </div>
</body>
</html>
