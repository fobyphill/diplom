﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
	public partial class cons_user : System.Web.UI.Page
	{
        string login_user, fam_user, status_user; // переменные для данных пользователя
        //строка подключения
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";

		protected void Page_Load(object sender, EventArgs e)
		{
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            fam_user = (string)Session["fam_user"];
            status_user = (string)Session["status_user"];
            if (login_user == null)
            { Response.Redirect("autorise.aspx"); }

            if (!Page.IsPostBack)
            {
                //СОздаем запрос к БД
                string q_table = "SELECT * from cons_output where u_f = '" + fam_user + "' and data_change = Date()";
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
	}
}