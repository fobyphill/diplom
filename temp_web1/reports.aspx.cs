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
            }
        }

        protected void b_print_Click(object sender, EventArgs e)
        {
            bool flag_error = false;
            string type_report;
            string month = DateTime.Parse(tb_month.Text).Month.ToString();//передаем месяц и год
            string year = DateTime.Parse(tb_month.Text).Year.ToString();
            string checked_cats = "";//перечень выделенных категорий
            if (rbl_choice_report.SelectedIndex == 1)
            {
                type_report = "custom";
                foreach (TreeNode tn in tv.Nodes)//Найдем выделенные категории
                {
                    if (tn.Checked)
                    {
                        checked_cats += tn.Value.ToString();
                        checked_cats += ",";
                    }
                    checked_cats += find_check_cats(tn);
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
                Response.Redirect("rept.aspx?type="+type_report+"&month=" + month + "&year=" + year + "&checked_cats=" + checked_cats);
            }
                
        }

        protected void rbl_choice_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_choice_report.SelectedIndex == 0)
            { 
                p_fast_report.Visible = true;
                p_custom_report.Visible = false;
            }
            else if (rbl_choice_report.SelectedIndex == 1)
            {
                p_fast_report.Visible = true;
                p_custom_report.Visible = true;
                l_cats.Text = "";
            }
        }

        protected void b_clear_Click(object sender, EventArgs e)
        {
            p_fast_report.Visible = false;
            p_custom_report.Visible = false;
            rbl_choice_report.SelectedIndex = -1;
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

        string find_check_cats(TreeNode n)
        {
            if (n.ChildNodes.Count > 0)
            {
                string s = "";
                foreach (TreeNode tn in n.ChildNodes)
                {
                    if (tn.Checked)
                    {
                        s += tn.Value.ToString();
                        s += ",";
                        s += find_check_cats(tn);
                    }
                    
                }
                return s;
            }
            else return "";
        }

    }
}