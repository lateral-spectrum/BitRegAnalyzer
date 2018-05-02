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

        //public static int GetLatestRunId()
        //{
        //    SQLiteCommand command = DatabaseManager.Connection.CreateCommand();
        //    command.CommandText = "SELECT RUN_ID FROM ANALYSES ORDER BY RUN_ID DESC";
        //    SQLiteDataReader reader = command.ExecuteReader();
        //    List<int> run_ids = new List<int>();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            int value = reader.GetInt32(0);
        //            Console.WriteLine(value);
        //            run_ids.Add(value);
        //        }

        //        reader.Close();
        //        return run_ids[0];
        //    }
        //    else
        //    {
        //        Console.WriteLine("LatestRunId has defaulted");
        //        return 0;
        //    }            
        //}

        //public static void Create(Book book)
        //{
        //    SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO Book (Id, Title, Language, PublicationDate, Publisher, Edition, OfficialUrl, Description, EBookFormat) VALUES (?,?,?,?,?,?,?,?)", sql_con);
        //    insertSQL.Parameters.Add(book.Id);
        //    insertSQL.Parameters.Add(book.Title);
        //    insertSQL.Parameters.Add(book.Language);
        //    insertSQL.Parameters.Add(book.PublicationDate);
        //    insertSQL.Parameters.Add(book.Publisher);
        //    insertSQL.Parameters.Add(book.Edition);
        //    insertSQL.Parameters.Add(book.OfficialUrl);
        //    insertSQL.Parameters.Add(book.Description);
        //    insertSQL.Parameters.Add(book.EBookFormat);
        //    try
        //    {
        //        insertSQL.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
