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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Получение данных из сессии и возврат на страницу авторизации при окончании сессии
            string login_user = (string)Session["login_user"];
            /*if (login_user == null)
            { Response.Redirect("autentific.aspx"); }*/
            if (!Page.IsPostBack)
            {
                //Заполним поля категорий
                string q_cat = "select * from cats";
                OleDbDataReader dr = my_query(q_cat);
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

                //Заполняем комбобокс со счетами
                string q_bil = "select id_bil, name_bil from bils";
                dr = my_query(q_bil);
                while (dr.Read())
                {
                    ddl_bils.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
                if (ddl_bils.Items.Count == 0)
                {
                    l_bil.Text = "Создайте счет на странице <a href='bils.aspx'>Управление счетами</a>";
                    l_bil.CssClass = "hint stress";
                }
                //заполним комбобокс с годами
                int year_now = DateTime.Now.Year - 1;
                for (int i = -1; i<5; i++)
                {
                    ddl_year.Items.Add(new ListItem(year_now.ToString(), i.ToString()));
                    year_now++;
                }

            }
        }
        protected void b_save_Click(object sender, EventArgs e)
        {

        }

        protected void b_cancel_Click(object sender, EventArgs e)
        {

        }

        OleDbDataReader my_query(string q)
        {
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q, ole_con);
            com.CommandType = CommandType.Text;//тип команды - текст
            OleDbDataReader dr = com.ExecuteReader();
            return dr;
        }

        void find_child(TreeNode pn)
        {
            //соединились с БД
            string q_cat = "select * from cats";
            OleDbDataReader dr = my_query(q_cat);
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
        }


    }
}