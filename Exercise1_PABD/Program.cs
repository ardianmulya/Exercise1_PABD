using System;
using System.Collections.Generic;
using System.Data;
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

        public void insert(string id_pembeli, string nama_pembeli, string alamat, SqlConnection con)
        {
            string str = "";
            str = "insert into pembeli (id_pembeli,nama_pembeli,alamat)values(@id,@nama,@alamat)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id_pembeli));
            cmd.Parameters.Add(new SqlParameter("nama", nama_pembeli));
            cmd.Parameters.Add(new SqlParameter("alamat", alamat));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil ditambahkan");
        }

        public void delete(string id_pembeli, SqlConnection con)
        {
            string str = "";
            str = "delete from pembeli where id_pembeli = @id ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id_pembeli));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil dihapus");
        }

        public void search(string id_pembeli, SqlConnection con)
        {
            string str = "";
            str = "select * from pembeli where id_pembeli = @id ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id_pembeli));
            SqlDataReader r = cmd.ExecuteReader();
            Console.WriteLine("Data Berhasil dicari");
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

        public void update(string id_pembeli, string nama_pembeli, string alamat, SqlConnection con)
        {
            string str = "";
            str = "Update pembeli set nama_pembeli = @nama, alamat = @alamat where id_pembeli = @id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id_pembeli));
            cmd.Parameters.Add(new SqlParameter("nama", nama_pembeli));
            cmd.Parameters.Add(new SqlParameter("alamat", alamat));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil ditambahkan");
        }

    }
}
