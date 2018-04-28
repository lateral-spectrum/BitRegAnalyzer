using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class InaccessibleKey
    {
        public string KeyName;
        public string ParentKeyName; 

        public InaccessibleKey(string name, string parent_key_name)
        {
            KeyName = name;
            ParentKeyName = parent_key_name;
        }
    }
}
