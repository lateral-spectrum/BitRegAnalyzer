using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32; 

namespace BitRegAnalyzer
{
    public class RegistryLevelData
    {
        public RegistryKey Key;
        public List<String> ValueNames;
        public List<String> Values;
        public List<RegistryKey> SubKeys;
        public List<String> SubKeyNames;

        public RegistryLevelData(RegistryKey key)
        {
            this.Key = key;
            this.ValueNames = new List<String>();    
            this.Values = new List<String>();
            this.SubKeys = new List<RegistryKey>();
            this.SubKeyNames = new List<String>();
        }       
    }
}
