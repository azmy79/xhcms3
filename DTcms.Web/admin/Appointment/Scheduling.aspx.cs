using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.admin.Appointment
{
    public partial class Scheduling : UI.ManagePage
    {

        public List<DTcms.Model.manager> ManagerList { get; set; }

        public List<DTcms.Model.Scheduling> SchedulingList { get; set; }

        public int CurrentMonthType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            CurrentMonthType = DateTime.Now.Month % 2;
            ManagerList = DTcms.Common.DataConvertHelper.DataTableToList<DTcms.Model.manager>(new BLL.manager().GetList(0, "role_id=3 and is_lock=0", "add_time Desc").Tables[0]);
            SchedulingList = new DTcms.BLL.Scheduling().GetModelList("1=1");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var list = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DTcms.Model.Scheduling>>(hidData.Value);
            if (new DTcms.BLL.Scheduling_Custom().BindScheduling(list))
                JscriptMsg("保存成功！", "Scheduling.aspx", "Success");
            else
                JscriptMsg("保存失败！", "Error");
            BindData();
        }
    }
}