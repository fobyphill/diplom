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
    public partial class add_consumpt : System.Web.UI.Page
    {
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";
        string sql_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
        string login_user, status_user;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            login_user = (string)Session["login_user"];
            status_user = (string)Session["status_user"];
  
            if (login_user == null)
            { Response.Redirect("autorise.aspx"); }
            if (!Page.IsPostBack)
            {
                if (status_user == "u")
                { 
                    p_vis.Visible = false;
                    p_main.CssClass = "maindiv";
                }
                
                string q_cat = "select * from cats";
                OleDbConnection ole_con = new OleDbConnection(sql_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_cat, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                //Заполним поля категорий
                while (dr.Read())
                {
                    if (dr[3].ToString() == "0")
                    {
                        TreeNode node_cat = new TreeNode(dr[1].ToString(), dr[0].ToString());
                        find_child(node_cat);
                        tv.Nodes.Add(node_cat);
                    }
                }
                dr.Close();

                //Заполним счета
                ddl_bils.Items.Add("Выберите счет");
                string q_bil = "select name_bil from bils";
                com = new OleDbCommand(q_bil, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(dr[0].ToString());
                }
                if (ddl_bils.Items.Count == 1)
                {
                l_bil.Text = "Создайте счет на странице <a href='bils.aspx'>Управление счетами</a>";
                l_bil.CssClass = "hint stress";
                }
                ole_con.Close();
                tb_data.Text = DateTime.Now.ToString("yyyy-MM-dd");//Укажем дату
            }

        }

        protected void b_cancel_Click(object sender, EventArgs e)
        {
            if ((string)Session["status_user"] == "a")
            { Response.Redirect("consumptions.aspx"); }
            else 
            { Response.Redirect("cons_user.aspx"); }
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string sql_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
            string q_max_id = "SELECT max(id_con) from consumptions";// Запрос наибольшего айди
            //выполняем, получаем в переменную наибольший айди
            OleDbConnection ole_con = new OleDbConnection(sql_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_max_id, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            int id_max;
            if (!Int32.TryParse(dr[0].ToString(), out id_max))
            { id_max = 0; }
            dr.Close();
            com.Dispose();
            ole_con.Close();
            DateTime dt = new DateTime();
            dt = DateTime.Parse(tb_data.Text);
            string dt_cr = dt.ToShortDateString();//Получим дату создания заказа
            string dt_ch = DateTime.Now.ToShortDateString();
            
            //Получим значение
            bool flag = false;//если все данные ввели, флаг не включается
            float value;
            string value_str = "";
            tb_value.Text = tb_value.Text.Replace('.', ',');
            if (!Single.TryParse(tb_value.Text, out value))
            {
                l_value.Text = "Укажите Корректное число";
                l_value.Style.Add("color", "red");
                flag = true;
            }
            else
            {
                 value_str = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
            
            //Получим ID категории
            string num_cat = "";
            if (tv.SelectedValue.ToString() == "")
            {
                l_cat.Text = "Укажите категорию";
                l_cat.Style.Add("color", "red");
                flag = true;
            }
            else
            { num_cat = tv.SelectedValue.ToString(); }

            //Заполним поле "комментарий"
            string descript_con = "";
            if (tb_descript.Text != "")
            {
                descript_con = tb_descript.Text;

            }
            //Добавим номер счета
            string bil = ddl_bils.SelectedItem.Text;
            if (ddl_bils.Items.Count == 1)
            {
                flag = true;
            }
            else if (ddl_bils.SelectedItem.Text == "Выберите счет")
            {
                flag = true;
                l_bil.Text = "Счет не выбран.";
                l_bil.Style.Add("color", "red");
            }
            //Заполним данными запрос
            string q_add = "insert into consumptions " +
            "(id_con, data_create, data_change, value_con, cat_con, bil_con, "+
            "descript_con, create_login, change_login) " +
            "values ("+((++id_max).ToString())+", '"+dt_cr
            + "', '" + dt_ch + "', " + value_str + ", " + num_cat+", '"+bil+"', '" + descript_con + "', '"
            +login_user+"', '"+login_user+"')";
            ole_con.Open();
            com = new OleDbCommand(q_add, ole_con);
            if (!flag)
            {
                com.ExecuteNonQuery(); //Выполнить изменение данных в БД
                com.Dispose(); ole_con.Close();
                 System.Threading.Thread.Sleep(450);
                 if ((string)Session["status_user"] == "a")
                 { Response.Redirect("consumptions.aspx"); }
                else
                 { Response.Redirect("cons_user.aspx"); }
            }
        }

        void find_child(TreeNode pn)
        {
            //соединились с БД
            string q_cat = "select * from cats";
            OleDbConnection ole_con = new OleDbConnection(sql_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_cat, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            //Забираем данные с нода
            string p_i = pn.Value.ToString();

            while (dr.Read())
            {
                if (dr[3].ToString() == p_i)
                {
                    TreeNode n = new TreeNode(dr[1].ToString(), dr[0].ToString());
                    find_child(n);
                    pn.ChildNodes.Add(n);
                }
            }
            dr.Close(); com.Dispose(); ole_con.Close();
        }

        protected void ib_show_hide_Click(object sender, ImageClickEventArgs e)
        {
            if (l_collapse.Text == "Развернуть все")
            {
                tv.ExpandAll();
                l_collapse.Text = "Свернуть все";
                ib_show_hide.CssClass = "checkbox_checked";
            }
            else
            {
                tv.CollapseAll(); 
                l_collapse.Text = "Развернуть все";
                ib_show_hide.CssClass = "checkbox_uncheck";
            }
        }
        
    }
}