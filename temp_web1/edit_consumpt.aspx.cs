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
    public partial class edit_consumpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)// Запускаем эту программу только в первый раз
            {
                string id_con = Request.QueryString["id_con"];
                //Получим данные расхода
                string q_con = "select * from consumptions where id_con = " + id_con;
                string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                "C:\\Users\\phill\\documents\\plaza.accdb";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_con, ole_con);
                com.CommandType = CommandType.Text;//тип команды - текст
                OleDbDataReader dr = com.ExecuteReader();
                dr.Read();
                id_con = dr[0].ToString();//Заново получил ID расхода
                string value = dr[3].ToString();// Получили значение величины расхода
                string cat_id = dr[4].ToString();// Получили номер категории расхода
                string descript = "";// Получаем значение описания расхода
                if (dr[5].ToString() != "")
                {
                    descript = dr[6].ToString();
                }
                string bil = dr[5].ToString();// Получили ID счета
                dr.Close();
                com.Dispose();
                //Все необходимые данные расхода получены

                // Заполняем категории
                string q_cat = "select * from cats";
                com = new OleDbCommand(q_cat, ole_con);
                com.CommandType = CommandType.Text;//тип команды - текст
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
                foreach(TreeNode n in tv.Nodes)
                {
                    if (n.Value.ToString() == cat_id)
                    { n.Select(); }
                }
                
                tb_value.Text = value;//Выводим значения расхода
                tb_descript.Text = descript;//Выводим комментарий
                //заполняем поля счетов
                string q_bil = "select id_bil, name_bil from bils";
                com = new OleDbCommand(q_bil, ole_con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ddl_bils.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                ddl_bils.SelectedValue = bil;// выводим текущий счет
                //Если счетов не задано, извещаем пользователя
                if (ddl_bils.Items.Count == 0)
                {
                    l_bil.Text = "Создайте счет на странице <a href='bils.aspx'>Управление счетами</a>";
                    l_bil.CssClass = "hint stress";
                }
                //Итак. все значения вывели. 
            }
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string id_con = Request.QueryString["id_con"];
            string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
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
            string bil = ddl_bils.SelectedValue;

            //Зададим пользователя, внесшего изменения. Пока что просто укажем админа
            string id_user = "1";
            //Объединим данные в переменной запроса
            string q_update_con = "update consumptions set data_change ='" +
            data_change + "', value_con = " + tb_value.Text + ", cat_con = " + num_cat +
            ", bil_con="+bil+", descript_con = '" + tb_descript.Text + "', change_id = " +
            id_user + " where id_con = " + id_con;
            OleDbCommand com = new OleDbCommand(q_update_con, ole_con);
            if (!flag)
            {
                com.ExecuteNonQuery(); //Выполнить изменение данных в БД
                com.Dispose();
                System.Threading.Thread.Sleep(700);
                Response.Redirect("consumptions.aspx");

            }
        }

        protected void b_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("consumptions.aspx");
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
            ole_con.Close();

        }
    }
}