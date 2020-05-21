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
        string login_user, name_user, fam_user, status_user; // переменные для данных пользователя
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";
        string id_plan = "";
        
        

        protected void Page_Load(object sender, EventArgs e)
        {   
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];
            status_user = (string)Session["status_user"];
            /*if (status_user != "a")
            { Response.Redirect("autorise.aspx"); }*/

            if (!Page.IsPostBack)
            {
                string q_tab = "SELECT top 10 * from plans_output";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_tab, ole_con);
                 OleDbDataReader dr = com.ExecuteReader();// открыть БД
                gv.DataSource = dr;// прочесть данные
                gv.DataBind();// выбросить в таблицу
                dr.Close(); com.Dispose();
                //Заполним-ка комбобоксы "счета" и "пользователи" данными
                string q_bils = "select name_bil from bils";
                string q_users = "select login_user, status_user from users";
                com = new OleDbCommand(q_bils, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(dr[0].ToString());
                }
                com = new OleDbCommand(q_users, ole_con);
                dr = com.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {
                    ddl_user.Items.Add(dr[0].ToString());

                    if (dr[1].ToString() == "a")
                    {
                        ddl_user.Items[i].Attributes.Add("style", "background-color:Yellow");
                    }
                    i++;
                }
                ole_con.Close();
                ole_con.Close();// закрыть всех


            }

        }

        protected void b_add_con_Click(object sender, EventArgs e)
        {
            Response.Redirect("add_plan.aspx");
        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            int count_rec = 0;
            string id_plan = "";
            foreach (GridViewRow gvr in gv.Rows)
            {
                CheckBox chb = (CheckBox)gvr.FindControl("chb_table");
                if (chb.Checked)
                {
                    count_rec++;
                    id_plan += gvr.Cells[1].Text + ",";
                }
            }
            if (count_rec > 0)
            {
                id_plan = id_plan.Substring(0, id_plan.Length - 1);
                Response.Redirect("edit_plan.aspx?id_plan=" + id_plan + "&count_rec=" + count_rec);
            }
            else
            {
                p_error.Visible = true;
            }
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {
            int count_rec = 0;
            string id_plan = "";
            foreach (GridViewRow gvr in gv.Rows)
            {
                CheckBox chb = (CheckBox)gvr.FindControl("chb_table");
                if (chb.Checked)
                {
                    count_rec++;
                    id_plan += gvr.Cells[1].Text + ",";
                }
            }
            if (count_rec > 0)
            {
                id_plan = id_plan.Substring(0, id_plan.Length - 1);
                l_count_of_plans.Text = count_rec.ToString();
                Session["id_plan"] = id_plan;
                mpe.Show();

            }
            else
            {
                p_error.Visible = true;
            }
        }

        protected void b_yes_Click(object sender, EventArgs e)
        {
            string id_plan = Session["id_plan"].ToString();
            string q_plan = "delete from plans where id_plan in (" + id_plan + ")";
            //Создал запрос с нужным расходом.
            //Соединяюсь с БД
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_plan, ole_con);
            com.ExecuteNonQuery();
            com.Dispose();
            ole_con.Close();
            System.Threading.Thread.Sleep(500);
            Response.Redirect("plans.aspx");
        }

        protected void b_no_Click(object sender, EventArgs e)
        {
        }

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            l_hint_no_1.Visible = false;
        }

        protected void chb_table_CheckedChanged(object sender, EventArgs e)
        {
            p_error.Visible = false;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                CheckBox chb = (CheckBox)gv.Rows[i].FindControl("chb_table");
                if (chb.Checked)
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(22, 219, 219);
                }
                else if (i % 2 != 0)
                { gv.Rows[i].BackColor = System.Drawing.Color.White; }
                else { gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(227, 234, 235); }

            }
        }

        protected void chb_header_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chb_all = (CheckBox)gv.HeaderRow.FindControl("chb_header");
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                CheckBox chb = (CheckBox)gv.Rows[i].FindControl("chb_table");
                if (chb_all.Checked)
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(22, 219, 219);
                    chb.Checked = true;
                }
                else if (i % 2 != 0)
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.White;
                    chb.Checked = false;
                }
                else
                {
                    gv.Rows[i].BackColor = System.Drawing.Color.FromArgb(227, 234, 235);
                    chb.Checked = false;
                }
            }
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            string q_find_plans = "select * from plans_output where"; //основной запрос
            bool flag = false, flag_error = false;
            //Начало блока всех условий
            //блок даты затрат
            if (tb_date_create_begin.Text == "" && tb_date_create_end.Text != "")
            {
                flag = true;
                DateTime dt = new DateTime();
                dt = DateTime.Parse(tb_date_create_end.Text);
                q_find_plans += " data_plan <= '" + dt.ToShortDateString() + "'";
            }
            if (tb_date_create_begin.Text != "" && tb_date_create_end.Text == "")
            {
                flag = true;
                DateTime dtb = new DateTime();
                dtb = DateTime.Parse(tb_date_create_begin.Text);
                q_find_plans += " data_plan >= '" + dtb.ToShortDateString() + "'";
            }
            if (tb_date_create_begin.Text != "" && tb_date_create_end.Text != "")
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
                q_find_plans += " data_plan between '" + dtb.ToShortDateString() + "' and '" + dte.ToShortDateString() + "'";
            }
            //блок значений
            if (tb_value_bottom.Text == "" && tb_value_top.Text != "")
            {
                if (!try_parse_val(tb_value_top.Text))
                {
                    l_hint_no_1.Text = "Указано некорректное значение верхнего порога затрат. Введите число.";
                    p_error.Visible = true;
                    flag_error = true;
                }
                else
                {
                    if (flag) { q_find_plans += " and"; }
                    q_find_plans += " value_plan <= " + tb_value_top.Text;
                    flag = true;

                }
            }
            if (tb_value_bottom.Text != "" && tb_value_top.Text == "")
            {
                if (!try_parse_val(tb_value_bottom.Text))
                {
                    l_hint_no_1.Text = "Указано некорректное значение нижнего порога затрат. Введите число.";
                    p_error.Visible = true;
                    flag_error = true;
                }
                else
                {
                    if (flag) { q_find_plans += " and"; }
                    q_find_plans += " value_plan >= " + tb_value_bottom.Text;
                    flag = true;
                }
            }
            if (tb_value_bottom.Text != "" && tb_value_top.Text != "")
            {
                if (!try_parse_val(tb_value_bottom.Text) || !try_parse_val(tb_value_top.Text))
                {
                    l_hint_no_1.Text = "Указано некорректное значение затрат. Введите число.";
                    p_error.Visible = true;
                    flag_error = true;
                }
                else
                {
                    if (flag) { q_find_plans += " and"; }
                    float value_top = float.Parse(tb_value_top.Text);
                    float value_bottom = float.Parse(tb_value_bottom.Text);
                    if (value_top < value_bottom)
                    {
                        string temp_str = tb_value_top.Text;
                        tb_value_top.Text = tb_value_bottom.Text;
                        tb_value_bottom.Text = temp_str;
                    }
                    q_find_plans += " value_plan between " + tb_value_bottom.Text + " and " + tb_value_top.Text;
                    flag = true;
                }
            }
            //блок категорий
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
                    p_error.Visible = true;
                    flag_error = true;
                }
                else
                {
                    if (flag) { q_find_plans += " and"; }
                    q_find_plans += " name_cat = '" + tb_cats.Text + "'";
                    flag = true;
                }
                dr2.Dispose();com2.Dispose();ole_con2.Close();
            }
            //блок счетов
            if (ddl_bils.SelectedIndex > 0)
            {
                if (flag) { q_find_plans += " and"; }
                q_find_plans += " bil_plan = '" + ddl_bils.SelectedItem.Text + "'";
                flag = true;
            }
            //блок пользователей
            if (ddl_user.SelectedIndex > 0)
            {
                if (flag) { q_find_plans += " and"; }
                q_find_plans += " login_user = '" + ddl_user.SelectedItem.Text + "'";
                flag = true;
            }
            //блок айди + комменты
            if (try_parse_val(tb_search.Text))
            {
                int id = Int32.Parse(tb_search.Text);
                q_find_plans = "select * from plans_output where id_plan = " + id;
                flag = true;
            }
            else if (tb_search.Text.Length > 0)
            {
                if (flag) { q_find_plans += " and"; }
                q_find_plans += " descript_plan like '%" + tb_search.Text + "%'";
                flag = true;
            }
            //Конец блока всех условий, вывод таблицы
            string q_table = "select top 10 * from plans_output order by data_plan desc";
            if (!flag_error)
            {
                p_error.Visible = false;
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand();
                if (flag)
                {
                    com = new OleDbCommand(q_find_plans, ole_con);
                    l_click_left.Text = "Показаны результаты поиска";
                }
                else
                {
                    com = new OleDbCommand(q_table, ole_con);
                    l_click_left.Text = "Показаны последние 10 записей";
                }

                OleDbDataReader dr = com.ExecuteReader();
                gv.DataSource = null;
                gv.DataBind();
                gv.DataSource = dr;
                gv.DataBind();
                ole_con.Close();
            }
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
            tb_value_bottom.Text = "";
            tb_value_top.Text = "";
            tb_cats.Text = "";
            ddl_bils.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            tb_search.Text = "";
        }

        protected void ib_show_hide_search_Click(object sender, ImageClickEventArgs e)
        {
            if (p_search.Visible == true)
            {
                p_search.Visible = false;
                l_collapse.Text = "Показать поиск";
                ib_show_hide_search.CssClass = "checkbox_uncheck";
            }
            else
            {
                p_search.Visible = true;
                l_collapse.Text = "Скрыть поиск";
                ib_show_hide_search.CssClass = "checkbox_checked";
            }
        }

    }
}