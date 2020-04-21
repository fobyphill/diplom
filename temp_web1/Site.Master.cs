using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class SiteMaster : MasterPage
    {
        string name_user, fam_user, login_user;
        protected void Page_Load(object sender, EventArgs e)
        {
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];
            login_user = (string)Session["login_user"];
            if (login_user != "")
            { l_user.Text = name_user + " " + fam_user; }
        }
    }
}