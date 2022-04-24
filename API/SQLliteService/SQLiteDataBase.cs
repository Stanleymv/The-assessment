
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using API.Classes;
using System.Data;

namespace API.SQLliteService
{
    public  static class SQLiteDataBase
    {

        private static SQLiteConnection sqlite_conn;
        private static SQLiteConnection con;
        private static SQLiteCommand cmd;
        private static SQLiteDataReader dr;
        private static MessageResponse response = new MessageResponse();
        public static void Init()
        {
            try
            {
               
                sqlite_conn = CreateConnection();
                CreateTable();


            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        public static SQLiteConnection CreateConnection()
        {
            

            try
            {
                SQLiteConnection sqlite_conn;

                if (!File.Exists("Roulette.sqlite"))
                {
                    SQLiteConnection.CreateFile("Roulette.sqlite");

                    con = new SQLiteConnection("Data Source=Roulette.sqlite;Version=3;");
                    con.Open();

                }
                else
                {
                    con = new SQLiteConnection("Data Source=Roulette.sqlite;Version=3;");
                    con.Open();
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToArray();
            }

            return con;
        }


        public static MessageResponse CreateTable()
        {
            try
            {
                
                string Createsql = "CREATE table IF NOT EXISTS Bets (PlaceBet int)";
                string Createsql1 = "CREATE table IF NOT EXISTS PreviousSpins (Spin int)";

                cmd = con.CreateCommand();
                cmd.CommandText = Createsql;

                cmd.ExecuteNonQuery();

                cmd = con.CreateCommand();
                cmd.CommandText = Createsql1;

                cmd.ExecuteNonQuery();

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccessfful = false;
                response.ErrorMessage = string.Format("Error {0}", ex.Message.ToString());
                return response;
            }

           
        }

        public static MessageResponse Place_Bet( int placeBet)
        {
           
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Bets (PlaceBet) Values (@placeBet)";
                cmd.Parameters.AddWithValue("@placeBet", placeBet);

                int result = cmd.ExecuteNonQuery();

                if (result == -1)
                {
                    response.IsSuccessfful = false;
                    response.ErrorMessage = string.Format("Error {0}", result.ToString());
                    return response;
                }
                else
                {
                    response.IsSuccessfful = true;
                    response.IsSuccessfulMessage = string.Format("OK {0}", result.ToString());
                    return response; ;
                }
               
            }
            catch (Exception ex)
            {
                response.ErrorMessage = string.Format("Exception: {0}", ex.Message.ToString()).ToString();
                response.IsSuccessfful = false;
                return response;
            }

            return response;
        }

        public static MessageResponse Spins( int spin)
        {
            try
            {
               
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO PreviousSpins (Spin) Values (@spin)";
                cmd.Parameters.AddWithValue("@spin", spin);

                cmd.ExecuteNonQuery();

                int result = cmd.ExecuteNonQuery();

                if (result == -1)
                {
                    response.IsSuccessfful = false;
                    response.ErrorMessage = string.Format("Failed to spin: {0}",result);
                    return response;
                }
                else
                {
                    response.IsSuccessfful = true;
                    response.ErrorMessage = string.Format("Spin was successfful: {0}", result);
                    return response;
                }

            }
            catch (Exception ex)
            {
               
                response.IsSuccessfful = true;
                response.ErrorMessage = string.Format("Exception error: {0}", ex.Message.ToString());
                return response;
            }

            return response;
        }


        public static MessageResponse Previous_Spins()
        {
           
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PreviousSpins";

                dr = cmd.ExecuteReader();

               
                if (dr == null)
                {
                    response.ErrorMessage = string.Format("Something went wrong: {0}", dr);
                    response.IsSuccessfful = false;
                    return response;
                }
                else
                {

                    while (dr.Read())
                    {
                        ReadSingleRow((IDataRecord)dr);

                        response.Message = string.Format("results: {0}", ReadSingleRow((IDataRecord)dr));
                    }

                    dr.Close();

                    response.IsSuccessfful = true;
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = string.Format("Exception error: {0}", ex.Message.ToString());
                response.IsSuccessfful = false;
                return response;
            }

            return response;
        }

        public static string ReadSingleRow(IDataRecord dataRecord)
        {
            return string.Format(String.Format("{0}", dataRecord[0]));
        }


    }
}
