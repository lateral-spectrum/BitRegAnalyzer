using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public static class AnalysisRunLogger
    {
        public static int CurrentRunID;

        public static void LogNewRun()
        {                                    
            try
            {
                string time = new DateTime().ToShortDateString();
                SQLiteCommand command = new SQLiteCommand(DatabaseManager.Connection);
                command.CommandText = string.Format("INSERT INTO ANALYSES (DATE_RUN) VALUES (\"{0}\")", time);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string badness = ex.ToString();
                Console.WriteLine("Failed to log new analysis.");
            }
        }

        public static void UpdateCurrentRunID()
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(DatabaseManager.Connection);
                command.CommandText = "SELECT * FROM ANALYSES ORDER BY RUN_ID DESC";
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    List<int> run_ids = new List<int>();
                    while (reader.Read())
                    {
                        run_ids.Add(reader.GetInt32(0));
                    }
                    CurrentRunID = run_ids[0];
                }
                else
                {
                    CurrentRunID = 0; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't update current run id.");
            }
        }        
    }
}
