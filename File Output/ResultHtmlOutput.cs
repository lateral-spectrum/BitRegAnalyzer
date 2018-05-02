using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{    
    public class ResultHtmlOutput
    {
        RegistryAnalyzer analyzer;
        public ResultHtmlOutput(RegistryAnalyzer lyzer)
        {
            analyzer = lyzer;
            Create();
        }

        private void Create()
        {
            string style_str = "<style></style>";
            string head_str = string.Format("{0}", style_str);
            string body_str = ""; 

            string html_output_str = string.Format(
                "<html><head>{0}</head><body>{1}</body></html>", head_str, body_str);           
        }

        //private string GetEntryItemSection
    }
}
