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
    public partial class users : System.Web.UI.Page
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
                string q_users = "SELECT * from users";

                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_users, ole_con);
                OleDbDataAdapter da = new OleDbDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //Передаю массив со счетами в сессию
                string[,] users_data = new string[ds.Tables[0].Rows.Count, 5];
                int i = 0;
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        users_data[i, j] = drow.ItemArray[j].ToString();
                    }
                    i++;
                }
                Session["users_data[,]"] = users_data;

                //Заполняю листбокс названиями счетов
                i = 0;
                OleDbDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    lb_users.Items.Add(dr["login_user"].ToString());
                    if (dr[4].ToString() == "a")
                    { lb_users.Items[i].Attributes.Add("style", "background-color:Yellow"); }
                    i++;
                }
                
                com.Dispose(); ole_con.Close();// закрыть всех
            }
        }

        protected void b_add_Click(object sender, EventArgs e)
        {
        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {

        }

        protected void b_clear_Click(object sender, EventArgs e)
        {

        }

        protected void b_yes_Click(object sender, EventArgs e)
        {

        }

        protected void b_no_Click(object sender, EventArgs e)
        {

        }

        protected void lb_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[,] users_data = (string[,])Session["users_data[,]"];

            for (int i = 0; i < users_data.GetLength(0); i++)
            {
                if (users_data[i, 4] == "a") //перекрашиваем поля листбокса
                { lb_users.Items[i].Attributes.Add("style", "background-color:Yellow"); }
                if (lb_users.SelectedItem.Text == users_data[i,2]) // выводим выделенного пользователя
                {
                    tb_login.Text = users_data[i,2];
                    tb_name.Text = users_data[i, 0];
                    tb_fam.Text = users_data[i, 1];
                    tb_password.Text = users_data[i, 3];
                    rbl_status.SelectedValue = users_data[i, 4];
                }
            }
            /*if (lb_users.SelectedValue.ToString() == "a")
            {
                rbl_status.SelectedValue = "a";
            }
            else { rbl_status.SelectedValue = "u"; }*/
        }
    }
}