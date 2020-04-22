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
    public partial class cats : System.Web.UI.Page
    {
        string login_user, name_user, fam_user;
        char status_user; // переменные для данных пользователя

        protected void Page_Load(object sender, EventArgs e)
        {
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            login_user = (string)Session["login_user"];
            if (login_user == null)
            { Response.Redirect("autentific.aspx"); }
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

        }

        protected void b_change_Click(object sender, EventArgs e)
        {

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
    }
}