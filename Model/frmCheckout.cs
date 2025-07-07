using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RM.Model
{
    public partial class frmCheckout: SampleAdd
    {
        public frmCheckout()
        {
            InitializeComponent();
        }

        public int amt;
        public int MainID = 0;

        private void txtReceived_TextChanged(object sender, EventArgs e) {
            int amt = 0;
            int receipt = 0;
            int change = 0;

            int.TryParse(txtBillAmount.Text, out amt);
            int.TryParse(txtReceived.Text, out receipt);

            change = receipt - amt;
            txtChange.Text = change.ToString();
        }

        public override void btnSave_Click(object sender, EventArgs e) {
            string qry = @"update tblMain set total = @total, received = @rec, change = @change, status = 'Paid'
                           where MainID = @id ";

            Hashtable ht = new Hashtable();
            ht.Add("@id", MainID);
            ht.Add("@total", txtBillAmount.Text);
            ht.Add("@rec", txtReceived.Text);
            ht.Add("@change", txtChange.Text);

            if (MainClass.SQL(qry, ht) >0) {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("Saved Successfully");
                this.Close();
            }
        }

        private void frmCheckout_Load(object sender, EventArgs e) {
            txtBillAmount.Text = amt.ToString();
        }
    }
}
