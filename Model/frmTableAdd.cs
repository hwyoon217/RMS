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
    public partial class frmTableAdd : SampleAdd {
        public frmTableAdd() {
            InitializeComponent();
        }

        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e) {
            string qry = "";

            if (id == 0) {
                // insert
                qry = "Insert into tables values(@Name)";
            }
            else {
                // update
                qry = "Update tables Set tname = @Name where tid = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQL(qry, ht) > 0) {
                guna2MessageDialog1.Show("Saved Successfully");  // 메시지박스 guna 버전으로 강화하기
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }
        }

    }
}
