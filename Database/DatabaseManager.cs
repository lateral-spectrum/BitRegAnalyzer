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
        public static SQLiteConnection Connection;

        public static void InitializeSchema()
        {
            string db_file_name = "analysis_data.sqlite";

            try
            {
                Connection = new SQLiteConnection(string.Format(@"Data Source={0};Version=3;", db_file_name));
                Connection.Open();
            }
            catch (Exception ex)
            {
                SQLiteConnection.CreateFile("data.sqlite");
                Connection = new SQLiteConnection(string.Format(@"Data Source={0};Version=3;", db_file_name));
                Connection.Open();
            }

            SQLiteCommand command;

            string analyses_table_create = @"CREATE TABLE IF NOT EXISTS ANALYSES (
                    RUN_ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    DATE_RUN TIMESTAMP NULL
                );";

            Console.WriteLine("Creating analyses table.");

            command = new SQLiteCommand(analyses_table_create, Connection);
            command.ExecuteNonQuery();

            string entry_table_create = @"CREATE TABLE IF NOT EXISTS ENTRIES (
                    ENT_ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    RUN_ID INT(11),
                    KEY_NAME VARCHAR(1024),
                    VALUE VARCHAR(2048),
                    LOCATION VARCHAR(2048)                    
                );";

            Console.WriteLine("Creating analyses table.");

            command = new SQLiteCommand(entry_table_create, Connection);
            command.ExecuteNonQuery();
            
            string matching_entry_table_create = @"CREATE TABLE IF NOT EXISTS MATCHING_ENTRIES (
                    ENT_ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    RUN_ID INT(11),
                    KEY_NAME VARCHAR(1024),
                    VALUE VARCHAR(2048),
                    LOCATION VARCHAR(2048),
                    MATCHING_FIELD VARCHAR(20)
                );";

            Console.WriteLine("Creating matching table.");
            command = new SQLiteCommand(matching_entry_table_create, Connection);
            command.ExecuteNonQuery();

            //SQLiteDataReader rdr = command.ExecuteReader();            
        }
    }
}
