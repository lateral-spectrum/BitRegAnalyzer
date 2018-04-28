using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class RegistrySearch
    {
        public List<RegistryEntry> SearchResults;
        public List<RegistryLevelData> RegistryLevelDatas;
        public List<InaccessibleKey> InaccessibleKeysDatas;

        public RegistrySearch()
        {
            this.SearchResults = new List<RegistryEntry>();
            this.RegistryLevelDatas = new List<RegistryLevelData>();
        }

        public void AnalyzeRegistrySect(RegistryKey top_level_key)
        {
            Console.WriteLine("Reading Registry...");

            RecursivelyCollectKeyLevelData(top_level_key);                      
        }

        private RegistryLevelData CollectKeyLevelData(RegistryKey parent_key)
        {
            RegistryLevelData level_data = new RegistryLevelData(parent_key);

            string[] sub_key_names = parent_key.GetSubKeyNames();                  
            level_data.ValueNames = new List<string>(parent_key.GetValueNames());
            level_data.Values = new List<string>();            

            foreach (string vn in level_data.ValueNames)
            {
                string val = parent_key.GetValue(vn).ToString();
                level_data.Values.Add(val);
            }            

            return level_data;
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
                }                                
            }           
        }
        



    }
}
