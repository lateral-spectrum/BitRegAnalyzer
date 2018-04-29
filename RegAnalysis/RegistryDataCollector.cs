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
        public List<InaccessibleKey> InaccessibleKeysDatas;

        public RegistryDataCollector (RegistryKey top_level_key)
        {
            TopLevelKey = top_level_key;
            RegistryEntries = new List<RegistryEntry>();
            InaccessibleKeysDatas = new List<InaccessibleKey>();            
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

                //Thread ui_update_thread = new Thread(() => { });
                RegistryAnalyzer.Main_Window.Dispatcher.Invoke(new Action(() =>
                {
                    RegistryAnalyzer.ActiveRegistryLocation = entry.RegistryLocation;
                    RegistryAnalyzer.ActiveRegistryValue = val;
                }), DispatcherPriority.ContextIdle);
                    
                Console.WriteLine(key.ToString() + " Value: " + val);
            }                        
            
            foreach (string sub_k in sub_key_names)
            {
                try
                {
                    RegistryKey sk = key.OpenSubKey(sub_k, false);
                    Console.WriteLine("Subkey: " + sk.ToString());
                    RecursivelyCollectKeyLevelData(sk);
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine("Couldn't access location: " + sub_k);                    
                    InaccessibleKey no_access_key = new InaccessibleKey(sub_k, key.Name);
                    InaccessibleKeysDatas.Add(no_access_key);
                }                                
            }           
        }        
    }
}