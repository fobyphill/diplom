using Microsoft.Reporting.WebForms;
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
    public partial class rept : System.Web.UI.Page
    {
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
            //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\plaza.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            string month = Request.QueryString["month"];
            string year = Request.QueryString["year"];
           // month = (Int32.Parse(month) + 1).ToString();
            if (!Page.IsPostBack)
            {
                rv.Reset();
                rv.ProcessingMode = ProcessingMode.Local;
                LocalReport lr = rv.LocalReport;
                lr.ReportPath = "rep_fast.rdlc";
                DataSet ds = new DataSet("ds_rep");
               
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                string q_all_cats = "select name_cat, id_cat, parent_id from cats";//Собираем список из главных категорий
                OleDbDataAdapter da = new OleDbDataAdapter(q_all_cats, ole_con);
                da.Fill(ds);
                ds.Tables[0].Columns.Add("summa");//Добавим столбец
                string q_report = "SELECT cats.name_cat, cats.id_cat, cats.parent_id, sum(consumptions.value_con) as summa " +
               "FROM consumptions INNER JOIN cats ON consumptions.cat_con=cats.id_cat " +
               "WHERE month(consumptions.data_create) = " + month + " and year(consumptions.data_create) = "+year+
               " GROUP BY cats.name_cat, cats.id_cat, cats.parent_id";// Получим нужные данные в запросе
                OleDbCommand com = new OleDbCommand(q_report, ole_con);
                OleDbDataReader dr = com.ExecuteReader();
                while (dr.Read())//Вставим данные в ДатаСэт на свои места
                {
                    foreach (DataRow drow in ds.Tables[0].Rows)
                    {
                        if (dr["id_cat"].ToString() == drow["id_cat"].ToString())
                        {drow["summa"] = dr["summa"];
                        break;
                        }
                    }
                }
                //собираем сумму расходов по главным категориям
               bool flag = true;//
               while(flag)
                {
                    flag = false;
                    foreach (DataRow drow in ds.Tables[0].Rows)
                    {
                        if (drow["parent_id"].ToString() != "0" && drow["summa"].ToString() !="")
                        {
                            foreach (DataRow drow_cut in ds.Tables[0].Rows)
                            {
                                if (drow_cut["id_cat"].ToString() == drow["parent_id"].ToString())
                                {
                                    float summa = 0;
                                    Single.TryParse(drow_cut["summa"].ToString(), out summa);
                                    summa += float.Parse(drow["summa"].ToString());
                                    drow_cut["summa"] = summa.ToString();
                                    drow["summa"] = "";
                                    break;
                                }
                            }
                            flag = true;
                        }
                    }
                }
                // Удаляем все записи, кроме главных категорий
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    if (drow["summa"].ToString() == "")
                    { drow.Delete(); }
                }
                ds.AcceptChanges();
                //обновим отчет, задав ему новый источник данных
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "ds_fast";
                rds.Value = ds.Tables[0];
                lr.DataSources.Add(rds);
            }
        }
        float summa_cons(DataSet d, string id)
        {
            float summa = 0;
            foreach (DataRow dr in d.Tables[0].Rows)
            {
                if (dr[2].ToString() == id)
                {
                    summa += float.Parse(dr[3].ToString());
                    summa += summa_cons(d, dr[1].ToString());
                }
            }
            return summa;
        }
    }
}