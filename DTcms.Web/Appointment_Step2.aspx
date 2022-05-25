<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appointment_Step2.aspx.cs" Inherits="DTcms.Web.Appointment_Step2" %>

<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>
<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
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
    <script src="script/common.js" type="text/javascript"></script>
    <script src="scripts/datepicker/WdatePicker.js"></script>
    <script src="script/custom/Common.js"></script>
    <script>
        //加载运行
        $(function () {
            //完成按钮点击事件
            $("#btnTrue").click(function () {
                if (!$("#txtName").val()) {
                    $.dialogAlert("请输入姓名");
                    return false;
                }
                if (!$("#txtContact").val()) {
                    $.dialogAlert("请输入联系方式");
                    return false;
                }
                if (!$("#txtContent").val()) {
                    $.dialogAlert("请输入服务要求");
                    return false;
                }
                if (!$("#txtCode").val()) {
                    $.dialogAlert("请输入验证码");
                    return false;
                }
                //提交
                $.ajax({
                    url: "ashx/UserAppointment.ashx" + location.search + "&option=Appointment",
                    data: $("form").serialize(),
                    type: "post",
                    dataType: "json",
                    success: function (ret) {
                        if (!ret.status)
                            $.dialogAlert(ret.msg);
                        else {
                            location.href = "Appointment_Step3.aspx" + location.search;
                        }
                    }
                });
            });
        });
    </script>
</head>
<body>
    <uc1:Header runat="server" ID="Header" />
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <!--顶部banner-->
    <div class="w">
        <div class="style_but3">网上预约</div>
        <div class="step_yy" style="background-position: 0px -69px"></div>
        <div class="w_yy">
            <div>
                <form>
                    <div style="font-weight: bold">请填写以下申请表，网上预约成功后，可至本处优先办理相关公证业务。 </div>
                    <table class="tab_01" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="120"><span class="red">*</span> 姓名：</td>
                            <td>
                                <input id="txtName" name="txtName" class="style_test" type="text" placeholder='请填写您的真实姓名'></td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span> 联系方式：</td>
                            <td>
                                <input id="txtContact" name="txtContact" class="style_test" type="text" placeholder='请填写您的有效联系方式'>
                                （请留下您的手机号码，以便我们发送预约通知短信）</td>
                        </tr>
                        <tr>
                            <td><span class="red">*</span> 办理公证事项：</td>
                            <td>
                                <textarea id="txtContent" name="txtContent" class="style_testarea" placeholder="请填写您的办理公证事项"></textarea></td>
                        </tr>

                        <tr>
                            <td><span class="red">*</span> 验证码：</td>
                            <td>
                                <input name="txtCode" id="txtCode" class="style_test2" style="background: url(images/ico_u5.png) no-repeat 10px center #ffffff" type="text" placeholder='请输入验证码'>
                                <img style="cursor: pointer" align="absmiddle" src="tools/verify_code.ashx" width="100" height="42" onclick="this.src='tools/verify_code.ashx?'+Math.random()">
                                看不清，点击图片切换
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><span class="k">
                                <input class="style_but2" type="button" value="提交" id="btnTrue">
                            </span><span class="k">
                                <input class="style_but2" type="reset" value="重置">
                            </span></td>
                        </tr>
                    </table>
                    <div class="showtxt">
                        <p style="font-weight: bold;">1、预约成功后，将收到短信提示，请保留通知短信并记下您的预约号，到本处办证的当日凭短信通知和预约号和由本处优先安排公证员接待，减少排队等待时间；  </p>
                        <p style="font-weight: bold;">2、优先接待并不表示该公证事项当天一定可以受理，如果当事人提交的材料不符合办证要求，则需要补充材料方可受理；  </p>
                        <p style="font-weight: bold;">3、若当事人预约的公证事项不符合法律规定或者不属于本公证处管辖范围，本处可依法不予受理；</p>
                        <p style="font-weight: bold;">4、对于办理继承权、赠与、保全证据、合同等复杂公证事项，由于所需材料较多，请当事人先进行电话咨询或者到本处现场咨询。</p>
                    </div>
                </form>
            </div>

        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
