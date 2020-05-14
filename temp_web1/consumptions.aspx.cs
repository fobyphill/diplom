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
    public partial class consumptions : System.Web.UI.Page
    {
        string login_user, status_user; // переменные для данных пользователя
        //строка подключения
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            status_user = (string)Session["status_user"];
            /*if (status_user != "a")
            { Response.Redirect("autorise.aspx"); }*/

            if (!Page.IsPostBack)
            {
                string q_table = "select top 10 * from cons_output order by data_create desc";
                string q_bils = "select name_bil from bils";
                string q_users = "select login_user, status_user from users";
                //СОздаем объект Оле - соединение с БД
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                //Выполняем запрос. Результат - массив в формате "Команда"
                OleDbCommand com = new OleDbCommand(q_table, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                //заполняем таблицу данными
                gv1.DataSource = dr;
                gv1.DataBind();
                //Заполним-ка комбобоксы "счета" и "пользователи" данными
                com = new OleDbCommand(q_bils, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(dr[0].ToString());
                }
                com = new OleDbCommand(q_users, ole_con);
                dr = com.ExecuteReader();
                int i = 1;
                while(dr.Read())
                {
                    ddl_user.Items.Add(dr[0].ToString());
                   
                        if (dr[1].ToString() == "a")
                        {
                            ddl_user.Items[i].Attributes.Add("style", "background-color:Yellow");
                        }
                        i++;
                }
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
              //string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";
              OleDbConnection ole_con = new OleDbConnection(con_str);
              ole_con.Open();
              OleDbCommand com = new OleDbCommand(q_con, ole_con);
              com.ExecuteNonQuery();
              com.Dispose();
              ole_con.Close();
              System.Threading.Thread.Sleep(500);
              Response.Redirect("consumptions.aspx");
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            string q_find_cons = "select * from cons_output"; //основной запрос
            string q_adapter = " where";// переходник для добавления условий
            bool flag = false;
            //Начало блока всех условий
            //блок даты затрат
            if (tb_date_create_begin.Text == "" && tb_date_create_end.Text != "")
            {
                flag = true;
                DateTime dt = new DateTime();
                dt = DateTime.Parse(tb_date_create_end.Text);
                q_find_cons += " where data_create <= '" + dt.ToShortDateString() + "'";
            }
            if (tb_date_create_begin.Text != "" && tb_date_create_end.Text == "")
            {
                flag = true;
                DateTime dtb = new DateTime();
                dtb = DateTime.Parse(tb_date_create_begin.Text);
                q_find_cons += " where data_create >= '" + dtb.ToShortDateString() + "'";
            }
            if (tb_date_create_begin.Text !="" && tb_date_create_end.Text !="")
            {
                flag = true;
                DateTime dtb = new DateTime();
                dtb = DateTime.Parse(tb_date_create_begin.Text);
                DateTime dte = new DateTime();
                dte = DateTime.Parse(tb_date_create_end.Text);
                if (dtb > dte)//меняем местами даты, если пользователь перепутал кра
                {
                    DateTime temp_date = new DateTime();
                    temp_date = dte;
                    dte = dtb;
                    dtb = temp_date;
                    string temp_data = tb_date_create_begin.Text;
                    tb_date_create_begin.Text = tb_date_create_end.Text;
                    tb_date_create_end.Text = temp_data;
                }
                q_find_cons += " where data_create between '" + dtb.ToShortDateString() + "' and '" + dte.ToShortDateString() + "'";
            }
            //блок даты изменения затрат
            if (flag){ q_adapter = " and"; }
            if (tb_date_change_begin.Text == "" && tb_date_change_end.Text != "")
            {
                flag = true;
                DateTime dt = new DateTime();
                dt = DateTime.Parse(tb_date_change_end.Text);
                q_find_cons += q_adapter;
                q_find_cons += " data_change <= '" + dt.ToShortDateString() + "'";
            }
            if (tb_date_change_begin.Text != "" && tb_date_change_end.Text == "")
            {
                flag = true;
                DateTime dtb = new DateTime();
                dtb = DateTime.Parse(tb_date_change_begin.Text);
                q_find_cons += q_adapter;
                q_find_cons += " data_change between >= '" + dtb.ToShortDateString() + "'";

            }
            if (tb_date_change_begin.Text != "" && tb_date_change_end.Text != "")
            {
                flag = true;
                DateTime dtb = new DateTime();
                dtb = DateTime.Parse(tb_date_change_begin.Text);
                DateTime dte = new DateTime();
                dte = DateTime.Parse(tb_date_change_end.Text);
                q_find_cons += q_adapter;
                if (dtb > dte)//меняем местами даты, если пользователь перепутал края
                {
                    DateTime temp_date = new DateTime();
                    temp_date = dte;
                    dte = dtb;
                    dtb = temp_date;
                    string temp_data = tb_date_change_begin.Text;
                    tb_date_change_begin.Text = tb_date_change_end.Text;
                    tb_date_change_end.Text = temp_data;
                }
                q_find_cons += " data_change between '" + dtb.ToShortDateString() + "' and '" + dte.ToShortDateString() + "'";

            }

            //блок значений
                if (flag) { q_adapter = " and"; }
                if (tb_value_bottom.Text == "" && tb_value_top.Text != "")
                {
                    if (!try_parse_val(tb_value_top.Text))
                    {
                        l_hint_no_1.Text = "Указано некорректное значение верхнего порога затрат. Введите число.";
                        l_hint_no_1.Visible = true;
                    }
                    else {
                        flag = true;
                        q_find_cons += q_adapter;
                        q_find_cons += " value_con <= " + tb_value_top.Text; 
                        }
                }
                if (tb_value_bottom.Text != "" && tb_value_top.Text == "")
                {   
                    if (!try_parse_val(tb_value_bottom.Text))
                    {
                        l_hint_no_1.Text = "Указано некорректное значение нижнего порога затрат. Введите число.";
                        l_hint_no_1.Visible = true;
                    }
                    else {
                        flag = true;
                        q_find_cons += q_adapter;
                        q_find_cons += " value_con >= " + tb_value_bottom.Text; 
                        }
                }
                if (tb_value_bottom.Text != "" && tb_value_top.Text != "")
                {
                    if (!try_parse_val(tb_value_bottom.Text) || !try_parse_val(tb_value_top.Text))
                    {
                        l_hint_no_1.Text = "Указано некорректное значение затрат. Введите число.";
                        l_hint_no_1.Visible = true;
                    }
                    else
                    {
                        q_find_cons += q_adapter;
                        flag = true;
                        float value_top = float.Parse(tb_value_top.Text);
                        float value_bottom = float.Parse(tb_value_bottom.Text);
                        if (value_top < value_bottom)
                        {
                            string temp_str = tb_value_top.Text;
                            tb_value_top.Text = tb_value_bottom.Text;
                            tb_value_bottom.Text = temp_str;
                        }
                        q_find_cons += " value_con between " + tb_value_bottom.Text + " and " + tb_value_top.Text;
                    }
                }
            
            //блок категорий
            if (flag) { q_adapter = " and"; }
            if (tb_cats.Text != "")
            {
                //Парсим категорию
                string q_ver_cat = "select count(*) from cats where name_cat = '" + tb_cats.Text + "'";
                OleDbConnection ole_con2 = new OleDbConnection(con_str);
                ole_con2.Open();
                OleDbCommand com2 = new OleDbCommand(q_ver_cat, ole_con2);
                OleDbDataReader dr2 = com2.ExecuteReader();
                    dr2.Read();
                if (dr2[0].ToString() == "0")
                {
                    l_hint_no_1.Text = "Категория указана неверно.";
                    l_hint_no_1.Visible = true;
                }
                else
                {
                    q_find_cons += q_adapter;
                    q_find_cons += " name_cat = '" + tb_cats.Text + "'";
                    flag = true;
                }
                 dr2.Dispose();
                 com2.Dispose();
                 ole_con2.Close();
            }

            //блок счетов
            if (flag) { q_adapter = " and"; }
            if (ddl_bils.SelectedIndex > 0)
            {
                q_find_cons += q_adapter;
                q_find_cons += " bil_con = '" + ddl_bils.SelectedItem.Text + "'";
                flag = true;
            }
            //блок пользователей
            if (flag) { q_adapter = " and"; }
            if (ddl_user.SelectedIndex >0)
            { 
                q_find_cons += q_adapter;
                q_find_cons += " (create_login = '" + ddl_user.SelectedItem.Text + "' or change_login = '"+ddl_user.SelectedItem.Text+"')";
                flag = true;
            }
            //блок айди + комменты
            if (flag) { q_adapter = " and"; }
            if (try_parse_val(tb_search.Text))
            {
                int id = Int32.Parse(tb_search.Text);
                q_find_cons = "select * from cons_output where id_con = " + id;
                flag = true;
            }
            else if (tb_search.Text.Length >0)
            {
                q_find_cons += q_adapter;
                q_find_cons += " descript_con like '%" + tb_search.Text + "%'";
                flag = true;
            }

            
            //Конец блока всех условий
            //вывод таблицы
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_find_cons, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            gv1.DataSource = null;
            gv1.DataBind();
            gv1.DataSource = dr;
            gv1.DataBind();
            ole_con.Close();
        }
        bool try_parse_val(string value)
        {
            float val_digit;
            value = value.Replace('.', ',');
            bool res = Single.TryParse(value, out val_digit);
            return res;

        }

        protected void b_clear_Click(object sender, EventArgs e)
        {
            tb_date_create_begin.Text = "";
            tb_date_create_end.Text = "";
            tb_date_change_begin.Text = "";
            tb_date_change_end.Text = "";
            tb_value_bottom.Text = "";
            tb_value_top.Text = "";
            tb_cats.Text = "";
            ddl_bils.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            tb_search.Text = "";
        }


    }
}