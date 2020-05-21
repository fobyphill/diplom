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
    public partial class test : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
           if (!Page.IsPostBack)
           {
               OleDbConnection ole_con = new OleDbConnection(con_str);
               ole_con.Open();
               OleDbCommand com = new OleDbCommand("select * from bils", ole_con);
               OleDbDataReader dr = com.ExecuteReader();
               gv.DataSource = dr;
               gv.DataBind();
           }
             
        }

         protected void cb_CheckedChanged(object sender, EventArgs e)
         {
             for (int i = 0; i < gv.Rows.Count; i++ )
             {
                 CheckBox chb = (CheckBox)gv.Rows[i].FindControl("cb");
                 if (chb.Checked)
                 {
                     gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(22, 219, 219);
                 }
                 else if (i % 2 != 0)
                     { gv.Rows[i].BackColor = System.Drawing.Color.White; }
                 else { gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(239, 243, 251); }

             }
                 
         }
    }
}