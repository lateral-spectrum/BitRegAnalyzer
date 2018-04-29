using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace BitRegAnalyzer
{
    public static class DatabaseManager
    {
        public static SQLiteConnection DatabaseConnection;

        public static void InitializeSchema()
        {
            string db_file_name = "analysis_data.sqlite";
            
            try
            {
                DatabaseConnection = new SQLiteConnection(string.Format(@"Data Source={0};Version=3;", db_file_name));
                DatabaseConnection.Open();
            }
            catch (Exception ex)
            {
                SQLiteConnection.CreateFile("data.sqlite");
                DatabaseConnection = new SQLiteConnection(string.Format(@"Data Source={0};Version=3;", db_file_name));
                DatabaseConnection.Open();
            }           

            string analyses_table_create_query = @"CREATE TABLE IF NOT EXISTS ANALYSES (
                    RUN_ID integer PRIMARY KEY,
                    INCLUDES_LOCAL_MACHINE_SOFTWARE TINY_INT NOT NULL,
                    INCLUDES_CURRENT_USER_SOFTWARE TINYINT NOT NULL            
                );";

            Console.WriteLine("Creating analyses table.");
            SQLiteCommand command = new SQLiteCommand(analyses_table_create_query, DatabaseConnection);
            command.ExecuteNonQuery();

            //SQLiteDataReader rdr = command.ExecuteReader();

            


        }
    }
}
