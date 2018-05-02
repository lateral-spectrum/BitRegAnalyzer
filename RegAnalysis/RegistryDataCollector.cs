using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Threading;
using System.IO;

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
                string val = key.GetValue(vn).ToString();// deped
                var value = key.GetValue(vn);
                var type = key.GetValue(vn).GetType().ToString();
                Console.WriteLine(type);

                if (type == "System.Byte[]")
                {
                    byte[] v = (byte[])value;
                    //byte[] bytes = Encoding.ASCII.GetBytes("orrent");
                    byte[] bytes = Encoding.UTF8.GetBytes("orrent");
                    string hex_string = ByteStringConverter.HexStringFromByteArr(bytes);
                    int test = 0; 
                    //string byte_string = ByteStringConverter.ByteArrayToString(v);
                    //string ByteStringConverter.Bytes

                    ////int test = 0;

                    //byte[] ByteStringConverter.StringToByteArray("006f007200720065006e0074");

                }

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
                    foreach (string mtf in matching_fields)
                    {
                        EntryLogger.LogMatchingEntry(entry, mtf);
                    }                    
                }
                matching_fields = analyzer.CheckValueMatch(vn);
                if (matching_fields.Count != 0)
                {
                    foreach (string mtf in matching_fields)
                    {
                        EntryLogger.LogMatchingEntry(entry, mtf);
                    }
                }
                matching_fields = analyzer.CheckValueMatch(entry.RegistryLocation);
                if (matching_fields.Count != 0)
                {
                    foreach (string mtf in matching_fields)
                    {
                        EntryLogger.LogMatchingEntry(entry, mtf);
                    }
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