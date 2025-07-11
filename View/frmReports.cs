﻿using RM.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.View
{
    public partial class frmReports: Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e) {
            string qry = @"select * from products";

            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.con.Close();

            frmPrint frm = new frmPrint();
            rptMenu cr = new rptMenu();
            cr.SetDatabaseLogon("sa", "std001");
            //집에서 할 경우
            //cr.SetDatabaseLogon("sa", "std001","DESKTOP-KO5M0LU","RM");
            cr.SetDataSource(dt);

            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e) {
            string qry = @"select * from staff";

            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.con.Close();

            frmPrint frm = new frmPrint();
            rptStaffList cr = new rptStaffList();
            cr.SetDatabaseLogon("sa", "std001");
            //집에서 할 경우
            //cr.SetDatabaseLogon("sa", "std001","DESKTOP-KO5M0LU","RM");
            cr.SetDataSource(dt);

            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSaleCat_Click(object sender, EventArgs e) {
            frmSaleByCategory frm = new frmSaleByCategory();
            frm.ShowDialog();
        }
    }
}
