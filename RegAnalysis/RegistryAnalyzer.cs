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
        public static void CollectRegistryData()
        {            
            RegistryKey[] keys_to_search = new RegistryKey[]
            {
                Registry.LocalMachine.OpenSubKey("SOFTWARE")
            };

            RegistryDataCollector[] data_collectors = new RegistryDataCollector[keys_to_search.Length];
            for (int i = 0; i < data_collectors.Length; i++)
            {
                data_collectors[i] = new RegistryDataCollector(keys_to_search[i]);                    
                data_collectors[i].Run();
            }
            
            int test = 0; 
        }


    }
}
