using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class autorise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!Page.IsPostBack)
            { Session.Abandon(); }*/
            Session["flag_add_edit_cat"] = "0";

        }

        protected void b_enter_Click(object sender, EventArgs e)
        {
            string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                "C:\\Users\\phill\\documents\\plaza.accdb";
            string q_autorize = "select * from users where login_user = '" + tb_login.Text + "' and pass_user = '" + tb_password.Text + "'";
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_autorize, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                Session["login_user"] = dr[2].ToString();
                Session["name_user"] = dr[0].ToString();
                Session["fam_user"] = dr[1].ToString();
                Session["status_user"] = dr[4].ToString();
                if ((string)Session["status_user"] == "a")
                { Response.Redirect("consumptions.aspx"); }
                else { Response.Redirect("cons_user.aspx"); }
                
            }
            else { l_incorrect.Visible = true; }
            dr.Close(); ole_con.Close();
        }
    }
}