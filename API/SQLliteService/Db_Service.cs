using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SQLliteService
{
    public static class Db_Service
    {
        public async static void InitializeDatabase()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                   new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    String tableCommand = "CREATE TABLE IF NOT " +
                        "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                        "Text_Entry NVARCHAR(2048) NULL)";

                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void PlaceBet(int placeBet)
        {
            try
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                    insertCommand.Parameters.AddWithValue("@Entry", placeBet);

                    insertCommand.ExecuteReader();

                    db.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public static List<String> GetData()
        {
            try
            {
                List<String> entries = new List<string>();

                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                   new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand selectCommand = new SqliteCommand
                        ("SELECT Text_Entry from MyTable", db);

                    SqliteDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        entries.Add(query.GetString(0));
                    }

                    db.Close();
                }

                return entries;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
