﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class Bid_Step2_3 : UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserLogin()) SkipLogin();
        }
    }
}