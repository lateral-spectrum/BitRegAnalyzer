using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{    
    public class RegistryDataCollector
    {
        public RegistryKey TopLevelKey;
        public List<RegistryLevelData> RegistryLevelDatas;
        public List<InaccessibleKey> InaccessibleKeysDatas;

        public RegistryDataCollector(RegistryKey top_level_key)
        {
            TopLevelKey = top_level_key;
            RegistryLevelDatas = new List<RegistryLevelData>();
            InaccessibleKeysDatas = new List<InaccessibleKey>();            
        }          

        public void Run()
        {
            RecursivelyCollectKeyLevelData(TopLevelKey);
        }

        private void RecursivelyCollectKeyLevelData(RegistryKey key)
        {
            RegistryLevelData level_data = new RegistryLevelData(key);

            string[] sub_key_names = key.GetSubKeyNames();
            level_data.ValueNames = new List<string>(key.GetValueNames());
            level_data.Values = new List<string>();

            foreach (string vn in level_data.ValueNames)
            {
                string val = key.GetValue(vn).ToString();
                level_data.Values.Add(val);
            }

            RegistryLevelDatas.Add(level_data);
            
            foreach (string sub_k in sub_key_names)
            {
                try
                {
                    RegistryKey sk = key.OpenSubKey(sub_k, false);
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