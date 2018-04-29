using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public class RegistryEntry
    {
        public string KeyName;
        public string Value;
        public string RegistryLocation; 

        public RegistryEntry()
        {
            this.KeyName = "";
            this.Value = "";
            this.RegistryLocation = ""; 
        }

        public RegistryEntry(string key_name, string value, string registry_location)
        {
            this.KeyName = key_name;
            this.Value = value;
            this.RegistryLocation = registry_location; 
        }
    }
}
