using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    class MainClass
    {
        // Data Source     >>    교수용PC = 학교컴 , DESKTOP-KO5M0LU  =  집컴
        public static readonly string con_string = "Data Source=교수용PC; Initial Catalog=RM;Persist Security Info=True; User ID=sa; Password=std001;";
        public static SqlConnection con = new SqlConnection(con_string);
        
        // Method to check user validation
        public static bool IsvalidUser(string user, string pass) {
            bool isValid = false;

            string qry = @"Select * from users where username= '" + user + "' and upass = '" + pass + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0) {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            }

            return isValid;
        }
        // create property for username

        public static string user;

        public static string USER {
            get { return user; }
            private set { user = value; }
        }

        //methord for crud operation

        public static int SQL(string qry, Hashtable ht) {
            int res = 0;
            try {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht) {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                con.Close();
            }

            return res;
        }

        // for loading data from database

        public static void LoadData(string qry, DataGridView gv, ListBox lb) {
            // serial no in gridview
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++) {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                }

                gv.DataSource = dt;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows) {
                count++;
                row.Cells[0].Value = count;
            }
        }
        public static void BlurBackground(Form Model) {
            Form Background = new Form() {
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.5d,
                BackColor = Color.Black,
                Size = frmMain.Instance.Size,
                Location = frmMain.Instance.Location,
                ShowInTaskbar = false
            };

            Background.Show();
            Model.Owner = Background;
            Model.ShowDialog(Background);
            Background.Dispose(); // OK: 직접 만든 폼
        }
      

        // for cb fill
        public static void CBFill(string qry, ComboBox cb) {
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

    }
}
