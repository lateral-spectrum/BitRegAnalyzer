using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public class ResultHtmlOutput
    {
        RegistryAnalyzer analyzer;
        string style_str;
        string head_str;
        string body_str;
        string html_str;
        string filename = "Report.html";

        public ResultHtmlOutput(RegistryAnalyzer lyzer)
        {
            analyzer = lyzer;            
            Create();
            Write();
            Open();
        }

        private void Create()
        {
            style_str = "<style></style>";
            head_str = "";
            body_str = "";
        
            foreach (RegistryDataCollector collector in analyzer.EntryCollectors)
            {
                body_str += string.Format("<h4>Subkey Results for: {0}</h4>", collector.TopLevelKey.ToString());
                foreach (RegistryEntry entry in collector.MatchingRegistryEntries)
                {
                    body_str += string.Format("<p>Entry: {0}, {1}</p>", entry.KeyName, entry.Value);
                }
            }

            html_str = string.Format(
                "<html><head>{0}</head><body>{1}</body></html>", head_str, body_str);

        }

        private void Write()
        {            
            var current_dir = Directory.GetCurrentDirectory();
            var report_dir = current_dir + "\\Reports";

            if (!Directory.Exists(report_dir))
            {
                Directory.CreateDirectory(report_dir);
            }
            
            File.WriteAllText(Path.Combine(report_dir, filename), html_str);
        }

        private void Open()
        {
            string file_dir = Directory.GetCurrentDirectory() + "\\Reports";
            System.Diagnostics.Process.Start(Path.Combine(file_dir, filename));            
        }        
    }
}
