using IOService.Model;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace IOService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string card = "card";
            string company = "company";
            string sql = "";
            string name = "";
            DirectoryInfo place = new DirectoryInfo(@"C:\Users\Dec\source\repos\IOService\IOService\IOService\Files");

            var Files = place.GetFiles().OrderByDescending(o => o.Name);
            using (SqlConnection conn = new SqlConnection(@"Data Source=WORKSTANTION01\SQLEXPRESS; Integrated Security= true"))
            {
                conn.Open();
                var cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                Company companies = new Company();
                Card cards = new Card();

                foreach (FileInfo i in Files)
                {
                    var lineNumber = 0;
                    using (StreamReader reader = new StreamReader(i.FullName))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (lineNumber != 0)
                            {
                                var values = line.Split(';');
                                name = i.Name;
                                ///Проверить, выгружен ли уже файл
                                sql = "Select COUNT(*) FROM IntegrateServices.dbo.FileTransfer WHERE Name = '" + i.Name + "'";
                                cmd.CommandText = sql;
                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                                int countFile = (int)cmd.ExecuteScalar();

                                if (countFile > 0)
                                {
                                    Console.Write("Файл уже был выгружен");
                                }
                                else
                                {
                                    if (name.Contains(company))
                                    {
                                        sql = "INSERT INTO IntegrateServices.dbo.Company (ExternalId, Name, RegAdress) VALUES ('" + values[0] + "', + '" + values[1] + "', + '" + values[2] + "')";
                                        cmd.CommandText = sql;
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();

                                        if (cmd.ExecuteNonQuery() == 1)
                                        {
                                            sql = "INSERT INTO IntegrateServices.dbo.FileTransfer (Name, Status) VALUES ('" + name + "', + '" + "Успешно" + "')";
                                            cmd.CommandText = sql;
                                            cmd.Connection = conn;
                                            cmd.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            sql = "INSERT INTO IntegrateServices.dbo.FileTransfer (Name, Status) VALUES ('" + name + "', + '" + "Ошибка" + "')";
                                            cmd.CommandText = sql;
                                            cmd.Connection = conn;
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else if (name.Contains(card))
                                    {
                                        sql = "INSERT INTO IntegrateServices.dbo.Card (ShortName, Inn) VALUES ('" + values[0] + "', + '" + values[1] + "')";
                                        cmd.CommandText = sql;
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();

                                        if (cmd.ExecuteNonQuery() == 1)
                                        {
                                            sql = "INSERT INTO IntegrateServices.dbo.FileTransfer (Name, Status) VALUES ('" + name + "', + '" + "Успешно" + "')";
                                            cmd.CommandText = sql;
                                            cmd.Connection = conn;
                                            cmd.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            sql = "INSERT INTO IntegrateServices.dbo.FileTransfer (Name, Status) VALUES ('" + name + "', + '" + "Ошибка" + "')";
                                            cmd.CommandText = sql;
                                            cmd.Connection = conn;
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        ///Список файлов пуст
                                    }
                                }
                                
                            }

                            lineNumber++;
                        }
                    }
                }
                conn.Close();
            }

            Console.ReadLine();
        }
    }
}
