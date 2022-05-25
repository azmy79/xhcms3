<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scheduling.aspx.cs" Inherits="DTcms.Web.admin.Appointment.Scheduling" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>公证员排班</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../script/easydialog.css" rel="stylesheet" />
    <script src="../../scripts/jquery-ui/jquery-ui.min.js"></script>
    <script src="../../script/easydialog.js"></script>
    <script>
        $(function () {
            $("#tabs").tabs();
            $("input:checkbox").click(function () {
                if (this.checked) $(this).parent().addClass("checked");
                else $(this).parent().removeClass("checked");
            });
        });

        //保存数据
        var SetValue = function () {
            var dataArr = [];
            $("input:checkbox:checked").each(function (i, obj) {
                dataArr.push({
                    Day: $(obj).attr("tag-day"),
                    MonthType: $(obj).attr("tag-monthtype"),
                    ManagerID: obj.value
                });
            });
            $("#hidData").val(JSON.stringify(dataArr));
        };

        //全选/全不选
        var CheckCBK = function (checked) {
            $("input:checkbox").each(function (i, obj) {
                obj.checked = !!checked;
                if (checked) $(obj).parent().addClass("checked");
                else $(obj).parent().removeClass("checked");
            })
        };
    </script>
    <style>
        body {
            font-size: 62.5%;
        }
        .checked {
            background-color: #99CCFF;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hidData" />
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>公证员排班</span>
        </div>
        <!--/导航栏-->
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li>
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click" OnClientClick="return SetValue();"><i></i><span>保存</span></asp:LinkButton>
                        </li>
                        <li>
                            <a class="all" href="javascript:;" onclick="CheckCBK(true);"><i></i><span>全选</span></a>
                        </li>
                        <li>
                            <a class="all" href="javascript:;" onclick="CheckCBK();"><i></i><span>全不选</span></a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">本月</a></li>
                <li><a href="#tabs-2">次月</a></li>
            </ul>
            <div id="tabs-1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>
                            <th width="5%">日期</th>
                            <th align="left" width="95%">公证员</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            for (int i = 1; i < 32; i++)
                            {
                                Response.Write("<tr>");
                                Response.Write("    <td align=\"center\">" + i + "</td>");
                                Response.Write("    <td>");
                                ManagerList.ForEach(p =>
                                {
                                    var isBind = SchedulingList.Find(q => q.MonthType == CurrentMonthType && q.ManagerID == p.id && q.Day == i) != null;
                                    Response.Write("        <label " + (isBind ? "class=\"checked\"" : string.Empty) + " style=\"margin: 10px; cursor: pointer; display: inline-block\">");
                                    Response.Write("            <input tag-monthtype=\"" + CurrentMonthType + "\" tag-day=\"" + i + "\" type=\"checkbox\" value=\"" + p.id + "\" " + (isBind ? "checked=\"checked\"" : string.Empty) + " name=\"cbkType1\">" + p.real_name);
                                    Response.Write("        </label>");
                                });
                                Response.Write("    </td>");
                                Response.Write("</tr>");
                            }
                        %>
                    </tbody>
                </table>
            </div>
            <div id="tabs-2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>
                            <th width="5%">日期</th>
                            <th align="left" width="95%">公证员</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            for (int i = 1; i < 32; i++)
                            {
                                Response.Write("<tr>");
                                Response.Write("    <td align=\"center\">" + i + "</td>");
                                Response.Write("    <td>");
                                ManagerList.ForEach(p =>
                                {
                                    var isBind = SchedulingList.Find(q => q.MonthType == 1 - CurrentMonthType && q.ManagerID == p.id && q.Day == i) != null;
                                    Response.Write("        <label " + (isBind ? "class=\"checked\"" : string.Empty) + " style=\"margin: 10px; cursor: pointer; display: inline-block\">");
                                    Response.Write("            <input tag-monthtype=\"" + (1 - CurrentMonthType) + "\" tag-day=\"" + i + "\"  type=\"checkbox\" value=\"" + p.id + "\" " + (isBind ? "checked=\"checked\"" : string.Empty) + " name=\"cbkType2\">" + p.real_name);
                                    Response.Write("        </label>");
                                });
                                Response.Write("    </td>");
                                Response.Write("</tr>");
                            }
                        %>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
