<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bid_Step2.aspx.cs" Inherits="DTcms.Web.Bid_Step2" %>

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
    <script src="scripts/datepicker/WdatePicker.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script>
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!/^[\u4e00-\u9fa5]+$/.test($("#txtCnName").val())) {
                    $.dialogAlert("申请人姓名能为空且必须为中文");
                    return false;
                }
                if (!$("#txtTel").val()) {
                    $.dialogAlert("请输入手机号码");
                    return false;
                } else if (!/^[0-9]{11}$/.test($("#txtTel").val())) {
                    $.dialogAlert("手机号码格式输入不正确");
                    return false;
                }
                if (!$("#txtBirthday").val()) {
                    $.dialogAlert("请输入出生日期");
                    return false;
                }
                if (!$("#txtCartNum").val()) {
                    $.dialogAlert("请输入证件号码");
                    return false;
                }
                if (!$("#txtAddress").val()) {
                    $.dialogAlert("请输入现住地址");
                    return false;
                }
                var BidData = eval("(" + $.cookie("BidData") + ")");
                $.extend(BidData, {
                    CnName: $("#txtCnName").val(),
                    EnName: $("#txtEnName").val(),
                    Sex: $("input:radio:checked").val(),
                    Tel: $("#txtTel").val(),
                    Address: $("#txtAddress").val(),
                    Birthday: $("#txtBirthday").val(),
                    CartType: $("input:radio[name=radCartType]:checked").val(),
                    CartNum: $("#txtCartNum").val()
                });
                //装载
                $.cookie("BidData", JSON.stringify(BidData), {
                    path: "/"
                });
                location.href = "Bid_Step2_2.aspx" + location.search;
            });

            //样式预览弹框隐藏
            $(".yuyue_box .bg").click(function () {
                $(".yuyue_box").hide();
            });
            //样式预览弹框显示
            $("#tabBidBusiness .tc").click(function () {
                $(".yuyue_box").show().find("img").attr("src", $(this).attr("tagsrc"));
            });

            //绑定数据
            //数据绑定
            if ($.cookie("BidData")) {
                //申办数据
                var bidData = eval("(" + $.cookie("BidData") + ")");
                if (bidData.CnName) {
                    $("#txtCnName").val(bidData.CnName);
                    $("#txtEnName").val(bidData.EnName);
                    $("input:radio[name=radSex][value=" + bidData.Sex + "]").click();
                    $("#txtTel").val(bidData.Tel);
                    $("#txtAddress").val(bidData.Address);
                    $("#txtBirthday").val(bidData.Birthday);
                    $("input:radio[name=radCartType][value=" + bidData.CartType + "]").click();
                    $("#txtCartNum").val(bidData.CartNum);
                }
            }

        });
    </script>
</head>
<body>
    <!--综合头部开始-->
    <uc1:Header runat="server" ID="Header" />
    <!--综合头部结束-->
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <!--顶部banner-->
    <div class="w">
        <div class="style_but3">在线申办流程指南</div>
        <div class="step_bz" style="background-position: 0px -69px"></div>
        <div class="w_bz">
            <div class="title">
                <h3 class="f_l">公证费用信息</h3>
                <div class="f_r">总费用：<em id="totalPrice"></em> 元人民币。</div>
            </div>
            <div class="c">
                <table id="tabBidBusiness" class="tab_02" cellpadding="0" cellspacing="1" width="100%" bgcolor="#CCCCCC">
                    <thead>
                        <tr style="background: #f9f9f9">
                            <td width="20%"></td>
                            <td width="20%">
                                <label>公证费用</label></td>
                            <td width="20%">
                                <label>公证证词翻译费用</label></td>
                            <td width="20%">
                                <label>副本费用</label></td>
                            <td width="20%">
                                <label>证书样式</label></td>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            var country = Country.Find(p => p.ID == Convert.ToInt32(BidData["Country"]));//前往国家
                            var copyCount = Convert.ToInt32(BidData["CopyCount"]);//副本数量
                            var tRLanguageArr = (BidData["TRLanguage"] as System.Collections.ArrayList).ToArray();
                            var tRLanguage = TRLanguage.FindAll(p => tRLanguageArr.Contains(p.ID.ToString()));//翻译语言
                            var bidBusinessObjArry = (BidData["BidBusiness"] as System.Collections.ArrayList).ToArray();
                            var bidBusiness = BidBusiness.FindAll(p => bidBusinessObjArry.Select(q => (q as Dictionary<string, object>)["BidBusiness"]).Contains(p.ID.ToString()));//申办业务
                            var certificateStyle = CertificateStyle.FindAll(p => bidBusinessObjArry.Select(q => (q as Dictionary<string, object>)["CertificateStyle"]).Contains(p.ID.ToString()));//证书样式
                            decimal totalPrice = 0;//总价格
                            Response.Write(WriteBidTable(country, copyCount, tRLanguage, bidBusiness, out totalPrice, certificateStyle));
                        %>
                        <tr>
                            <td style="background: #f9f9f9">
                                <label>所需公证文本翻译费用</label></td>
                            <td class="red" colspan="4">该项目根据实际需要由公证员跟您沟通后确认</td>
                        </tr>
                        <tr>
                            <td style="background: #f9f9f9">
                                <label>说明</label></td>
                            <td class="red" colspan="4">上述费用为预收费用，若与实际产生的费用不一致，将实行多退少补，在申请人领取公证书时办理补费或退费手续，取证时请在领证窗口领取相关费用正式票据。</td>
                        </tr>
                    </tbody>
                </table>
                <script>
                    $("#totalPrice").text("<%=Convert.ToInt32(totalPrice)%>");
                </script>
            </div>
            <div class="title">
                <h3 class="f_l">翻译语言</h3>
            </div>
            <div class="c">
                <%
                    for (int i = 0; i < tRLanguage.Count; i++)
                    {
                        Response.Write("<span class=\"k\">" + tRLanguage[i].Name + "</span>");
                    }
                %>
            </div>
            <div class="title">
                <h3 class="f_l">其他申办信息</h3>
            </div>
            <div class="c">
                <span class="k">国家：<%=country.Name %></span>
                <span class="k">用途：<%=Purpose.Find(p=>p.ID==Convert.ToInt32(BidData["Purpose"])).Name %></span>
            </div>
            <div class="title">
                <h3 class="f_l">基本个人信息</h3>
            </div>
            <div class="c">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="100"><span class="red">*</span> 申请人姓名：</td>
                        <td>
                            <input value="<%=UserExt.CnName %>" class="style_test" id="txtCnName" type="text" placeholder='请填写申办人的真实姓名'></td>
                    </tr>
                    <tr>
                        <td>外文名：</td>
                        <td>
                            <input class="style_test" value="<%=UserExt.EnName %>" id="txtEnName" type="text" maxlength="20" placeholder='不能超过20个字符，英文'></td>
                    </tr>
                    <tr>
                        <td><span class="red">*</span> 性别：</td>
                        <td><span class="k">
                            <input name="radSex" type="radio" value="男" <%=UserInfo.sex=="男"?"checked=\"checked\"":string.Empty %>>
                            男</span><span class="k">
                                <input name="radSex" type="radio" value="女" <%=UserInfo.sex=="女"?"checked=\"checked\"":string.Empty %>>
                                女</span></td>
                    </tr>
                    <tr>
                        <td><span class="red">*</span>出生日期：</td>
                        <td>
                            <input class="style_test" value="<%=UserInfo.birthday %>" id="txtBirthday" type="text" maxlength="20" placeholder='不能超过20个字符，英文' onfocus="WdatePicker()"></td>
                    </tr>
                    <tr>
                        <td width="100">证件类型：</td>
                        <td>
                            <%
                                foreach (var item in Enum.GetValues(typeof(DTcms.Common.DTEnums.证件类型)))
                                {
                                    var enumObj = (DTcms.Common.DTEnums.证件类型)item;
                                    Response.Write("<span class=\"k\">");
                                    Response.Write("    <input " + (enumObj.GetHashCode() == UserExt.CartType ? "checked=\"checked\"" : string.Empty) + " name=\"radCartType\" type=\"radio\" value=\"" + enumObj.GetHashCode() + "\"/>");
                                    Response.Write("    " + enumObj.ToString() + "</span>");
                                }
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td width="100"><span class="red">*</span>证件号码：</td>
                        <td>
                            <input class="style_test" value="<%=UserExt.CartNum %>" id="txtCartNum" type="text" placeholder='证件号码'></td>
                    </tr>
                    <tr>
                        <td width="100"><span class="red">*</span> 手机号码：</td>
                        <td>
                            <input class="style_test" value="<%=UserInfo.mobile %>" id="txtTel" type="text" placeholder='11位数字' maxlength="11"></td>
                    </tr>

                    <tr>
                        <td width="100"><span class="red">*</span> 现住地址：</td>
                        <td>
                            <input class="style_test" value="<%=UserInfo.address %>" id="txtAddress" type="text" placeholder='真实的现居住地'></td>
                    </tr>
                </table>
            </div>
            <input class="style_but2" type="button" id="btnTrue" value="下一步">
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
    <div class="yuyue_box" style="display: none;">
        <div class="bg"></div>
        <div class="yuyue_con">
            <img src="">
        </div>
    </div>

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
