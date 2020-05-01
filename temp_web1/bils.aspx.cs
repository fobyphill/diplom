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
    public partial class bils : System.Web.UI.Page
    {
        string login_user, name_user, fam_user;
        char status_user; // переменные для данных пользователя
        string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            "C:\\Users\\phill\\documents\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Зададим параметры пользователя
            login_user = "admin";
            /*login_user = (string)Session["login_user"];
            name_user = (string)Session["name_user"];
            fam_user = (string)Session["fam_user"];*/

            //перебросим пользователя на экран авторизации по окончании сессии
            /*if (login_user == null)
            { Response.Redirect("autorise.aspx"); }*/

            if (!Page.IsPostBack)
            {
                string q_bils = "SELECT * from bils";
                
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_bils, ole_con);
                OleDbDataAdapter da = new OleDbDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //Передаю массив со счетами в сессию
                string[,] bils_data = new string[ds.Tables[0].Rows.Count, 3];
                int i = 0;
               foreach (DataRow drow in ds.Tables[0].Rows)
               {
                   for (int j = 0; j<3; j++)
                   {
                       bils_data[i, j] = drow.ItemArray[j].ToString();
                   }
                   i++;
               }
               Session["bils_data[,]"] = bils_data;
              // com.Dispose();
              // com = new OleDbCommand(q_bils,ole_con);
                //Заполняю листбокс названиями счетов
               OleDbDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {lb_bils.Items.Add(dr[0].ToString());}
                if (lb_bils.Items.Count == 0)
                { 
                    lb_bils.Text = "Счетов пока нет. Создайте новый счет";
                    lb_bils.Style.Add("color", "red");
                }
                com.Dispose(); ole_con.Close();// закрыть всех
            }
        }

        protected void b_add_con_Click(object sender, EventArgs e)
        {

        }

        protected void b_change_Click(object sender, EventArgs e)
        {
            if (lb_bils.SelectedIndex == -1)
            {
                l_hint_no_1.Visible = true;
            }
            else
            {
                string q_edit = "update bils set name_bil = '" + tb_name.Text + "', num_bil = '" + tb_num.Text + "', descript_bil = '" + tb_descript.Text + "'"+
                    "where name_bil = '"+lb_bils.SelectedItem.Text+"'";
                exe_query(q_edit);
                System.Threading.Thread.Sleep(450);
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void b_delete_Click(object sender, EventArgs e)
        {
            if (lb_bils.SelectedIndex == -1)
            {
                l_hint_no_1.Visible = true;
            }
            else
            {
                //Проверка, есть ли затраты или планирования, привязанные к указанному счету.
                string q_ver_con = "select count(*) from consumptions where bil_con = '" + lb_bils.SelectedItem.Text+"'";
                string q_ver_plan = "select count(*) from plans where bil_plan = '" + lb_bils.SelectedItem.Text + "'";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_ver_con, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                dr.Read();
                string kol_con = dr[0].ToString();
                dr.Close(); com.Dispose();
                com = new OleDbCommand(q_ver_plan, ole_con);
                dr = com.ExecuteReader();
                dr.Read();
                string kol_plan = dr[0].ToString();
                if (kol_con != "0")
                { l_click_left.Text = "Невозможно удалить счет. К нему привязаны записи о затратах в количестве "+kol_con+
             ". Перед удаланием счета удалите или перенесите записи о затратах на странице \"<a href='consumptions.aspx'>Управление затратами</a>\".";}
                if (kol_con != "0" && kol_plan != "0")
                {
                    l_click_left.Text += "<br />";
                    l_click_left.Text += "Невозможно удалить счет. К нему привязаны записи о планировании в количестве " + kol_plan +
          ". Перед удаланием счета удалите или перенесите записи о планировании затрат на странице \"<a href='plans.aspx'>Планирование затрат</a>\".";
                }
                else if (kol_plan != "0")
                {
                    l_click_left.Text = "Невозможно удалить счет. К нему привязаны записи о планировании в количестве " + kol_plan +
          ". Перед удаланием счета удалите или перенесите затраты на странице \"<a href='plans.aspx'>Планирование затрат</a>\".";
                }
                if (kol_plan =="0" && kol_con == "0")
                { mpe.Show(); }
            }
        }

        protected void b_yes_Click(object sender, EventArgs e)
        {
            string q_del = "delete from bils where name_bil = '" + lb_bils.SelectedItem.Text + "'";
            exe_query(q_del);
            System.Threading.Thread.Sleep(450);
            Response.Redirect(Request.RawUrl);
        }

        protected void b_no_Click(object sender, EventArgs e)
        {

        }

        protected void lb_bils_SelectedIndexChanged(object sender, EventArgs e)
        {
            l_click_left.Text = "Выберите счет из списка";
            l_hint_no_1.Visible = false;
            l_name.CssClass = "norm";
            l_name.Text = "Название счета";
            l_num.CssClass = "norm";
            l_num.Text = "Номер счета";
            tb_name.Text = lb_bils.SelectedItem.Text;
            string[,] bils_data = (string[,])Session["bils_data[,]"];
            for (int i = 0; i < bils_data.GetLength(0); i++)
            {
                if (bils_data[i,0] == lb_bils.SelectedValue.ToString())
                {
                    tb_descript.Text = bils_data[i, 2];
                    tb_num.Text = bils_data[i, 1];
                    break;
                }
            }

        }

        protected void b_clear_Click(object sender, EventArgs e)
        {
            tb_descript.Text = "";
            tb_name.Text = "";
            tb_num.Text = "";
        }

        protected void b_add_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (tb_name.Text == "")
            { 
                flag = true;
                l_name.Text = "Не указано имя счета.";
                l_name.CssClass = "stress";
            }
            if (tb_num.Text =="")
            {
                flag = true;
                l_num.Text = "Не указан номер счета";
                l_num.CssClass = "stress";
            }
            if (!flag)
            {
                string q_add = "INSERT INTO bils ( name_bil, num_bil, descript_bil ) VALUES ('" + tb_name.Text + "', '" + tb_num.Text + "', '" + tb_descript.Text + "')";
                exe_query(q_add);
                System.Threading.Thread.Sleep(450);
                Response.Redirect(Request.RawUrl);
            }
        }

        void exe_query(string q)
        {
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            OleDbCommand com = new OleDbCommand(q, ole_con);
            com.ExecuteNonQuery();
            ole_con.Close();
        }
    }
}