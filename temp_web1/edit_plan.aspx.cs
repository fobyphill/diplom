using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class edit_plan : System.Web.UI.Page
    {
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               "C:\\Users\\phill\\documents\\plaza.accdb";
        string login_user;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id_plan = Request.QueryString["id_plan"];
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            //login_user = (string)Session["login_user"];
            login_user = "admin";
            /* if (login_user == null)
             { Response.Redirect("autorise.aspx"); }*/
            if (!Page.IsPostBack)
            {
                //Получим данные записи о планировании
                string q_plan = "select * from plans where id_plan = " + id_plan;
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_plan, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                dr.Read();
                string data_plan = dr[1].ToString();
                string value_plan = dr[2].ToString();
                string cat_plan = dr[3].ToString();
                string bil_plan = dr[4].ToString();
                string descript_plan = dr[5].ToString();
                dr.Close();
                com.Dispose();

                //Заполним поля категорий
                string q_cat = "select * from cats";
                //соединение с БД
                com = new OleDbCommand(q_cat, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[3].ToString() == "0")
                    {
                        TreeNode node_cat = new TreeNode(dr[1].ToString(), dr[0].ToString());
                        find_child(node_cat);
                        tv.Nodes.Add(node_cat);
                    }
                }
                //закрываем БД
                dr.Close();
                com.Dispose();

                //Выделим нужное значение категории
                foreach (TreeNode n in tv.Nodes)
                {
                    if (n.Value.ToString() == cat_plan)
                    {
                        n.Select();
                        break;
                    }
                    select_child(n, cat_plan);
                }

                //Заполняем комбобокс со счетами
                string q_bil = "select id_bil, name_bil from bils";
                com = new OleDbCommand(q_bil, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                if (ddl_bils.Items.Count == 0)
                {
                    l_bil.Text = "Создайте счет на странице \"Управление счетами\"";
                    l_bil.CssClass = "hint stress";
                }
                dr.Close();
                ole_con.Close();
                com.Dispose();

                ddl_bils.SelectedValue = bil_plan;//выводим текущий счет
                tb_value.Text = value_plan;//выводим значение в поле
                tb_descript.Text = descript_plan;//выводим комментарий


                //заполним комбобокс с годами
                DateTime dt = DateTime.Parse(data_plan);
                int year_selected = dt.Year;
                ddl_year.Items.Add(year_selected.ToString());
                int year_now = DateTime.Now.Year;
                year_now--;
                for (int i = 0; i < 5; i++)
                {
                    if (year_now != year_selected)
                    { ddl_year.Items.Add(year_now.ToString()); }
                    year_now++;
                }             

                //Выведем необходимый месяц
                int month_selected = DateTime.Parse(data_plan).Month;
                ddl_month.SelectedValue = month_selected.ToString();

            }
        }

        protected void ib_show_hide_Click(object sender, ImageClickEventArgs e)
        {
            if (l_collapse.Text == "Развернуть все")
            {
                tv.ExpandAll();
                l_collapse.Text = "Свернуть все";
            }
            else
            {
                tv.CollapseAll();
                l_collapse.Text = "Развернуть все";
            }
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string id_plan = Request.QueryString["id_plan"];
            OleDbConnection ole_con = new OleDbConnection(con_str);
            //Формируем запрос на изменение
            bool flag = false;// Флаг. Индикатор отсутствия ошибки
            //Получим обновленную дату планирования
            int year_edit = Int32.Parse(ddl_year.SelectedItem.Text);
            int month_edit = Int32.Parse(ddl_month.SelectedValue.ToString());
            DateTime dt = new DateTime(year_edit, month_edit, 1);
            string data_plan = dt.ToShortDateString();
            //Получим ID категории
            string cat_plan = "";
            if (tv.SelectedValue.ToString() == "")
            {
                l_cat.Text = "Укажите категорию";
                l_cat.Style.Add("color", "red");
                flag = true;
            }
            else
            { cat_plan = tv.SelectedValue.ToString(); }

            //Получим значение расхода
            float value_plan;
            tb_value.Text = tb_value.Text.Replace('.', ',');
            if (!Single.TryParse(tb_value.Text, out value_plan))
            {
                l_value.Text = "Укажите Корректное число";
                l_value.Style.Add("color", "red");
                flag = true;
            }
            else
            {
                tb_value.Text = value_plan.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
            
            string bil_plan = ddl_bils.SelectedValue;//Получим значение счета
            string descript_plan = tb_descript.Text;//описание плана

            //Объединим данные в переменной запроса
            string q_update_plan = "update plans set  data_plan ='" + data_plan + "', value_plan = "+value_plan+", cat_plan = "+cat_plan+
                ", bil_plan = "+bil_plan+", descript_plan = '"+ descript_plan+"', login_user = '"+login_user+"' where id_plan = "+id_plan;
            if (!flag)
            {
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_update_plan, ole_con);
                com.ExecuteNonQuery(); //Выполнить изменение данных в БД
                com.Dispose();
                ole_con.Close();
                System.Threading.Thread.Sleep(450);
                Response.Redirect("plans.aspx");

            }
        }

        protected void b_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("plans.aspx");
        }
        void find_child(TreeNode pn)
        {
            string id_con = Request.QueryString["id_con"];
            //соединились с БД
            string q_cat = "select * from cats";
            OleDbConnection ole_con = new OleDbConnection(con_str);
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
            dr.Close();
            com.Dispose();
            ole_con.Close();
        }

        void select_child(TreeNode n, string cat)
        {
            if (n.ChildNodes.Count > 0)
            {
                foreach (TreeNode n_ch in n.ChildNodes)
                {
                    if (n_ch.Value.ToString() == cat)
                    {
                        n_ch.Select();
                        break;
                    }
                    select_child(n_ch, cat);
                }
            }
        }
    }
}