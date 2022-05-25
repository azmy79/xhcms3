<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidBusinessList.aspx.cs" Inherits="DTcms.Web.admin.Bid.BidBusinessList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>申办业务管理</title>
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
        //加载运行
        $(function () {
            //初始化
            $dialogSetTRLanguagePrice = $("#dialog-SetTRLanguagePrice").dialog({
                autoOpen: false,
                modal: true,
                height: 400,
                width: 600,
                title: "设置翻译价格",
                show: {
                    effect: "clip",
                    duration: 100
                },
                hide: {
                    effect: "clip",
                    duration: 100
                },
                buttons:
                    [{
                        text: "确定",
                        click: function () {
                            //提交
                            $.ajax({
                                url: "/ashx/BidBusiness.ashx?option=BindTRLanguage&ID=" + ID,
                                data: $dialogSetTRLanguagePrice.serialize(),
                                type: "post",
                                dataType: "json",
                                success: function (ret) {
                                    if (!ret.status)
                                        $.dialogAlert(ret.msg);
                                    else {
                                        $.dialogAlert("设置成功", "提示信息", function () {
                                            $dialogSetTRLanguagePrice.dialog("close");
                                        });
                                    }
                                }
                            });
                        }
                    }, {
                        text: "取消",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }],
                open: function () {
                    var $tbody = $(this).find("tbody").empty();
                    //提交
                    $.ajax({
                        url: "/ashx/BidBusiness.ashx?option=GetTRLanguage&ID=" + ID,
                        type: "GET",
                        dataType: "json",
                        success: function (ret) {
                            var _html = [];
                            $.each(ret, function (i, obj) {
                                _html.push("<tr>");
                                _html.push("    <td align=\"center\">" + obj.TRLanguageName + "</td>");
                                _html.push("    <td>");
                                _html.push("        <input type=\"hidden\" name=\"hidID\" value=\"" + obj.TRLanguageID + "\" />");
                                _html.push("        <input type=\"text\" name=\"TRPrice\" value=\"" + obj.TRPrice + "\" class=\"sort\" onkeydown=\"return checkNumber(event);\" />");
                                _html.push("    </td>");
                                _html.push("</tr>");
                            });
                            $tbody.html(_html.join(""));
                        }
                    });
                }
            });

            //初始化
            $dialogSetDocumentType = $("#dialog-SetDocumentType").dialog({
                autoOpen: false,
                modal: true,
                height: 400,
                width: 600,
                title: "关联证件类型",
                show: {
                    effect: "clip",
                    duration: 100
                },
                hide: {
                    effect: "clip",
                    duration: 100
                },
                buttons:
                    [{
                        text: "确定",
                        click: function () {
                            //提交
                            $.ajax({
                                url: "/ashx/BidBusiness.ashx?option=BindDocumentType&ID=" + ID,
                                data: $dialogSetDocumentType.serialize(),
                                type: "post",
                                dataType: "json",
                                success: function (ret) {
                                    if (!ret.status)
                                        $.dialogAlert(ret.msg);
                                    else {
                                        $.dialogAlert("设置成功", "提示信息", function () {
                                            $dialogSetDocumentType.dialog("close");
                                        });
                                    }
                                }
                            });
                        }
                    }, {
                        text: "取消",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }],
                open: function () {
                    //提交
                    $.ajax({
                        url: "/ashx/BidBusiness.ashx?option=GetDocumentType&ID=" + ID,
                        type: "GET",
                        dataType: "json",
                        success: function (ret) {
                            var _html = [];
                            $.each(ret.List, function (i, obj) {
                                _html.push("<label style=\"margin:10px; cursor:pointer; display:inline-block\">");
                                var isBind = false;
                                $.each(ret.BindList, function (j, o) {
                                    if (o.DocumentTypeID == obj.ID)
                                        isBind = true;
                                });
                                _html.push("    <input name=\"cbkDocumentType\" " + (isBind ? "checked=\"checked\"" : "") + " value=\"" + obj.ID + "\"  type=\"checkbox\"/>" + obj.Name + "</label>");
                            });
                            $dialogSetDocumentType.html(_html.join(""));
                        }
                    });
                }
            });

            //初始化
            $dialogSetCertificateStyle = $("#dialog-SetCertificateStyle").dialog({
                autoOpen: false,
                modal: true,
                height: 580,
                width: 800,
                title: "设置证书样式",
                show: {
                    effect: "clip",
                    duration: 100
                },
                hide: {
                    effect: "clip",
                    duration: 100
                }
            });
        });



        var ID, $dialogSetTRLanguagePrice, $dialogSetDocumentType, $dialogSetCertificateStyle;


        //设置翻译语言价格
        var SetTRLanguagePrice = function (id) {
            ID = id;
            $dialogSetTRLanguagePrice.dialog("open");
        };


        //关联证件类型
        var SetDocumentType = function (id) {
            ID = id;
            $dialogSetDocumentType.dialog("open");
        };

        //设置设置证书样式
        var SetCertificateStyle = function (id) {
            $dialogSetCertificateStyle.find("iframe").attr("src", "CertificateStyleList.aspx?bbsid=" + id);
            $dialogSetCertificateStyle.dialog("open");
        };

    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>申办业务管理</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="BidBusinessEdit.aspx"><i></i><span>新增</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton>
                        </li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','本操作会删除本类别及下属子类别，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th width="6%">选择</th>
                        <th align="left">名称</th>
                        <th align="left">公证费用</th>
                        <th align="left">副本费用</th>
                        <th align="left">排序</th>
                        <th align="left">是否置顶</th>
                        <th align="left">设置翻译价格</th>
                        <th align="left">关联所需证件类型</th>
                        <th align="left">设置证书样式</th>
                        <th width="12%">操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("ID")%>' runat="server" />
                        </td>
                        <td><a href="BidBusinessEdit.aspx?option=edit&id=<%#Eval("ID")%>"><%#Eval("Name")%></a>
                        </td>
                        <td>￥<%#Convert.ToInt32(Eval("NotaryPrice")) %>
                        </td>
                        <td>￥<%#Convert.ToInt32(Eval("CopyPrice")) %>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("Sort")%>' CssClass="sort" onkeydown="return checkNumber(event);" />
                        </td>
                        <td>
                            <%#Eval("IsTop").ToString()=="True"?"<label style=\"color:red\">是</label>":"否" %>
                        </td>
                        <td>
                            <a href="javascript:;" onclick="SetTRLanguagePrice(<%#Eval("ID") %>)">设置</a>
                        </td>
                        <td>
                            <a href="javascript:;" onclick="SetDocumentType(<%#Eval("ID") %>)">关联</a>
                        </td>
                        <td>
                            <a href="javascript:;" onclick="SetCertificateStyle(<%#Eval("ID") %>)">设置</a>
                        </td>
                        <td align="center">
                            <a href="BidBusinessEdit.aspx?option=edit&id=<%#Eval("ID")%>">修改</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
    <form id="dialog-SetTRLanguagePrice">
        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
            <thead>
                <tr>
                    <th align="center">翻译语言</th>
                    <th align="left">价格</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <!--/列表-->
    </form>
    <form id="dialog-SetDocumentType" style="padding: 10px;">
    </form>
    <form id="dialog-SetCertificateStyle" style="padding: 10px;">
        <iframe width="100%" height="100%"></iframe>
    </form>
</body>
</html>
