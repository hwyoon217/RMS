﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.Model {
    public partial class frmAddCustomer : Form {
        public frmAddCustomer() {
            InitializeComponent();
        }

        public string orderType = "";
        public int driverID = 0;
        public string cusName = "";
        public int mainID = 0;

        private void frmAddCustomer_Load(object sender, EventArgs e) {
            if (orderType== "Take Away") {
                lblDriver.Visible = false;
                cbDriver.Visible = false;
            }

            string qry = "select staffID 'id', sName 'name' from staff where sRole like 'Driver'";
            MainClass.CBFill(qry, cbDriver);

            if (mainID > 0) {
                cbDriver.SelectedValue = driverID;
            }
        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e) {
            driverID = Convert.ToInt32(cbDriver.SelectedValue);
        }
    }
}
