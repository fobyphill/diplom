using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace temp_web1
{
    public partial class cats : System.Web.UI.Page
    {
        string login_user, name_user, fam_user;
        char status_user; // переменные для данных пользователя

        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               "C:\\Users\\phill\\documents\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            login_user = (string)Session["login_user"];
           /* if (login_user == null)
            { Response.Redirect("autentific.aspx"); }*/
            if (!Page.IsPostBack)
            {
                string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               "C:\\Users\\phill\\documents\\plaza.accdb";
                string q_cat = "select * from cats";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_cat, ole_con);
                com.CommandType = CommandType.Text;//тип команды - текст
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
            }
        }

        protected void b_add_cat_Click(object sender, EventArgs e)
        {
            Session["flag_add_edit_cat"] = "a";
            p_add_edit.CssClass = "vis";
            l_parent_cat.Text = "Кликните по родителькой категории. <br />Пустая строка создаст корневую категорию";

        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            Session["flag_add_edit_cat"] = "e";
            p_add_edit.CssClass = "vis";
            l_parent_cat.Text = "родительская категория";
            l_cat.Text = "Выберите категорию для редактирования";
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {

        }

        protected void b_yes_Click(object sender, EventArgs e)
        {

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

        protected void tv_SelectedNodeChanged(object sender, EventArgs e)
        {
            if ((string)Session["flag_add_edit_cat"] == "a")
            { tb_parent_cat.Text = tv.SelectedNode.Text; }
            else if ((string)Session["flag_add_edit_cat"] == "e")// если нажата кнопка "редактировать"
            {
                tb_cat.Text = tv.SelectedNode.Text;
                TreeNode pn = tv.SelectedNode.Parent;
                tb_parent_cat.Text = pn.Text;
            }
        }

        protected void b_save_Click(object sender, EventArgs e)
        {
            string id_cat = "";
            string id_parent_cat = "";
            if ((string)Session["flag_add_edit_cat"] == "a")//найдем параметры запроса в случае добавления
            {
                //Получим ID категории
                string q_max_id = "SELECT Max(id_cat) FROM cats;";
                OleDbDataReader dr = my_query(q_max_id);
                dr.Read();
                int id = Int32.Parse(dr[0].ToString());
                id++;
                id_cat = id.ToString();
                dr.Close();

                //Получим ID Родительской категории
                if (tb_parent_cat.Text == "")
                { id_parent_cat = "0"; }
                else
                { id_parent_cat = tv.SelectedNode.Value.ToString(); }
            }

            if ((string)Session["flag_add_edit_cat"] == "e") // найдем параметры запроса в случае редактирования
            {
                //Получим ID категории
                id_cat = tv.SelectedNode.Value.ToString();

                //Получим ID родительской категории

                foreach (TreeNode n in tv.Nodes)
                {
                    if (n.Text == tb_parent_cat.Text)
                    {
                        Session["id_parent_cat"] = n.Value.ToString();
                        break;
                    }
                    else 
                    {
                        find_parent_cat(n, tb_parent_cat.Text);
                        if (Session["id_parent_cat"] != null)
                        { break; }
                    }
                }
                id_parent_cat = (string)Session["id_parent_cat"];
            }

            //запрос на добавление данных
            string ex_add = "INSERT INTO cats ( id_cat, name_cat, parent_id ) VALUES (" + id_cat + ", '" + tb_cat.Text + "', " +
                id_parent_cat + ")";
            //Вносим изменение в БД
       /*     OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(ex_add, ole_con);
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();
            System.Threading.Thread.Sleep(450);
            Response.Redirect(Request.RawUrl);*/
        }
        OleDbDataReader my_query(string q)//Процедура запроса данных из БД
        {
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q, ole_con);
            com.CommandType = CommandType.Text;//тип команды - текст
            OleDbDataReader dr = com.ExecuteReader();
            return dr;

        }
        void find_parent_cat(TreeNode n, string cat) //процедура нахождения категории
        {
            if (n.ChildNodes.Count > 0)
            {
                foreach (TreeNode n_ch in n.ChildNodes)
                {
                    if (n_ch.Text == cat)
                    {
                        Session["id_parent_cat"] = n_ch.Value.ToString();
                        break;
                    }
                    else {
                        find_parent_cat(n_ch, cat);
                        if (Session["id_parent_cat"] != null)
                        { break; }
                        }

                }

            }
        }// конец процедуры поиска категории
    }
}