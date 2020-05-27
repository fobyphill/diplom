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
    public partial class reports : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tb_month.Text = DateTime.Now.ToString("yyyy-MM");
            }
        }

        protected void b_add_Click(object sender, EventArgs e)
        {
            //Выведем список главных категорий
            /*OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select name_cat from cats where parent_id = 0", ole_con);
            DataSet ds = new DataSet();
            da.Fill(ds);*/
            string month = DateTime.Parse(tb_month.Text).Month.ToString();
            string year = DateTime.Parse(tb_month.Text).Year.ToString();
            Response.Redirect("rept.aspx?month="+month+"&year="+year);
        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            /*string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";
            OleDbConnection ole = new OleDbConnection(con_str);
            ole.Open();
            OleDbCommand com = new OleDbCommand("select * from cons_output" /*where data_change = #08/05/2020#", ole);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            string aaa = dr[4].ToString;*/
        }

        protected void rbl_choice_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_choice_report.SelectedIndex == 0)
            { p_fast_report.Visible = true; }
        }

    }
}