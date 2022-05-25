<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bid_View.aspx.cs" Inherits="DTcms.Web.admin.order.Bid_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>查看订单信息</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <style>
        .yuyue_box {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 10000;
            left: 0;
            top: 0;
            display: none;
        }

            .yuyue_box .bg {
                width: 100%;
                height: 100%;
                position: absolute;
                z-index: 1;
                left: 0;
                top: 0;
                background: #000;
                filter: Alpha(opacity=70);
                -moz-opacity: .7;
                opacity: 0.7;
            }

            .yuyue_box .yuyue_con {
                width: 960px;
                position: absolute;
                left: 50%;
                top: 5%;
                z-index: 10;
                margin: 0px 0 0 -500px;
                padding: 20px;
                background: #FFF;
                overflow: scroll;
                height: 600px;
            }

                .yuyue_box .yuyue_con img {
                    width: 100%;
                }
    </style>
    <script>
        $(function () {
            //样式预览弹框隐藏
            $(".yuyue_box .bg").click(function () {
                $(".yuyue_box").hide();
            });
            //样式预览弹框显示
            $(".tc").click(function () {
                $(".yuyue_box").show().find("img").attr("src", $(this).attr("tagsrc"));
            });
        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="express_list.aspx" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="order_list.aspx"><span>订单管理</span></a>
            <i class="arrow"></i>
            <span>订单详细</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">订单详细信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>订单号</dt>
                <dd>
                    <span id="spanOrderNo"><%=OrderModel.order_no %></span>
                </dd>
            </dl>
            <dl>
                <dt>申办号</dt>
                <dd>
                    <span id="span1"><%=BidModel.Number %></span>
                    <span style="float: right; margin-right: 60px; color: red; font-size: 30px;">订单金额：￥<%=Convert.ToInt32(OrderModel.order_amount) %></span>
                </dd>
            </dl>
            <dl>
                <dt>会员信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">会员账户</th>
                            <td>
                                <%=(new DTcms.BLL.users().GetModel(BidModel.UserID)??new DTcms.Model.users()).user_name %></td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>公证信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">翻译语言</th>
                            <td>
                                <%
                                    TRLanguage.ForEach(p =>
                                    {
                                        Response.Write(p.Name + "&nbsp;&nbsp;");
                                    });  
                                %>
                            </td>
                        </tr>
                        <tr>
                            <th>用途</th>
                            <td>
                                <%=BidModel.PurposeName %>
                            </td>
                        </tr>
                        <tr>
                            <th>前往国家</th>
                            <td><%=BidModel.CountryName %></td>
                        </tr>
                        <tr>
                            <th>公证书数量</th>
                            <td><%=BidModel.CopyCount %></td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>申办业务</dt>
                <dd>
                    <table id="tabBidBusiness" border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <thead>
                            <tr style="background: #f9f9f9">
                                <td width="20%"></td>
                                <td width="20%">
                                    <label>公证费用</label></td>
                                <td width="20%">
                                    <label>公证书翻译费用</label></td>
                                <td width="20%">
                                    <label>副本费用</label></td>
                                <td width="20%">
                                    <label>证书样式</label></td>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                var country = new DTcms.BLL.Country().GetModel(BidModel.CountryID);//前往国家
                                var certificateStyle = new DTcms.BLL.CertificateStyle().GetModelList("ID in(select CertificateStyleID from Bid_BidBusiness where Bid_BidBusiness.BidID=" + BidModel.ID + ") order by Sort Desc");
                                decimal totalPrice = 0;//总价格
                                Response.Write(DTcms.Web.UI.BasePage.WriteBidTable(country, BidModel.CopyCount, TRLanguage, BidBusiness, out totalPrice, certificateStyle));
                            %>
                        </tbody>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>个人基本信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">申请人姓名</th>
                            <td><%=BidModel.CnName %></td>
                        </tr>
                        <tr>
                            <th>英文名</th>
                            <td><%=BidModel.EnName %></td>
                        </tr>
                        <tr>
                            <th>性别</th>
                            <td><%=BidModel.Sex?"男":"女" %></td>
                        </tr>
                        <tr>
                            <th>出生日期</th>
                            <td><%=BidModel.Birthday.ToString("yyyy-MM-dd") %></td>
                        </tr>
                        <tr>
                            <th>证件类型</th>
                            <td><%=Enum.Parse(typeof(DTcms.Common.DTEnums.证件类型),BidModel.CartType.ToString()).ToString() %></td>
                        </tr>
                        <tr>
                            <th>证件号码</th>
                            <td><%=BidModel.CartNum %></td>
                        </tr>
                        <tr>
                            <th>手机号码</th>
                            <td><%=BidModel.Tel %></td>
                        </tr>
                        <tr>
                            <th>现住地址</th>
                            <td><%=BidModel.Address %></td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>证件信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <%
                            Document.ForEach(p =>
                            {
                                Response.Write("<tr>");
                                Response.Write("    <th width=\"20%\">" + p.Name + "</th>");
                                Response.Write("    <td><a href=\"" + p.Path + "\">【下载附件】</a></td>");
                                Response.Write("</tr>");
                            });  
                        %>
                    </table>
                </dd>
            </dl>
            <%
                var bidFile = BidSourceFile.Count > 0 ? BidSourceFile[0] : new DTcms.Model.BidSourceFile();
            %>
            <dl <%=BidSourceFile.Count > 0?string.Empty:"style=\"display:none\"" %>>
                <dt>所需公证文本信息</dt>
                <dd>
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                        <tr>
                            <th width="20%">是否需要翻译</th>
                            <td><%=bidFile.NeedTranslation?"是":"否" %></td>
                        </tr>
                        <tr>
                            <th width="20%">所需公证文本</th>
                            <td><a href="<%=bidFile.Path %>">【下载附件】</a></td>
                        </tr>
                        <tr>
                            <th width="20%">翻译价格</th>
                            <td>￥<%=Convert.ToInt32(bidFile.TranslationPrice) %>
                            </td>
                        </tr>
                    </table>
                </dd>
            </dl>
            <dl>
                <dt>申办时间</dt>
                <dd>
                    <%=BidModel.AddTime.ToString("yyyy-MM-dd") %>
                </dd>
            </dl>
            <dl>
                <dt>支付时间</dt>
                <dd>
                    <%=Convert.ToDateTime(OrderModel.payment_time).ToString("yyyy-MM-dd") %>
                </dd>
            </dl>
        </div>
        <!--/内容-->
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
    <div class="yuyue_box" style="display: none;">
        <div class="bg"></div>
        <div class="yuyue_con">
            <img src="">
        </div>
    </div>
</body>
</html>
