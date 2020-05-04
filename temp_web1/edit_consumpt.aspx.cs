using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class edit_consumpt2 : System.Web.UI.Page
    {
        string login_user, name_user, fam_user, status_user; // переменные для данных пользователя
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                "C:\\Users\\phill\\documents\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];
            status_user = (string)Session["status_user"];
            if (login_user == null)
            { Response.Redirect("autorise.aspx"); }
            if (!Page.IsPostBack)// Запускаем эту программу только в первый раз
            {
                string id_con = Request.QueryString["id_con"];
                if (status_user == "u" &&  !ver_user(id_con))//защита от попыток юзера исправить чужую запись
                { Response.Redirect("autorise.aspx"); }
                //Получим данные расхода
                string q_con = "select * from consumptions where id_con = " + id_con;
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_con, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                dr.Read();
                id_con = dr[0].ToString();//Заново получил ID расхода
                string data_create = dr[1].ToString(); //Получили дату создания расхода
                string value = dr[3].ToString();// Получили значение величины расхода
                string cat_id = dr[4].ToString();// Получили номер категории расхода
                string bil_con = dr[5].ToString();
                string descript = "";// Получаем значение описания расхода
                if (dr[6].ToString() != "")
                {
                    descript = dr[6].ToString();
                }
                dr.Close();
                com.Dispose();
                //Все необходимые данные расхода получены

                // Заполняем категории
                string q_cat = "select * from cats";
                com = new OleDbCommand(q_cat, ole_con);
                dr = com.ExecuteReader();
                //Заполняем категориями Дерево
                while (dr.Read())
                {
                    if (dr[3].ToString() == "0")
                    {
                        TreeNode node_cat = new TreeNode(dr[1].ToString(), dr[0].ToString());
                        //string parent_id = dr[0].ToString();
                        find_child(node_cat);
                        tv.Nodes.Add(node_cat);
                    }
                }
                dr.Close();
                //выеделение текущей категории
                foreach (TreeNode n in tv.Nodes)
                {
                    if (n.Value.ToString() == cat_id)
                    {
                        n.Select();
                        break;
                    }
                    select_child(n, cat_id);
                }

                tb_data.Text = DateTime.Parse(data_create).ToString("yyyy-MM-dd");

                tb_value.Text = value;//Выводим значения расхода
                tb_descript.Text = descript;//Выводим комментарий
                //заполняем поля счетов
                string q_bil = "select name_bil from bils";
                com = new OleDbCommand(q_bil, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(dr[0].ToString());
                }
                //выделим счет данного расхода
                for (int i = 0; i < ddl_bils.Items.Count; i++ )
                {
                    if (ddl_bils.Items[i].Text == bil_con)
                    {
                        ddl_bils.SelectedIndex = i;
                        break;
                    }
                }
                //Если счетов не задано, извещаем пользователя
                if (ddl_bils.Items.Count == 0)
                {
                    l_bil.Text = "Создайте счет на странице <a href='bils.aspx'>Управление счетами</a>";
                    l_bil.CssClass = "hint stress";
                }

                //Итак. все значения вывели. 
                ole_con.Close();
            }
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string id_con = Request.QueryString["id_con"];
            OleDbConnection ole_con = new OleDbConnection(con_str);
            //Формируем запрос на изменение
            bool flag = false;// Флаг. Индикатор отсутствия ошибки
            //Получим дату изменения
            string data_change = DateTime.Now.ToShortDateString();
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
            //Получим измененную дату создания заказа
            string data_create = tb_data.Text;

            //Получим значение расхода
            float value;
            tb_value.Text = tb_value.Text.Replace('.', ',');
            if (!Single.TryParse(tb_value.Text, out value))
            {
                l_value.Text = "Укажите Корректное число";
                l_value.Style.Add("color", "red");
                flag = true;
            }
            else
            {
                tb_value.Text = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
            //Получим значение счета
            string bil = ddl_bils.SelectedItem.Text;

            //Объединим данные в переменной запроса
            string q_update_con = "update consumptions set data_create='"+ data_create +"', data_change ='" +
            data_change + "', value_con = " + tb_value.Text + ", cat_con = " + num_cat +
            ", bil_con='" + bil + "', descript_con = '" + tb_descript.Text + "', change_login = '" +
            login_user + "' where id_con = " + id_con;

            if (!flag)
            {
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_update_con, ole_con);
                com.ExecuteNonQuery(); //Выполнить изменение данных в БД
                com.Dispose();
                ole_con.Close();
                System.Threading.Thread.Sleep(450);
                if (status_user == "a")
                { Response.Redirect("consumptions.aspx"); }
                else
                { Response.Redirect("cons_user.aspx"); }

            }
        }

        protected void b_cancel_Click(object sender, EventArgs e)
        {
            if (status_user == "a")
            { Response.Redirect("consumptions.aspx"); }
            else
            { Response.Redirect("cons_user.aspx"); }
        }
        void find_child(TreeNode pn)
        {
            //соединились с БД
            string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            string q_cat = "select * from cats";
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

        bool ver_user(string id)// проверка пользователя и даты заказа
        {
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            string q = "select create_login, data_change from consumptions where id_con = " + id;
            OleDbCommand com = new OleDbCommand(q, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                string dt = DateTime.Now.ToShortDateString();
                if (dr[0].ToString() == login_user && DateTime.Parse(dr[1].ToString()) == DateTime.Parse(dt))
                { return true; }
            }
            return false;

        }
    }
}