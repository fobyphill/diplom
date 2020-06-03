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
    public partial class reports : System.Web.UI.Page
    {
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                tb_month.Text = DateTime.Now.ToString("yyyy-MM");//Выведем текущий месяц по умолчанию
                //Выведем все категории в дерево
                string q_cat = "select * from cats";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbCommand com = new OleDbCommand(q_cat, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[3].ToString() == "0")
                    {
                        TreeNode node_cat = new TreeNode(dr[1].ToString(), dr[0].ToString());
                        //string parent_id = dr[0].ToString();
                        edit_consumpt2 ec2 = new edit_consumpt2();
                        ec2.find_child(node_cat);
                        tv.Nodes.Add(node_cat);
                    }
                }
                dr.Close(); ole_con.Close();
                //вкинем даты
                DateTime date_now = new DateTime();
                date_now = DateTime.Now;
                int mons = DateTime.Now.Month;
                mons--;
                int day = date_now.Day;
                int year_now = date_now.Year;
                DateTime date_before = new DateTime(year_now, mons, day);
                tb_date_to.Text = date_now.ToString("yyyy-MM-dd");
                tb_date_from.Text = date_before.ToString("yyyy-MM-dd");
            }
        }

        protected void b_print_Click(object sender, EventArgs e)
        {
            bool flag_error = false;
            string type_report, month = "", year = "";
            if (rbl_period_choise.SelectedIndex == 0)//если период выбран Месяц, то
            {
                month = DateTime.Parse(tb_month.Text).Month.ToString();//передаем месяц и год
                year = DateTime.Parse(tb_month.Text).Year.ToString();
                Session["method_date"] = "month";
            }
            else//если период произвольный - то
            {
                DateTime temp_date;
                if (DateTime.TryParse(tb_date_from.Text, out temp_date))
                { month = temp_date.ToShortDateString(); }
                if (DateTime.TryParse(tb_date_to.Text, out temp_date))
                {year = temp_date.ToShortDateString();}
                Session["method_date"] = "period";
            }
            string checked_cats = "";//перечень выделенных категорий
            if (rbl_choice_report.SelectedIndex == 1)
            {
                if (rbl_option.SelectedIndex == 0)
                { type_report = "only_cats"; }
                else { 
                        type_report = "with_include";
                        string list_cats = "";
                        foreach (TreeNode tn in tv.CheckedNodes)
                        {list_cats += find_check_and_child(tn);}
                        list_cats = list_cats.Substring(0, list_cats.Length - 1);
                        Session["list_cats"] = list_cats;
                    }
                foreach (TreeNode tn in tv.CheckedNodes)//Найдем выделенные категории
                {
                    checked_cats += tn.Value.ToString();
                    checked_cats += ",";
                }
                if (checked_cats == "")
                {
                    flag_error = true;
                    
                    l_cats.Text = "Не выбрана ни одна категория";
                    l_cats.CssClass = "stress";
                }
                else { checked_cats = checked_cats.Substring(0, checked_cats.Length - 1); }
            }
            else { type_report = "fast"; }
            if (!flag_error)
            {
                int count = 0;
                foreach (ListItem cb in cbl_con_plan.Items)
                {
                    if (cb.Selected == true)
                    { count++; }
                }
                if (count == 1 && cbl_con_plan.SelectedIndex == 0)//если одна галка и она затраты
                {
                    Response.Write("<script>window.open ('rept.aspx?type=" + type_report +
                    "&month=" + month + "&year=" + year + "&checked_cats=" + checked_cats + "','_blank');</script>");
                }
                else if (count == 1 && cbl_con_plan.SelectedIndex == 1)//если одна галка и она планирование
                {
                    Response.Write("<script>window.open ('rept_plan.aspx?type=" + type_report +
                    "&month=" + month + "&year=" + year + "&checked_cats=" + checked_cats + "','_blank');</script>");
                }
                else if (count == 2)//если обе галки - показываем комплексный отчет
                {
                    Response.Write("<script>window.open ('rept_complex.aspx?type=" + type_report +
                     "&month=" + month + "&year=" + year + "&checked_cats=" + checked_cats + "','_blank');</script>");
                }
                else
                {
                    l_con_plan.Text = "Не выбран тип отчета - по затратам, планам или комплексный";
                    l_con_plan.CssClass = "stress";
                }
            }
                
        }

        protected void rbl_choice_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_choice_report.SelectedIndex == 0)
            { 
                p_custom_report.Visible = false;
                p_period_choise.Visible = false;
                rbl_period_choise.SelectedIndex = 0;
                p_free_period.Visible = false;
                p_fast_report.Visible = true;
            }
            else 
            {
                p_custom_report.Visible = true;
                p_period_choise.Visible = true;
            }
        }

        protected void ib_show_hide_Click(object sender, ImageClickEventArgs e)
        {
            if (l_collapse.Text == "Развернуть все")
            {
                tv.ExpandAll();
                l_collapse.Text = "Свернуть все";
                ib_show_hide.CssClass = "checkbox_checked";
            }
            else
            {
                tv.CollapseAll();
                l_collapse.Text = "Развернуть все";
                ib_show_hide.CssClass = "checkbox_uncheck";
            }
        }

        protected void ib_check_Click(object sender, ImageClickEventArgs e)
        {
            if (l_check.Text == "Отметить все")
            {
                l_check.Text = "Снять все отметки";
                ib_check.CssClass = "checkbox_checked";
                foreach (TreeNode tn in tv.Nodes)
                {
                    tn.Checked = true;
                    check_tree(tn, true);
                }
                
            }
            else
            {
                l_check.Text = "Отметить все";
                ib_check.CssClass = "checkbox_uncheck";
                foreach (TreeNode tn in tv.Nodes)
                {
                    tn.Checked = false;
                    check_tree(tn, false);
                }
            }

        }

        void check_tree(TreeNode n, bool v)
        {
            if (n.ChildNodes.Count > 0)
            {
                foreach (TreeNode tn in n.ChildNodes)
                {
                    tn.Checked = v;
                    check_tree(tn, v);
                }
            }
        }

        string find_check_and_child(TreeNode n)
        {
            string s = n.Value.ToString()+",";
            if (n.ChildNodes.Count > 0)
            {
                foreach (TreeNode tn in n.ChildNodes)
                {
                    s += find_check_and_child(tn);
                }
            }
            return s;
        }

        protected void rbl_period_choise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_period_choise.SelectedIndex == 0)
            {
                p_fast_report.Visible = true;
                p_free_period.Visible = false;
            }
            else
            {
                p_free_period.Visible = true;
                p_fast_report.Visible = false;
            }
        }

        protected void cbl_con_plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbl_con_plan.Items[1].Selected)
            {
                rbl_period_choise.SelectedIndex = 0;
                var li = rbl_period_choise.Items.FindByValue("1");
                li.Enabled = false;
                p_fast_report.Visible = true;
                p_free_period.Visible = false;
            }
            else
            {
                rbl_period_choise.Items.FindByValue("1").Enabled = true;
            }
        }
    }
}