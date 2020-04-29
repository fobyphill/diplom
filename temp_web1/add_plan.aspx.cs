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
    public partial class add_plan : System.Web.UI.Page
    {
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               "C:\\Users\\phill\\documents\\plaza.accdb";
        string login_user;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            //login_user = (string)Session["login_user"];
            login_user = "admin";
           /* if (login_user == null)
            { Response.Redirect("autorise.aspx"); }*/
            
            if (!Page.IsPostBack)
            {
                //Заполним поля категорий
                string q_cat = "select * from cats";
                //соединение с БД
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_cat, ole_con);
                //com.CommandType = CommandType.Text;
                OleDbDataReader dr = com.ExecuteReader();
                // конец соединения с БД
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
                ole_con.Close();
                com.Dispose();

                //Заполняем комбобокс со счетами
                string q_bil = "select id_bil, name_bil from bils";
                ddl_bils.Items.Add(new ListItem("Выберите счет", "null"));
                ole_con.Open();
                com = new OleDbCommand(q_bil, ole_con);
                com.CommandType = CommandType.Text;
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                if (ddl_bils.Items.Count == 0)
                {
                    l_bil.Text = "Создайте счет на странице <a href='bils.aspx'>Управление счетами</a>";
                    l_bil.CssClass = "hint stress";
                }
                dr.Close();
                ole_con.Close();
                com.Dispose();

                //заполним комбобокс с годами
                int year_now = DateTime.Now.Year;
                ddl_year.Items.Add(year_now.ToString());
                year_now--;
                ddl_year.Items.Add(year_now.ToString());
                year_now += 2;
                for (int i = 0; i<4; i++)
                {
                    ddl_year.Items.Add(year_now.ToString());
                    year_now++;
                }

                //Выведем текущий месяц
                ddl_month.SelectedValue = DateTime.Now.Month.ToString();

            }
        }
        protected void b_save_Click(object sender, EventArgs e)
        {
            bool flag_add = false;
            //Получим ID новой записи
            int id_plan;
            string q_max_id = "SELECT max(id_plan) from plans";// Запрос наибольшего айди
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_max_id, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (!Int32.TryParse(dr[0].ToString(), out id_plan))
            { id_plan = 0; }
            id_plan++;//внимание !!ЧИСЛО!!
            dr.Close();
            ole_con.Close();
            com.Dispose();
            

            //Получим дату плана
            if (ddl_month.SelectedValue.ToString() == "null")//Проверка месяца
            {
                flag_add = true;
                l_period.Style.Add("color", "red");
                l_period.Text = "Не указан месяц";
            }
            if (ddl_year.SelectedValue.ToString() == "null")// проверка года
            {
                if (flag_add == true)
                {
                    l_period.Text += ", не указан год.";
                    l_period.Style.Add("color", "red");
                }
                else
                {
                    l_period.Text = "Не указан год";
                    l_period.Style.Add("color", "red");
                    flag_add = true;
                }
            }
            string data_plan = "";
            if (!flag_add)
            {
                data_plan = new DateTime(Int32.Parse(ddl_year.SelectedItem.Text), Int32.Parse(ddl_month.SelectedValue), 1).ToShortDateString();
            }

            //Получим значение
            float value;
            string value_str = "";
            tb_value.Text = tb_value.Text.Replace('.', ',');
            if (!Single.TryParse(tb_value.Text, out value))
            {
                l_value.Text = "Укажите Корректное число";
                l_value.Style.Add("color", "red");
                flag_add = true;
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
                flag_add = true;
            }
            else
            { num_cat = tv.SelectedValue.ToString(); }

            //Заполним поле "комментарий"
            string descript_plan = "";
            if (tb_descript.Text != "")
            {
                descript_plan = tb_descript.Text;
            }

            //Добавим номер счета
            string bil = ddl_bils.SelectedValue.ToString();
            if (ddl_bils.Items.Count == 1)
            {
                flag_add = true;
                l_bil.Text = "У Вас нет счетов. Добавьте их на странице \"Счет\"";
                l_bil.Style.Add("color", "red");
            }
            else if (ddl_bils.SelectedValue == "null")
            {
                flag_add = true;
                l_bil.Text = "Счет не выбран.";
                l_bil.Style.Add("color", "red");
            }

            //Соберем запрос
            string q_add_plan = "INSERT INTO plans ( id_plan, data_plan, value_plan, cat_plan, bil_plan, descript_plan, login_user ) "+
            "VALUES ("+id_plan.ToString()+", '"+data_plan+"', "+value+", "+num_cat+", "+bil+", '"+descript_plan+"', '"+login_user+"')";
            if (!flag_add)
            {
                ole_con.Open();
                com = new OleDbCommand(q_add_plan, ole_con);
                com.ExecuteNonQuery();
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
        /*void exe_query(string q, OleDbConnection oc)
        {
            oc.Open();
            OleDbCommand com = new OleDbCommand(q, oc);
            com.CommandType = CommandType.Text;//тип команды - текст
            com.ExecuteNonQuery();
            oc.Close();
        }
        OleDbDataReader my_query(string q, OleDbConnection oc)
        {
            oc.Open();
            //ole_con.Open();
            OleDbCommand com = new OleDbCommand(q, oc);
            com.CommandType = CommandType.Text;//тип команды - текст
            OleDbDataReader dr = com.ExecuteReader();
        }*/

        void find_child(TreeNode pn)
        {
            //соединились с БД
            string q_cat = "select * from cats";
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q_cat, ole_con);
            com.CommandType = CommandType.Text;
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


    }
}