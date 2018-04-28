using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public class RegistryAnalyzer
    {
        public void AnalyzeRegistry()
        {
            RegistrySearch searcher = new RegistrySearch();
            RegistryKey search_one_top_key = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            searcher.AnalyzeRegistrySect(search_one_top_key);
        }
    }
}
