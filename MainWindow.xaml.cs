using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    public partial class MainWindow : Window
    {        
        public App MainApp; 

        public MainWindow(App main)
        {
            MainApp = main;
            InitializeComponent();            
        }      

        private void RunAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            //Thread LoadingBarThread = new Thread(DoProgressBar);
            //LoadingBarThread.Start();

            MainProgressBar.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(DoProgressBar));

            Console.WriteLine("Collecting registry data");
            RegistryAnalyzer.CollectRegistryData();
            
        }

        private void DoProgressBar()
        {
            while (MainProgressBar.Value < 100)
            {
                MainProgressBar.Value += 3;
                Thread.Sleep(750);
            }            
        }
    }
}
