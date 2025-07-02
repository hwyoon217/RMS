using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.Model {
    public partial class frmProductAdd : SampleAdd {
        public frmProductAdd() {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;

        private void frmProductAdd_Load(object sender, EventArgs e) {
            // for cb fill
            string qry = "select catID 'id', catName 'name' from category";

            MainClass.CBFill(qry, cbCat);

            if (cID > 0) {
                cbCat.SelectedValue = cID;
            }
            if (id > 0) {
                ForUpdateLoadData();
            }
        }

        string filepath;
        byte[] imageByteArray;
        private void btnBrowse_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK) {
                filepath = ofd.FileName;
                txtImage.Image = new Bitmap(filepath);
            }
        }

        public override void btnSave_Click(object sender, EventArgs e) {
            string qry = "";

            if (id == 0) {
                // insert
                qry = "Insert into products values (@Name, @Price, @Cat, @Image)";
            }
            else {
                // update
                qry = "Update products Set pName = @Name , pPrice = @Price, categoryID = @Cat, pImage = @image where pID = @id";
            }

            // for image
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Price", txtPrice.Text);
            ht.Add("@Cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@image", imageByteArray);

            if (MainClass.SQL(qry, ht) > 0) {
                guna2MessageDialog1.Show("Saved Successfully");  // 메시지박스 guna 버전으로 강화하기
                id = 0;
                cID = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = -1;
                txtImage.Image = RM.Properties.Resources.product_pic;
                txtName.Focus();
            }
        }

        private void ForUpdateLoadData() {
            string qry = @"select * from products where pID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0) {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();

                byte[] imageArray = (byte[])dt.Rows[0]["pImage"];
                byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
            }
        }
    }

}
