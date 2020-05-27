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
    public partial class test : System.Web.UI.Page
    {
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
         protected void Page_Load(object sender, EventArgs e)
        {
            
           if (!Page.IsPostBack)
           {
               string q_cat = "select * from cats";
               OleDbConnection ole_con = new OleDbConnection(con_str);
               ole_con.Open();
               OleDbCommand com = new OleDbCommand(q_cat, ole_con);
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
         void find_child(TreeNode pn)
         {
             //соединились с БД
             string q_cat = "select * from cats";
             OleDbConnection ole_con = new OleDbConnection(con_str);
             ole_con.Open();
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
             dr.Close(); com.Dispose(); ole_con.Close();
         }

         protected void b_choise_Click(object sender, EventArgs e)
         {
             l_out.Text = "БАБУЛЯ";
         }

         protected void tv_SelectedNodeChanged(object sender, EventArgs e)
         {
             l_out.Text = tv.SelectedNode.Text;
         }

    }
}