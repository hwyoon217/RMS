using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM
{
    class MainClass
    {

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
            }

            return isValid;

            //  create main form
        }
    }
}
