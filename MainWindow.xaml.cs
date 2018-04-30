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

            List<RegistryKey> keys_to_search = new List<RegistryKey>();
            if (Convert.ToBoolean(CheckBoxCurrentUser.IsChecked))
            {
                keys_to_search.Add(Registry.CurrentUser.OpenSubKey("SOFTWARE"));
            }
            if (Convert.ToBoolean(CheckBoxLocalMachine.IsChecked))
            {
                keys_to_search.Add(Registry.LocalMachine.OpenSubKey("SOFTWARE"));
            }

            //RegistryDataCollector.CollectionCancelled = false;
            CancelAnalysisButton.IsEnabled = true;
            CancelAnalysisButton.Visibility = Visibility.Visible;

            MainApp.Analzer = new RegistryAnalyzer(this);
            Thread collection_thread = new Thread(new ThreadStart(() => {
                MainApp.Analzer.CollectRegistryData(keys_to_search);
            }));

            collection_thread.Start();

            //SetUIMode(true); // needs to be in callback
        }       

        public void SetUIMode(bool target_enabled)
        {            
            if (target_enabled)
            {
                RunAnalysisButton.Visibility = Visibility.Hidden;
                RunAnalysisButton.IsEnabled = false; 
                CancelAnalysisButton.Visibility = Visibility.Visible;
                CancelAnalysisButton.IsEnabled = true; 
            }
            else
            {
                RunAnalysisButton.Visibility = Visibility.Visible;
                RunAnalysisButton.IsEnabled = true;
                CancelAnalysisButton.Visibility = Visibility.Hidden;
                CancelAnalysisButton.IsEnabled = false; 
            }

            CheckBoxCurrentUser.IsEnabled = target_enabled;
            CheckBoxLocalMachine.IsEnabled = target_enabled;

            TermTextBox.IsEnabled = target_enabled;          
        }        

        private void OutputHtmlButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelAnalysisButton_Click(object sender, RoutedEventArgs e)
        {            
            RegistryDataCollector.CollectionCancelled = true;
            CancelAnalysisButton.IsEnabled = false;
            CancelAnalysisButton.Visibility = Visibility.Hidden;
        }
    }
}
