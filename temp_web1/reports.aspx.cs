using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void b_add_Click(object sender, EventArgs e)
        {
            Response.Redirect("report.aspx");
        }
    }
}