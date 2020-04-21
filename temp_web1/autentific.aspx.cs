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
    public partial class autentific : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["login_user"] = "";
            Session["name_user"] = "";
            Session["fam_user"] = "";
            Session["status_user"] = ' ';
        }

        protected void b_enter_Click(object sender, EventArgs e)
        {
            string q_autorize = "select * from users where login_user = '" + tb_login.Text + "' and pass_user = '" + tb_password.Text + "'";
            OleDbDataReader dr = my_query(q_autorize);
            if (dr.Read())
            {
                Session["login_user"] = dr[2].ToString();
                Session["name_user"] = dr[0].ToString();
                Session["fam_user"] = dr[1].ToString();
                Session["status_user"] = char.Parse(dr[4].ToString());
                Response.Redirect("consumptions.aspx");
            }
            else { l_incorrect.Visible = true; }
        }
        OleDbDataReader my_query(string q)
        {
            string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               "C:\\Users\\phill\\documents\\plaza.accdb";
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q, ole_con);
            com.CommandType = CommandType.Text;//тип команды - текст
            OleDbDataReader dr = com.ExecuteReader();
            return dr;

        }


    }
}