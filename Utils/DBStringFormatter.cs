using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public static class DBStringFormatter
    {
        public static string CreateInsertStringFromEntry(RegistryEntry entry, string table_name, int run_id)
        {
            string entry_str = string.Format(@"INSERT INTO {0} (RUN_ID, KEY_NAME, VALUE, LOCATION) VALUES ({1},{2},{3},{4})",
                table_name, run_id, entry.KeyName, entry.Value, entry.RegistryLocation);
            return entry_str;
        }
    }
}
