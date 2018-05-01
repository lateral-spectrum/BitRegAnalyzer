using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BitRegAnalyzer
{
    public static class EntryLogger
    {
        public static void LogEntries(List<RegistryEntry> entries)
        {                        
            foreach (RegistryEntry entry in entries)
            {                                
                SQLiteCommand command = DatabaseManager.Connection.CreateCommand();
                command.CommandText = DBStringFormatter.GetEntryInsertStatement(entry, AnalysisRunLogger.CurrentRunID);
                command.ExecuteNonQuery();
                Console.WriteLine(command.CommandText);
            }
        }

        public static void LogEntry(RegistryEntry entry)
        {

        }        

        public static void LogMatchEntry(RegistryEntry entry, string matching_field)
        {
            
        }

        //public static void LogMatchingEntries(List<RegistryEntry> matching_entries)
        //{
        //    foreach (RegistryEntry match in matching_entries)
        //    {
        //        SQLiteCommand command = DatabaseManager.Connection.CreateCommand();
        //        command.CommandText = DBStringFormatter.GetMatchingEntryInsertStatement(match, AnalysisRunLogger.CurrentRunID);
        //        command.ExecuteNonQuery();
        //        Console.WriteLine(command.CommandText);
        //    }
        //}

        //public static int GetNextRunId()
        //{
        //    string stmt = "SELECT RUN_ID FROM ANALYSES ORDER BY RUN_ID ASC";
        //    SQLiteCommand command = new SQLiteCommand(stmt, DatabaseManager.Connection);
        //    SQLiteDataReader reader = command.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            int val = reader.GetInt32(0);
        //            Console.WriteLine("Reader: " + val);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Reader has no results.");
        //    }

        //    return 0; 
        //}

        //public static int GetGreatestRunId()
        //{
        //    string stmt = "SELECT * FROM ANALYSES ORDER BY RUN_ID ASC";
        //    SQLiteCommand command = new SQLiteCommand(stmt, DatabaseManager.Connection);
        //    SQLiteDataReader reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        int run_id = reader.GetInt32(0);
        //        Console.WriteLine("Run id: " + run_id);
        //        //Console.WriteLine(reader.GetInt32(0) + " "
        //        //    + reader.GetString(1) + " " + reader.GetInt32(2));
        //    }

        //    int test = 0;


        //    return 0;
        //}
    }
}
