using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public class RegistryKeyReadResult
    {
        public string path = "";
        public string[] keys = null;
        public string[] values = null;

        public RegistryKeyReadResult(string path, string[] keys, string[] values)
        {
            this.path = path;
            this.keys = keys;
            this.values = values;
        }
    }
}
