using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Appointment
{
    public partial class AppointmentDetail : UI.ManagePage
    {

        public DTcms.Model.Appointment Model { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model = new DTcms.BLL.Appointment().GetModel(DTcms.Common.DTRequest.GetInt("id", 0));
            }
        }
    }
}