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
        void AppStartup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("Application Startup Running");             

            MainWindow mainWindow = new MainWindow(this);         
            mainWindow.Show();           
        }
    }
}
