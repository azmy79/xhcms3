<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appointment_Step1.aspx.cs" Inherits="DTcms.Web.Appointment_Step1" %>

<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>

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
    <style>
        .time-choice {
            width: 204px;
            margin: 10px auto 0px;
        }

            .time-choice .t {
                font-size: 14px;
                font-weight: bold;
                color: #900;
                text-align: center;
                padding-bottom: 8px;
            }

            .time-choice input {
                width: 190px;
                height: 35px;
                line-height: 35px;
                border: #ef7000 5px solid;
                padding-left: 6px;
            }

        .radio_01 {
            -moz-border-radius: 15px; /* Gecko browsers */
            -webkit-border-radius: 15px; /* Webkit browsers */
            border-radius: 15px; /* W3C syntax */
        }

            .radio_01 img {
                -moz-border-radius: 7.5px; /* Gecko browsers */
                -webkit-border-radius: 7.5px; /* Webkit browsers */
                border-radius: 7.5px; /* W3C syntax */
            }
    </style>
    <script>

        var ID;
        function myFun(obj) {
            ID = $(obj).attr("tagdata");
            $.each($(".radio_01"), function (i, obj) {
                obj.style.border = '5px solid #cccccc';
            });
            obj.style.border = '10px solid #ef7000';
        }

        $(function () {
            $("#txtTime").focus(function () {
                WdatePicker({
                    minDate: '%y-%M-%d',
                    maxDate: '%y-{%M+1}-%ld',
                    dateFmt: 'yyyy-MM-dd',
                    onpicked: BindData,
                    disabledDays: [0,6]
                });
            }).val(new Date().ToString("yyyy-MM-dd"));

            //绑定公证员
            var BindData = function () {
                $.ajax({
                    url: "ashx/UserAppointment.ashx?option=GetNotary",
                    type: "GET",
                    dataType: "json",
                    data: {
                        Time: $("#txtTime").val()
                    },
                    success: function (ret) {
                        ID = "";
                        var _html = [];
                        $.each(ret, function (i, obj) {
                            if (i % 5 == 0)
                                _html.push("<tr>");
                            _html.push("<td width=\"20%\" align=\"center\">");
                            _html.push("    <div class=\"num_gzy\">");
                            _html.push("        <span class=\"radio_01\" onclick=\"myFun(this)\" tagdata=\"" + obj.ID + "\">");
                            _html.push("            <img width=\"200px\" height=\"250px\" alt=\"" + obj.Name + "\" src=\"" + obj.HeadImg + "\"></span>");
                            _html.push("        <p>" + obj.Name + "</p>");
                            _html.push("        <p>编号：" + obj.Number + "&nbsp;&nbsp;已预约：" + obj.AptCount + "人</p>");
                            _html.push("    </div>");
                            _html.push("</td>");
                            if (i % 5 == 4 || i == ret.length - 1)
                                _html.push("</tr>");
                        });
                        $("table tbody").html(_html.join(""));
                    }
                });
            };

            BindData();

            $(".style_but2").click(function () {
                if (ID) location.href = "Appointment_Step2.aspx?cid=" + $.GetQueryString("cid") + "&id=" + ID + "&time=" + $("#txtTime").val();
                else
                    $.dialogAlert("请选择公证员");
            });
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
        <div class="style_but3">网上预约</div>
        <div class="step_yy" style="background-position: 0px 0px"></div>
        <div class="time-choice">
            <div class="t">请先选择预约时间</div>
            <input type="text" id="txtTime" placeholder='选择时间'>
        </div>

        <div class="w_yy">
            <div>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tbody></tbody>
                    <tfoot>
                        <tr>
                            <td colspan="5" align="center">
                                <input class="style_but2" type="button" value="下一步"></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
</body>
</html>
