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
    public partial class plans : System.Web.UI.Page
    {
        string login_user, name_user, fam_user;
        char status_user; // переменные для данных пользователя
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";
        string id_plan = "";
        
        

        protected void Page_Load(object sender, EventArgs e)
        {   
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];


            //перебросим пользователя на экран авторизации по окончании сессии
            /*if (login_user == null)
            { Response.Redirect("autorise.aspx"); }*/
            if (!Page.IsPostBack)
            {
                string q_tab = "SELECT plans.id_plan, plans.data_plan, plans.value_plan, cats.name_cat, bils.name_bil,"+
                    "plans.descript_plan, users.fam_user from (((plans "+
                    "inner join cats on plans.cat_plan = cats.id_cat) "+
                    "inner join bils on plans.bil_plan = bils.id_bil) "+
                    "inner join users on plans.login_user = users.login_user) "+
                     "order by plans.id_plan";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_tab, ole_con);
                 OleDbDataReader dr = com.ExecuteReader();// открыть БД
                gv.DataSource = dr;// прочесть данные
                gv.DataBind();// выбросить в таблицу
                dr.Close(); com.Dispose();ole_con.Close();// закрыть всех
            }

        }

        protected void b_add_con_Click(object sender, EventArgs e)
        {
            Response.Redirect("add_plan.aspx");
        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            if (gv.SelectedIndex != -1)
            {
                string id_plan = gv.Rows[gv.SelectedIndex].Cells[1].Text;
                Response.Redirect("edit_plan.aspx?id_plan=" + id_plan);
            }
            else
            {
                l_hint_no_1.Visible = true;
                l_hint_no_1.Text += "<br />";
            }
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {

        }

        protected void b_yes_Click(object sender, EventArgs e)
        {

        }


    }
}