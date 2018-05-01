using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public static class DBStringFormatter
    {
        public static string GetEntryInsertStatement(RegistryEntry entry, int run_id)
        {
            string entry_key_name = string.Format(@"{0}", entry.KeyName);
            entry_key_name = FieldClean(entry_key_name);
            string entry_value = string.Format(@"{0}", entry.Value);
            entry_value = FieldClean(entry_value);
            string entry_location = string.Format(@"{0}", entry.RegistryLocation);
            entry_location = FieldClean(entry_location);

            string entry_str = string.Format("INSERT INTO {0} (RUN_ID, KEY_NAME, VALUE, LOCATION) VALUES ({1},\"{2}\",\"{3}\",\"{4}\");",
                "ENTRIES", run_id, entry_key_name, entry_value, entry_location);           
            return entry_str;
        }

        public static string GetMatchingEntryInsertStatement(RegistryEntry entry, string matching_field, int run_id)
        {
            string entry_key_name = string.Format("@{0}", entry.KeyName);
            entry_key_name = FieldClean(entry_key_name);
            string entry_value = string.Format(@"{0}", entry.Value);
            entry_value = FieldClean(entry_value);
            string entry_location = string.Format(@"{0}", entry.RegistryLocation);
            entry_location = FieldClean(entry_location);

            string insert_str = string.Format("INSERT INTO MATCHING_ENTRIES (RUN_ID, KEY_NAME, VALUE, LOCATION, MATCHING_FIELD) VALUES ({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\")", 
                run_id, entry_key_name, entry_value, entry_location, matching_field);

            return insert_str;
        }

        //public static string GetMatchingEntryInsertStatement(RegistryEntry entry, int run_id)
        //{
        //    string entry_key_name = string.Format(@"{0}", entry.KeyName);
        //    entry_key_name = FieldClean(entry_key_name);
        //    string entry_value = string.Format(@"{0}", entry.Value);
        //    entry_value = FieldClean(entry_value);
        //    string entry_location = string.Format(@"{0}", entry.RegistryLocation);
        //    entry_location = FieldClean(entry_location);

        //    string entry_str = string.Format("INSERT INTO {0} (RUN_ID, KEY_NAME, VALUE, LOCATION) VALUES ({1},\"{2}\",\"{3}\",\"{4}\");",
        //        "MATCHING_ENTRIES", run_id, entry_key_name, entry_value, entry_location);
        //    return entry_str;
        //}

        public static string FieldClean(string org_str)
        {
            org_str = org_str.Replace("\\", "\\\\");
            org_str = org_str.Replace("\"", "");
            return org_str;
        }
    }
}
