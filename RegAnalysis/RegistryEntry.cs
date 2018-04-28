using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public class RegistryEntry
    {
        public string Path;
        public string Value;
        public string RegistryLocation; 

        public RegistryEntry()
        {
            this.Path = "";
            this.Value = "";
            this.RegistryLocation = ""; 
        }

        public RegistryEntry(string path, string value, string registry_location)
        {
            this.Path = path;
            this.Value = value;
            this.RegistryLocation = registry_location; 
        }
    }
}
