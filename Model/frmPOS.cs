using System;
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
    public partial class frmPOS : Form {
        public frmPOS() {
            InitializeComponent();
        }

        public int MainID = 0;
        public string OrderType;

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
        
        private void frmPOS_Load(object sender, EventArgs e) {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory() {
            string qry = "Select * from Category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            if (dt.Rows.Count > 0) {
                foreach (DataRow row in dt.Rows) {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(148, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();

                    // event for click
                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
                
            }
        }

        private void b_Click(object sender, EventArgs e) {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in ProductPanel.Controls) {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void Additems(string id, string name, string cat, string price, Image image) {
            var w = new ucProduct() {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = image,
                id = Convert.ToInt32(id)
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) => {
                var wdg = (ucProduct)ss;

                // this will check it product already there then a one to quauntity and update price
                foreach (DataGridViewRow item in guna2DataGridView1.Rows) {
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id) {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                            double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        GetTotal(); // 토탈 가격 갱신
                        return;
                    }
                }
                // this line add new product
                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }
        // getting product from database

        private void LoadProducts() {
            string qry = "Select * from products inner join category  on catID = categoryID ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows) {
                Byte[] imagearray = (byte[])item["pImage"];
                byte[] imagebytearray = imagearray;

                Additems(item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(), item["pPrice"].ToString(),  Image.FromStream(new MemoryStream(imagearray))); 
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e) {
            foreach (var item in ProductPanel.Controls) {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(tbSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            // for searil no
            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows) {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal() {
            int tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows) {
                tot += int.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lblTotal.Text = tot.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e) {
            lblTable.Text = "";
            lblTable.Visible = false;
            lblWaiter.Text = "";
            lblWaiter.Visible = false;

            guna2DataGridView1.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "00";
        }

        private void btnDelivery_Click(object sender, EventArgs e) {
            lblTable.Text = "";
            lblTable.Visible = false;
            lblWaiter.Text = "";
            lblWaiter.Visible = false;

            OrderType = "Delivery";
        }

        private void btnTakeAway_Click(object sender, EventArgs e) {
            lblTable.Text = "";
            lblTable.Visible = false;
            lblWaiter.Text = "";
            lblWaiter.Visible = false;

            OrderType = "Take Away";
        }
    }
}
