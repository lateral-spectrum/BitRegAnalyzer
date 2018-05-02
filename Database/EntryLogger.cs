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
        public static MainWindow main_window;         

        public static void LogEntries(List<RegistryEntry> entries)
        {
            main_window.Dispatcher.Invoke(() =>
            {
                main_window.LogScrollViewer.Visibility = System.Windows.Visibility.Visible;
                main_window.LogScrollViewer.Content = "";
            });

            foreach (RegistryEntry entry in entries)
            {                                
                SQLiteCommand command = DatabaseManager.Connection.CreateCommand();
                command.CommandText = DBStringFormatter.GetEntryInsertStatement(entry, AnalysisRunLogger.CurrentRunID);
                command.ExecuteNonQuery();                
                main_window.LogScrollViewer.Dispatcher.Invoke(() =>
                {
                    main_window.LogScrollViewer.Content += command.CommandText + "\n";
                    main_window.LogScrollViewer.ScrollToBottom();
                });
            }
        }

        public static void LogEntry(RegistryEntry entry)
        {

        }        

        public static void LogMatchingEntry(RegistryEntry entry, string matching_field)
        {
            SQLiteCommand command = DatabaseManager.Connection.CreateCommand();
            command.CommandText = DBStringFormatter.GetMatchingEntryInsertStatement(entry, matching_field, AnalysisRunLogger.CurrentRunID);
            command.ExecuteNonQuery();
            Console.WriteLine("MATCH: " + command.CommandText);
            main_window.MainApp.Analyzer.NumMatchingEntries += 1;
        } 
    }

    
}
