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
            
            if (AdminPermissionChecker.IsAdministrator())
            {           
                AdminPermissionWarningLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                AdminPermissionWarningLabel.Visibility = Visibility.Visible;
            }            
        }      

        private void RunAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            SetUIMode(false);
            string term = TermTextBox.Text;            

            Console.WriteLine("Collecting registry data");            
            MainProgressBar.Value = 50;

            List<RegistryKey> keys_to_search = new List<RegistryKey>();
            if (Convert.ToBoolean(CheckBoxCurrentUser.IsChecked))
            {
                keys_to_search.Add(Registry.CurrentUser.OpenSubKey("SOFTWARE"));
            }
            if (Convert.ToBoolean(CheckBoxLocalMachine.IsChecked))
            {
                keys_to_search.Add(Registry.LocalMachine.OpenSubKey("SOFTWARE"));
            }

            Thread analysis_thread = new Thread(() => RegistryAnalyzer.CollectRegistryData(keys_to_search));
            analysis_thread.Start();

            while (analysis_thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            SetUIMode(true);
        }

        private void DoProgressBar()
        {
            //while (MainProgressBar.Value < 100)
            //{
            //    MainProgressBar.Value += 3;
            //    Thread.Sleep(750);
            //}            
        }

        public void SetUIMode(bool target_enabled)
        {
            RunAnalysisButton.IsEnabled = target_enabled;
            CheckBoxCurrentUser.IsEnabled = target_enabled;
            CheckBoxLocalMachine.IsEnabled = target_enabled;
            TermTextBox.IsEnabled = target_enabled;
        }
    }
}
