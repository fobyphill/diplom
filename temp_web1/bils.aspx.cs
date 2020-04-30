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
    public partial class bils : System.Web.UI.Page
    {
        string login_user, name_user, fam_user;
        char status_user; // переменные для данных пользователя
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Зададим параметры пользователя
            login_user = "admin";
            /*login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];*/

            //перебросим пользователя на экран авторизации по окончании сессии
            /*if (login_user == null)
            { Response.Redirect("autorise.aspx"); }*/

            if (!Page.IsPostBack)
            {
                string q_bils = "SELECT * from bils";
                
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_bils, ole_con);
                OleDbDataAdapter da = new OleDbDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //Передаю массив со счетами в сессию
                string[,] bils_data = new string[ds.Tables[0].Rows.Count, 4];
                int i = 0;
               foreach (DataRow drow in ds.Tables[0].Rows)
               {
                   for (int j = 0; j<4; j++)
                   {
                       bils_data[i, j] = drow.ItemArray[j].ToString();
                   }
                   i++;
               }
               Session["bils_data[,]"] = bils_data;
              // com.Dispose();
              // com = new OleDbCommand(q_bils,ole_con);
                //Заполняю листбокс названиями счетов
               OleDbDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {lb_bils.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));}
                if (lb_bils.Items.Count == 0)
                { 
                    lb_bils.Text = "Счетов пока нет. Создайте новый счет";
                    lb_bils.Style.Add("color", "red");
                }
                com.Dispose(); ole_con.Close();// закрыть всех
            }
        }

        protected void b_add_con_Click(object sender, EventArgs e)
        {

        }

        protected void b_change_Click(object sender, EventArgs e)
        {

        }

        protected void b_delete_Click(object sender, EventArgs e)
        {

        }

        protected void b_yes_Click(object sender, EventArgs e)
        {

        }

        protected void b_no_Click(object sender, EventArgs e)
        {

        }

        protected void lb_bils_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_name.Text = lb_bils.SelectedItem.Text;
            tb_num.Text = lb_bils.SelectedValue.ToString();
            string[,] bils_data = (string[,])Session["bils_data[,]"];
            for (int i = 0; i < bils_data.GetLength(0); i++)
            {
                if (bils_data[i,0] == lb_bils.SelectedValue.ToString())
                {
                    tb_descript.Text = bils_data[i, 3];
                    break;
                }
            }

        }

        protected void b_clear_Click(object sender, EventArgs e)
        {
            tb_descript.Text = "";
            tb_name.Text = "";
            tb_num.Text = "";
        }

        protected void b_add_Click(object sender, EventArgs e)
        {
            string q_add = "";
        }
        void exe_query(string q)
        {
            OleDbConnection ole_con = new OleDbConnection(con_str);
            OleDbCommand com = new OleDbCommand(q, ole_con);
            com.ExecuteNonQuery();
            ole_con.Close();
        }
    }
}