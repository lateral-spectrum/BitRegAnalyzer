using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BitRegAnalyzer
{   
    public partial class App : Application
    {
        public RegistryAnalyzer Analzer; 

        void AppStartup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("Application Startup Running");

            DatabaseManager.InitializeSchema();

            MainWindow main_window = new MainWindow(this);
            main_window.Show();                       
        }
    }
}
