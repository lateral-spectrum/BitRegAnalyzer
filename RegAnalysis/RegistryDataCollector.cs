using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Threading;

namespace BitRegAnalyzer
{    
    public class RegistryDataCollector
    {
        public RegistryKey TopLevelKey;        
        public List<RegistryEntry> RegistryEntries; 
        public List<string> InaccessibleEntries;

        private MainWindow main_window;
        private RegistryAnalyzer analyzer;

        public static bool CollectionCancelled;

        public RegistryDataCollector (RegistryKey top_level_key, RegistryAnalyzer lyzer)
        {
            analyzer = lyzer; 
            TopLevelKey = top_level_key;
            RegistryEntries = new List<RegistryEntry>();
            InaccessibleEntries = new List<string>();
            CollectionCancelled = false; 
        }          

        public void Run()
        {                        
            RecursivelyCollectKeyLevelData(TopLevelKey);
        }

        private void RecursivelyCollectKeyLevelData(RegistryKey key)
        {                          
            string[] sub_key_names = key.GetSubKeyNames();
            string[] value_names = key.GetValueNames();                        

            foreach (string vn in value_names)
            {
                string val = key.GetValue(vn).ToString();                
                RegistryEntry entry = new RegistryEntry();
                entry.KeyName = vn;
                entry.Value = val;
                entry.RegistryLocation = key.ToString();
                RegistryEntries.Add(entry);
                analyzer.NumEntriesRecorded += 1;

                analyzer.ActiveRegistryLocation = entry.RegistryLocation;
                analyzer.ActiveRegistryValue = val;

                List<string> matching_fields = analyzer.CheckValueMatch(val);
                if (matching_fields.Count != 0)
                {
                    List<RegistryEntry> matching_entries = new List<RegistryEntry>();
                    foreach (string mv in matching_fields)
                    {
                        
                    }
                    //EntryLogger.LogMatchingEntries(matching_entries);
                }                              
            }                        
            
            foreach (string sub_k in sub_key_names)
            {
                try
                {                    
                    RegistryKey sk = key.OpenSubKey(sub_k, false);                   
                    RecursivelyCollectKeyLevelData(sk);
                }
                catch (SecurityException ex)
                {                                                                             
                    string no_access_location = sub_k.ToString();
                    InaccessibleEntries.Add(no_access_location);
                }                                
            }           
        }
        
        
    }
}