﻿using Microsoft.Reporting.WebForms;
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
    public partial class rept_plan : System.Web.UI.Page
    {
        string con_str = "Provider=SQLOLEDB;Data Source=PHILL-ПК\\SQLEXPRESS;Initial Catalog=plaza;Integrated Security=SSPI";
        string[] month_12 = { "январь", "февраль", "март", "апрель", "май",
                                    "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string type_report = Request.QueryString["type"];
                if (type_report == "fast")
                {
                    l_descript.Text = "планирование по главным категориям <br /> ";
                    fast_report();

                }
                else if (type_report == "only_cats")
                {
                    l_descript.Text = "планирование по выбранным категориям <br />";
                    only_cats_report();
                }
                else
                {
                    l_descript.Text = "планирование по выбранным и дочерним категориям <br />";
                    with_include_report();
                }
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

        void fast_report()
        {
            string month = Request.QueryString["month"];
            string year = Request.QueryString["year"];
            l_descript.Text += "за " + month_12[Int32.Parse(month) - 1] + " " + year + " года";
            DataSet ds = new DataSet();//Помню, не указал здесь имя ДатаСос
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            string q_all_cats = "select name_cat, id_cat, parent_id from cats";//Собираем список из главных категорий
            OleDbDataAdapter da = new OleDbDataAdapter(q_all_cats, ole_con);
            da.Fill(ds);
            DataColumn col_summa = new DataColumn("value_plan");
            col_summa.DataType = System.Type.GetType("System.Single");
            ds.Tables[0].Columns.Add(col_summa);//Добавим столбец
            string q_report = "SELECT cats.name_cat, cats.id_cat, cats.parent_id, sum(plans.value_plan) as value_plan " +
            "FROM plans INNER JOIN cats ON plans.cat_plan=cats.id_cat " +
            "WHERE month(plans.data_plan) = " + month + " and year(plans.data_plan) = " + year +
            " GROUP BY cats.name_cat, cats.id_cat, cats.parent_id";// Получим нужные данные в запросе
            OleDbCommand com = new OleDbCommand(q_report, ole_con);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())//Вставим данные в ДатаСэт на свои места
            {
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    if (dr["id_cat"].ToString() == drow["id_cat"].ToString())
                    {
                        drow["value_plan"] = dr["value_plan"];
                        break;
                    }
                }
            }
            //собираем сумму расходов по главным категориям
            bool flag = true;//
            while (flag)
            {
                flag = false;
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    if (drow["parent_id"].ToString() != "0" && drow["value_plan"].ToString() != "")
                    {
                        foreach (DataRow drow_cut in ds.Tables[0].Rows)
                        {
                            if (drow_cut["id_cat"].ToString() == drow["parent_id"].ToString())
                            {
                                float summa = 0;
                                Single.TryParse(drow_cut["value_plan"].ToString(), out summa);
                                summa += float.Parse(drow["value_plan"].ToString());
                                drow_cut["value_plan"] = summa.ToString();
                                drow["value_plan"] = System.DBNull.Value;
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
                if (drow["value_plan"].ToString() == "")
                { drow.Delete(); }
            }
            ds.AcceptChanges();//применили обновления в ДатаСэт
            //Обновляем отчет
            print_report(ds);
        }

        void only_cats_report()
        {
            string month = Request.QueryString["month"];
            string year = Request.QueryString["year"];
            string checked_cats = Request.QueryString["checked_cats"];
            DataSet ds = new DataSet();
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            //формируем запрос в соответствии с выбранным периодом - месяцем или произвольным
            string q_report;
            l_descript.Text += "за " + month_12[Int32.Parse(month) - 1] + " " + year + " года";
            q_report = "SELECT cats.name_cat, cats.id_cat,  sum(plans.value_plan) as value_plan " +
            "FROM plans INNER JOIN cats ON plans.cat_plan=cats.id_cat " +
            "WHERE month(plans.data_plan) = " + month + " and year(plans.data_plan) = " + year +
            " and cats.id_cat in(" + checked_cats + ") GROUP BY cats.name_cat, cats.id_cat";// Получим нужные данные в запросе
            

            OleDbDataAdapter da = new OleDbDataAdapter(q_report, ole_con);
            da.Fill(ds);
            print_report(ds);
        }

        void with_include_report()
        {
            string month = Request.QueryString["month"];
            string year = Request.QueryString["year"];
            string checked_cats = Request.QueryString["checked_cats"];
            string list_cats = Session["list_cats"].ToString();
            string[] array_cats = list_cats.Split(',');
            DataSet ds = new DataSet("ds_rep");
            OleDbConnection ole_con = new OleDbConnection(con_str);
            ole_con.Open();
            //Запросим перечень всех категорий, их айди, родительских айди + суммы затрат в указанный зером месяц, если затраты были
            string q_report = "";
            l_descript.Text += "за " + month_12[Int32.Parse(month) - 1] + " " + year + " года";
            q_report = "SELECT cats.name_cat, cats.id_cat, cats.parent_id, sum(plans.value_plan) as value_plan " +
            "FROM cats LEFT JOIN plans ON plans.cat_plan=cats.id_cat " +
            "and month(plans.data_plan) = " + month + " and year(plans.data_plan) = " + year +
            " GROUP BY cats.name_cat, cats.id_cat, cats.parent_id";// Получим нужные данные в запросе
            OleDbDataAdapter da = new OleDbDataAdapter(q_report, ole_con);
            da.Fill(ds);
            //Удалим лишние категории
            foreach (DataRow drow in ds.Tables[0].Rows)
            {
                bool flag_own = false;
                for (int i = 0; i < array_cats.Length; i++)
                {
                    if (array_cats[i] == drow[1].ToString())
                    { flag_own = true; break; }
                }
                if (!flag_own)
                { drow.Delete(); }
            }
            ds.AcceptChanges();
            //Укажем старшим категориям ID_par = 0
            foreach (DataRow drow in ds.Tables[0].Rows)
            {
                bool flag_parent = false;
                foreach (DataRow drow_parent in ds.Tables[0].Rows)
                {
                    if (drow[2].ToString() == drow_parent[1].ToString())
                    {
                        flag_parent = true;
                        break;
                    }
                }
                if (!flag_parent)
                { drow[2] = 0; }
            }
            //собираем сумму расходов по главным категориям
            bool flag = true;//
            while (flag)
            {
                flag = false;
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    if (drow["parent_id"].ToString() != "0" && drow["value_plan"].ToString() != "")
                    {
                        foreach (DataRow drow_cut in ds.Tables[0].Rows)
                        {
                            if (drow_cut["id_cat"].ToString() == drow["parent_id"].ToString())
                            {
                                float summa = 0;
                                Single.TryParse(drow_cut["value_plan"].ToString(), out summa);
                                summa += float.Parse(drow["value_plan"].ToString());
                                drow_cut["value_plan"] = summa.ToString();
                                drow["value_plan"] = System.DBNull.Value;
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
                if (drow["value_plan"].ToString() == "")
                { drow.Delete(); }
            }
            ds.AcceptChanges();//применили обновления в ДатаСэт
            //Обновляем отчет
            print_report(ds);
        }
        
        void print_report(DataSet dset)
        {
            rv.Reset();
            rv.ProcessingMode = ProcessingMode.Local;
            LocalReport lr = rv.LocalReport;
            lr.ReportPath = "rep_plan.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dset_plan";//поменять
            rds.Value = dset.Tables[0];
            lr.DataSources.Add(rds);
        }
    }
}