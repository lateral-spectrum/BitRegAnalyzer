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
            // do insert for all records
            //DatabaseManager.Connection
            int run_id = GetGreatestRunId();
            
            foreach (RegistryEntry entry in entries)
            {
                
                //string insert_cmd = DBStringFormatter.GetEntryInsertStatement();
            }
        }

        public static void LogEntry(RegistryEntry entry)
        {

        }

        public static void LogMatchEntry(RegistryEntry entry)
        {

        }

        public static int GetGreatestRunId()
        {
            string stmt = "SELECT * FROM ANALYSES ORDER BY RUN_ID ASC";
            SQLiteCommand command = new SQLiteCommand(stmt, DatabaseManager.Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int run_id = reader.GetInt32(0);
                Console.WriteLine("Run id: " + run_id);
                //Console.WriteLine(reader.GetInt32(0) + " "
                //    + reader.GetString(1) + " " + reader.GetInt32(2));
            }

            int test = 0;


            return 0;
        }
    }
}
