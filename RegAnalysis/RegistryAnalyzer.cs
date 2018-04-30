using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class RegistryAnalyzer
    {
        private MainWindow main_window; 

        public RegistryAnalyzer(MainWindow main_win)
        {
            main_window = main_win; 
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

                main_window.ActiveRegistryLocationText.Dispatcher.Invoke(() => {
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
                // do ui update
            }
        }
        
        public int NumTopLevelKeysToAnalyze;

        private int num_top_level_keys_analyzed;
        public int NumTopLevelKeysAnalyzed
        {
            get
            {
                return num_top_level_keys_analyzed;
            }
            set
            {
                num_top_level_keys_analyzed = value;              
                main_window.AnalysisProgressBar.Dispatcher.Invoke(() => {
                    main_window.AnalysisProgressBar.Value = NumTopLevelKeysAnalyzed / NumTopLevelKeysAnalyzed;
                });
            }
        }

        private int num_keys_recorded;
        public int NumKeysRecorded
        {
            get
            {
                return num_keys_recorded;
            }
            set
            {
                num_keys_recorded = value;
                // update ui
            }
        }

        public RegistryDataCollector[] CollectRegistryData(List<RegistryKey> keys_to_search)
        {                       
            RegistryDataCollector[] data_collectors = new RegistryDataCollector[keys_to_search.Count];
            for (int i = 0; i < data_collectors.Length; i++)
            {
                data_collectors[i] = new RegistryDataCollector(keys_to_search[i], this);                    
                data_collectors[i].Run();                
            }
            
            return data_collectors;
        }

        //public static List<RegistryLevelData> RegistryDataSearch(string term)
        //{
        //    List<RegistryLevelData> matching_levels = new List<RegistryLevelData>();

        //    return matching_levels;
        //}


    }
}
