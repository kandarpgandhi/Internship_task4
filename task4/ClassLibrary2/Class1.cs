using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary2
{
    public class Class1
    {
        public DataSet selectDatabyId(string str)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GI21EV1\SQLEXPRESS; Initial Catalog=ApiDatabase; Integrated Security=true;");
            con.Open();
            SqlCommand cmd = new SqlCommand(str,con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
            con.Close();
        }
    }
}
