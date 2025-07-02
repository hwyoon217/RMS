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

namespace RM.Model {
    public partial class frmStaffAdd : SampleAdd {
        public frmStaffAdd() {
            InitializeComponent();
        }

        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e) {

        }

        public override void btnSave_Click(object sender, EventArgs e) {
            string qry = "";

            if (id == 0) {
                // insert
                qry = "Insert into staff values (@Name, @Phone, @Role)";
            }
            else {
                // update
                qry = "Update staff Set sName = @Name , sPhone = @Phone, sRole = @Role where staffID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Phone", txtPhone.Text);
            ht.Add("@Role", cbRole.Text);

            if (MainClass.SQL(qry, ht) > 0) {
                guna2MessageDialog1.Show("Saved Successfully");  // 메시지박스 guna 버전으로 강화하기
                id = 0;
                txtName.Text = "";
                txtPhone.Text = "";
                cbRole.SelectedIndex = -1;
                txtName.Focus();
            }
        }
    }
}
