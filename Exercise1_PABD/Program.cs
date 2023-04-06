using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1_PABD
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From pembeli", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
    }
}
