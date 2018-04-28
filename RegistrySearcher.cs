using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class RegistrySearch
    {
        public List<RegistryEntry> SearchResults;
        public List<RegistryLevelData> RegistryLevelDatas;

        public RegistrySearch()
        {
            this.SearchResults = new List<RegistryEntry>();
        }

        public void SearchRegistrySect(RegistryKey top_level_key, string search_value)
        {
            Console.WriteLine("Reading Registry...");
            List<String> found_values = new List<String>();

            List<RegistryLevelData> sect_levels_data = new List<RegistryLevelData>();

            string[] sub_key_names = top_level_key.GetSubKeyNames();
            foreach (string s in sub_key_names)
            {
                Console.WriteLine("Found subkey: " + s);
                RegistryKey sub_key = top_level_key.OpenSubKey(s);

                RegistryLevelData level_data = CollectKeyLevelData(sub_key);
                Console.WriteLine("Got {0} values for this level.", level_data.Values.Count);
                Console.WriteLine("Got {0} subkeys for this level.", level_data.SubKeys.Count);
                sect_levels_data.Add(level_data);
            }

            Console.WriteLine("Comparing keys and values...");
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
                RegistryKey sk = key.OpenSubKey(sub_k, false);

                RecursivelyCollectKeyLevelData(sk);
            }

            RegistryLevelDatas.Add(level_data);
        }

        //private void CollectKeyLevelData(RegistryKey key)
        //{
        //    RegistryKey next_key = top_level_key.OpenSubKey(key);
        //    string[] next_sub_key_names = next_key.GetSubKeyNames();
        //    if (next_sub_key_names.Length > 0)
        //    {
        //        Console.WriteLine("Found {0} more subkeys.", next_sub_key_names.Length);
        //        foreach (string key_name in next_sub_key_names)
        //        {

        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        public void DoSearchOne()
        {            
            Registry.LocalMachine.OpenSubKey("SOFTWARE");
            RegistryKey software_key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EASEUS", false);

            string[] key_names = software_key.GetValueNames();
            key_names[0] = null;

            string[] values = new string[key_names.Length];
            for (int k = 0; k < key_names.Length; k++)
            {
                values[k] = software_key.GetValue(key_names[k]).ToString();
                Console.WriteLine(string.Format("Key Name: {0}, Value {1}", key_names[k], values[k]));
            }

            RegistryKeyReadResult result = new RegistryKeyReadResult("path", key_names, values);
        }
        
        public void SearchRegistryKey(string path)
        {
            string[] parts = path.Split('\\');
            switch (parts[0])
            {
                case "SOFTWARE":
                    break;
                case "CURRENT_USER":
                    break;
                default:
                    break;
            }
        }



    }
}
