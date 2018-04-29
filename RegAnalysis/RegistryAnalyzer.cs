using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public static class RegistryAnalyzer
    {
        public static MainWindow Main_Window;

        private static string active_registry_location;
        public static string ActiveRegistryLocation
        {
            get
            {
                return active_registry_location;
            }
            set
            {
                active_registry_location = value;
                Main_Window.ActiveLocationLabel.Content = value; 
            }
        }

        private static string active_registry_value;
        public static string ActiveRegistryValue
        {
            get
            {
                return active_registry_value;
            }
            set
            {
                active_registry_value = value;
                Main_Window.ActiveValueLabel.Content = value; 
            }
        }

        public static RegistryDataCollector[] CollectRegistryData(List<RegistryKey> keys_to_search)
        {                       
            RegistryDataCollector[] data_collectors = new RegistryDataCollector[keys_to_search.Count];
            for (int i = 0; i < data_collectors.Length; i++)
            {
                data_collectors[i] = new RegistryDataCollector(keys_to_search[i]);                    
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
