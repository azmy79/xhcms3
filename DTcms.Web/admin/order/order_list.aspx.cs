using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int payment_status;
        protected int express_status;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = DTRequest.GetQueryInt("status");
            this.payment_status = DTRequest.GetQueryInt("payment_status");
            this.express_status = DTRequest.GetQueryInt("express_status");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("payment_status=2 and status=2 and id>0" + CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords), "status asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.payment_status > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
            }
            if (this.express_status > 0)
            {
                this.ddlExpressStatus.SelectedValue = this.express_status.ToString();
            }
            txtKeywords.Text = this.keywords;
            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&page={4}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, int _payment_status, int _express_status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_status > 0)
            {
                strTemp.Append(" and status=" + _status);
            }
            if (_payment_status > 0)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_express_status > 0)
            {
                strTemp.Append(" and express_status=" + _express_status);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("order_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = string.Empty;
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (model.payment_status > 0)
                    {
                        _title = "待付款";
                    }
                    else
                    {
                        _title = "待确认";
                    }
                    break;
                case 2: //如果订单为已确认状态，则进入发货状态
                    if (model.express_status > 1)
                    {
                        _title = "已发货";
                    }
                    else
                    {
                        //_title = "待发货";
                        _title = "已付款";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "已取消";
                    break;
                case 5:
                    _title = "已作废";
                    break;
            }

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), txtKeywords.Text));
        }

        //订单状态
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                ddlStatus.SelectedValue, this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //支付状态
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), ddlPaymentStatus.SelectedValue, this.express_status.ToString(), this.keywords));
        }

        //发货状态
        protected void ddlExpressStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), ddlExpressStatus.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("order_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("order_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords), "Success");
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutExcel_Click(object sender, EventArgs e)
        {
            var html = new StringBuilder();
            html.Append("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            html.Append(" <head>");
            html.Append(" <!--[if gte mso 9]><xml>");
            html.Append("<x:ExcelWorkbook>");
            html.Append("<x:ExcelWorksheets>");
            html.Append("<x:ExcelWorksheet>");
            html.Append("<x:Name></x:Name>");
            html.Append("<x:WorksheetOptions>");
            html.Append("<x:Print>");
            html.Append("<x:ValidPrinterInfo />");
            html.Append(" </x:Print>");
            html.Append("</x:WorksheetOptions>");
            html.Append("</x:ExcelWorksheet>");
            html.Append("</x:ExcelWorksheets>");
            html.Append("</x:ExcelWorkbook>");
            html.Append("</xml>");
            html.Append("<![endif]-->");
            html.Append(" </head>");
            html.Append("<body>");
            html.Append("<table>");
            html.Append("<tr style=\"text-align:center\">");
            html.Append("<td>订单号</td>");
            html.Append("<td>类型</td>");
            html.Append("<td>姓名</td>");
            html.Append("<td>性别</td>");
            html.Append("<td>证件类型</td>");
            html.Append("<td>证件号码</td>");
            html.Append("<td>出生日期</td>");
            html.Append("<td>住址</td>");
            html.Append("<td>联系电话</td>");
            html.Append("<td>公证事项</td>");
            html.Append("<td>语言</td>");
            html.Append("<td>用途</td>");
            html.Append("<td>国家</td>");
            html.Append("<td>份数</td>");
            html.Append("<td>是否加急</td>");
            html.Append("</tr>");
            var strWhere = "PaymentStatus=2 and OrderStatus=2";
            if (!string.IsNullOrEmpty(txtBeginTime.Text))
            {
                strWhere += " and PaymentTime>'" + txtBeginTime.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text))
            {
                strWhere += " and PaymentTime<'" + DateTime.Parse(txtEndTime.Text.Trim()).AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            var list = new DTcms.BLL.View_BidOrder().GetModelList(strWhere);
            list.ForEach(p =>
            {
                html.Append("<tr style=\"text-align:left;\">");
                html.Append("<td>" + p.OrderNo + "</td>");
                html.Append("<td>当事人</td>");
                html.Append("<td>" + p.CnName + "</td>");
                html.Append("<td>" + (p.Sex ? "男" : "女") + "</td>");
                html.Append("<td>" + Enum.Parse(typeof(DTcms.Common.DTEnums.证件类型), p.CartType.ToString()).ToString() + "</td>");
                html.Append("<td>" + p.CartNum + "</td>");
                html.Append("<td>" + p.Birthday + "</td>");
                html.Append("<td>" + p.Address + "</td>");
                html.Append("<td>" + p.Tel + "</td>");
                html.Append("<td>" + p.BidBusiness + "</td>");
                html.Append("<td>" + p.TRLanguage + "</td>");
                html.Append("<td>" + p.PurposeName + "</td>");
                html.Append("<td>" + p.CountryName + "</td>");
                html.Append("<td>" + p.CopyCount + "</td>");
                html.Append("<td></td>");
                html.Append("</tr>");
            });
            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");
            OutExcel(html.ToString(), "已付款申办信息" + DateTime.Now.ToShortDateString() + ".xls");
        }



        /// <summary>
        /// 以HTML形式输出EXCEL
        /// </summary>
        /// <param name="excelHTML">导出HTML字符串</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="charset">输出流HTTP字符集</param>
        /// <param name="encoding">内容编码</param>
        void OutExcel(string excelHTML, string fileName, string charset = "UTF-8", System.Text.Encoding encoding = null)
        {
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + (HttpContext.Current.Request.Browser.Browser.ToLower().IndexOf("ie") != -1 ? HttpUtility.UrlEncode(fileName) : fileName));
            HttpContext.Current.Response.Charset = charset;
            HttpContext.Current.Response.ContentEncoding = encoding ?? System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(excelHTML);
            HttpContext.Current.Response.End();
        }

        protected void btnOutOrderExcel_Click(object sender, EventArgs e)
        {
            var html = new StringBuilder();
            html.Append("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            html.Append(" <head>");
            html.Append(" <!--[if gte mso 9]><xml>");
            html.Append("<x:ExcelWorkbook>");
            html.Append("<x:ExcelWorksheets>");
            html.Append("<x:ExcelWorksheet>");
            html.Append("<x:Name></x:Name>");
            html.Append("<x:WorksheetOptions>");
            html.Append("<x:Print>");
            html.Append("<x:ValidPrinterInfo />");
            html.Append(" </x:Print>");
            html.Append("</x:WorksheetOptions>");
            html.Append("</x:ExcelWorksheet>");
            html.Append("</x:ExcelWorksheets>");
            html.Append("</x:ExcelWorkbook>");
            html.Append("</xml>");
            html.Append("<![endif]-->");
            html.Append(" </head>");
            html.Append("<body>");
            html.Append("<table>");
            html.Append("<tr style=\"text-align:center\">");
            html.Append("<td>订单号</td>");
            html.Append("<td>申办号</td>");
            html.Append("<td>姓名</td>");
            html.Append("<td>性别</td>");
            html.Append("<td>证件类型</td>");
            html.Append("<td>证件号码</td>");
            html.Append("<td>出生日期</td>");
            html.Append("<td>住址</td>");
            html.Append("<td>联系电话</td>");
            html.Append("<td>公证事项</td>");
            html.Append("<td>语言</td>");
            html.Append("<td>用途</td>");
            html.Append("<td>国家</td>");
            html.Append("<td>份数</td>");
            html.Append("<td>订单金额</td>");
            html.Append("<td>付款时间</td>");
            html.Append("</tr>");
            var strWhere = "PaymentStatus=2 and OrderStatus=2";
            if (!string.IsNullOrEmpty(txtBeginTime.Text))
            {
                strWhere += " and PaymentTime>'" + txtBeginTime.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text))
            {
                strWhere += " and PaymentTime<'" + DateTime.Parse(txtEndTime.Text.Trim()).AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            var list = new DTcms.BLL.View_BidOrder().GetModelList(strWhere);
            list.ForEach(p =>
            {
                html.Append("<tr style=\"text-align:left;\">");
                html.Append("<td>" + p.OrderNo + "</td>");
                html.Append("<td>" + p.Number + "</td>");
                html.Append("<td>" + p.CnName + "</td>");
                html.Append("<td>" + (p.Sex ? "男" : "女") + "</td>");
                html.Append("<td>" + Enum.Parse(typeof(DTcms.Common.DTEnums.证件类型), p.CartType.ToString()).ToString() + "</td>");
                html.Append("<td>" + p.CartNum + "</td>");
                html.Append("<td>" + p.Birthday + "</td>");
                html.Append("<td>" + p.Address + "</td>");
                html.Append("<td>" + p.Tel + "</td>");
                html.Append("<td>" + p.BidBusiness + "</td>");
                html.Append("<td>" + p.TRLanguage + "</td>");
                html.Append("<td>" + p.PurposeName + "</td>");
                html.Append("<td>" + p.CountryName + "</td>");
                html.Append("<td>" + p.CopyCount + "</td>");
                html.Append("<td>" + p.OrderAmount + "</td>");
                html.Append("<td>" + p.PaymentTime + "</td>");
                html.Append("</tr>");
            });
            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");
            OutExcel(html.ToString(), "已支付订单信息" + DateTime.Now.ToShortDateString() + ".xls");
        }

    }
}