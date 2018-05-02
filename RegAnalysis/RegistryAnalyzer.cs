using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class RegistryAnalyzer
    {
        private MainWindow main_window;
        public List<string> SearchTerms;

        public RegistryAnalyzer(MainWindow main_win)
        {
            main_window = main_win;
            SearchTerms = new List<string>();
        }

        private string active_registry_location;
        public string ActiveRegistryLocation
        {
            get
            {
                return active_registry_location;
            }
            set
            {
                active_registry_location = value;
                main_window.ActiveRegistryLocationText.Dispatcher.Invoke(() =>
                {
                    main_window.ActiveRegistryLocationText.Text = value;
                });
            }
        }

        private string active_registry_value;
        public string ActiveRegistryValue
        {
            get
            {
                return active_registry_value;
            }
            set
            {
                active_registry_value = value;
                main_window.ActiveValueText.Dispatcher.Invoke(() =>
                {
                    main_window.ActiveValueText.Text = value;
                });
            }
        }                

        private int num_entries_recorded;
        public int NumEntriesRecorded
        {
            get
            {
                return num_entries_recorded;
            }
            set
            {
                num_entries_recorded = value;
                main_window.NumEntriesText.Dispatcher.Invoke(() =>
                {
                    main_window.NumEntriesText.Text = value.ToString();
                });               
            }
        }

        private int num_matching_entries;
        public int NumMatchingEntries
        {
            get
            {
                return num_matching_entries;
            }
            set
            {
                num_matching_entries = value;
                main_window.NumMatchesText.Dispatcher.Invoke(() =>
                {
                    main_window.NumMatchesText.Text = value.ToString(); 
                });
            }
        }

        public RegistryDataCollector[] CollectRegistryData(List<RegistryKey> keys_to_search)
        {
            RegistryDataCollector[] data_collectors = new RegistryDataCollector[keys_to_search.Count];
            for (int i = 0; i < keys_to_search.Count; i++)
            {
                data_collectors[i] = new RegistryDataCollector(keys_to_search[i], this);        
            }

            foreach (RegistryDataCollector collector in data_collectors)
            {
                collector.Run();
            }
            
            return data_collectors;
        }       

        public List<string> CheckValueMatch(string incoming_value)
        {
            List<string> matching_terms = new List<string>();
            foreach (string s_term in SearchTerms)
            {
                int test = incoming_value.IndexOf(s_term);
                int nothing = 0;
                if (incoming_value.IndexOf(s_term) == -1)
                {
                    continue;
                }
                else
                {
                    matching_terms.Add(s_term);
                }
            }

            return matching_terms; 
        }
    }
}
