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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rv.Reset();
                rv.ProcessingMode = ProcessingMode.Local;
                LocalReport lr = rv.LocalReport;
                lr.ReportPath = "rep_test.rdlc";
                DataSet ds = new DataSet("ds_rep");
                string q = "SELECT cats.name_cat, sum(consumptions.value_con) " +
                "FROM consumptions INNER JOIN cats ON consumptions.cat_con=cats.id_cat " +
                "WHERE month(consumptions.data_create)=month(date()) " +
                "GROUP BY cats.name_cat";
                string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    "C:\\Users\\phill\\documents\\plaza.accdb";
                OleDbConnection ole_con = new OleDbConnection(con_str);
                ole_con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(q, ole_con);
                da.Fill(ds);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "ds_full";
                rds.Value = ds.Tables[0];
                lr.DataSources.Add(rds);
            }

        }
    }
}