﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class consumptions : System.Web.UI.Page
    {
        string login_user, name_user, fam_user, status_user; // переменные для данных пользователя
        //строка подключения
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];
            status_user = (string)Session["status_user"];
            if (status_user != "a")
            { Response.Redirect("autorise.aspx"); }

            if (!Page.IsPostBack)
            {   
                //СОздаем запрос к БД
                string q_table = "SELECT consumptions.id_con, consumptions.data_create, " +
                    "consumptions.data_change, consumptions.value_con, cats.name_cat, consumptions.bil_con, " +
                    "consumptions.descript_con, users.fam_user as u_f, users2.fam_user as u_f2 " +
                    "from (((consumptions " +
                    "inner join users on " +
                    "consumptions.create_login = users.login_user) " +
                    "inner join users2 on " +
                    "consumptions.change_login = users2.login_user) " +
                    "inner join cats on consumptions.cat_con = cats.id_cat) " +
                    "order by consumptions.id_con";
                //СОздаем объект Оле - соединение с БД
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                //Выполняем запрос. Результат - массив в формате "Команда"
                OleDbCommand ole_com = new OleDbCommand(q_table, ole_con);
                OleDbDataReader dr = ole_com.ExecuteReader();
                //заполняем таблицу данными
                gv1.DataSource = dr;
                gv1.DataBind();
                ole_con.Close();
            }
           
        }

        protected void b_add_con_Click(object sender, EventArgs e)
        {
           Response.Redirect("add_consumpt.aspx");
        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            if (gv1.SelectedIndex != -1)
            {
                string id_con = gv1.Rows[gv1.SelectedIndex].Cells[1].Text;
                Response.Redirect("edit_consumpt.aspx?id_con=" + id_con);
            }
            else
            {
                l_hint_no_1.Visible = true;
                l_hint_no_1.Text += "<br />";
            }

        }

        protected void b_delete_Click(object sender, EventArgs e)
        {
            if (gv1.SelectedIndex != -1)
            {
                mpe.Show();
              
            }
            else
            {
                l_hint_no_1.Visible = true;
                l_hint_no_1.Text += "<br />";
            }
        }

        protected void b_yes_Click(object sender, EventArgs e)
        {
            string id_con = gv1.Rows[gv1.SelectedIndex].Cells[1].Text;//Получил ID выбранного расхода
              string q_con = "delete from consumptions where id_con = " + id_con;
              //Создал запрос с нужным расходом.

              //Соединяюсь с БД
              string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                  "C:\\Users\\phill\\documents\\plaza.accdb";
              OleDbConnection ole_con = new OleDbConnection(con_str);
              ole_con.Open();
              OleDbCommand com = new OleDbCommand(q_con, ole_con);
              com.ExecuteNonQuery();
              com.Dispose();
              ole_con.Close();
              System.Threading.Thread.Sleep(500);
              Response.Redirect("consumptions.aspx");
        }

    }
}