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
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukan server tujuan : ");
                    string server = Console.ReadLine();
                    Console.WriteLine("Masukan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukan password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.WriteLine("\nketik y ntuk terhubung ke database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'y':
                            {
                                SqlConnection conn = null;
                                string strkoneksi = "data source = MSI\\{0}; " +
                                    "initial catalog = {1}; " +
                                    "user ID = {2}; password = {3}";
                                conn = new SqlConnection(string.Format(strkoneksi, server, db, user, pass));
                                conn.Open();
                                Console.Clear();

                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("4. Delete");
                                        Console.WriteLine("5. Search");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA PEMBELI\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA PEMBELI\n");
                                                    Console.WriteLine("Masukan ID : ");
                                                    string id_pembeli = Console.ReadLine();

                                                    Console.WriteLine("Masukan Nama Pembeli : ");
                                                    string nama = Console.ReadLine();
                                                    Console.WriteLine("Masukan Alamat Pembeli : ");
                                                    string Alamat = Console.ReadLine();
                                                    if (id_pembeli.Equals(pr.searchdata(id_pembeli, conn)))
                                                    {
                                                        Console.WriteLine("data sudah ada");
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            pr.insert(id_pembeli, nama, Alamat, conn);

                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data ");
                                                        }
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA YANG AKAN DIHAPUS\n");
                                                    Console.WriteLine("Masukan ID_PEMBELI : ");
                                                    string id_pembeli = Console.ReadLine();
                                                    Console.Write("apakah Yakin ingin menghapus data " + id_pembeli + "? (Y/N) : ");
                                                    string jwb = Console.ReadLine();
                                                    if (jwb.Equals("Y"))
                                                    {
                                                        try
                                                        {
                                                            pr.delete(id_pembeli, conn);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("\nAnda tidak memiliki akses untuk menghapus data");
                                                        }

                                                    }
                                                    else
                                                        break;
                                                }
                                                break;
                                            case '4':
                                                {
                                                    Console.WriteLine("INPUT DATA YANG AKAN DICARI\n");
                                                    Console.WriteLine("Masukan ID PEMBELI : ");
                                                    string id_pembeli = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.search(id_pembeli, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk mencari data");
                                                    }
                                                }
                                                break;
                                            case '5':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA YANG AKAN DI UBAH\n");
                                                    Console.WriteLine("Masukan ID PEMBELI : ");
                                                    string id_pembeli = Console.ReadLine();

                                                    Console.WriteLine("Masukan Nama Pembeli : ");
                                                    string Nama = Console.ReadLine();
                                                    Console.WriteLine("Masukan Alamat Pembeli : ");
                                                    string Alamat = Console.ReadLine();

                                                    pr.update(id_pembeli, Nama, Alamat, conn);

                                                }
                                                break;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
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

        public string searchdata(string id_pembeli, SqlConnection con)
        {
            string str = "";
            string nim = "";
            str = "select * from pembeli where id_pembeli = @id ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", id_pembeli));
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                nim = r.GetValue(0).ToString();
                Console.WriteLine();
            }
            r.Close();
            return nim;
        }
    }
}
