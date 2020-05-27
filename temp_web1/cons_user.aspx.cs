using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace temp_web1
{
	public partial class cons_user : System.Web.UI.Page
	{
        string login_user, fam_user, status_user; // переменные для данных пользователя
        //строка подключения
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";

		protected void Page_Load(object sender, EventArgs e)
		{
            //Зададим параметры пользователя
            login_user = (string)Session["login_user"];
            fam_user = (string)Session["fam_user"];
            status_user = (string)Session["status_user"];
            if (login_user == null)
            { Response.Redirect("autorise.aspx"); }

            if (!Page.IsPostBack)
            {
                //СОздаем запрос к БД
                string q_table = "SELECT * from cons_output where (cr_login = '" + login_user +
                    "' or ch_login = '"+login_user+"') and data_change = convert(date, GETDATE()) order by data_create desc";
                //СОздаем объект Оле - соединение с БД
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                //Выполняем запрос. Результат - массив в формате "Команда"
                OleDbCommand ole_com = new OleDbCommand(q_table, ole_con);
                OleDbDataReader dr = ole_com.ExecuteReader();
                //заполняем таблицу данными
                gv1.DataSource = dr;
                gv1.DataBind();
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
                Response.Redirect("edit_consumpt.aspx?id_con=" + id_con+"&count_rec=1");
            }
            else
            {p_error.Visible = true;}
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {
            if (gv1.SelectedIndex != -1)
            {
                mpe.Show();

            }
            else { p_error.Visible = true; }
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
            Response.Redirect(Request.RawUrl);
        }
	}
}