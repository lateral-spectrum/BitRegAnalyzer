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
            MainApp.Analyzer = new RegistryAnalyzer(this);
            MainApp.Analyzer.SearchTerms = new List<string>() { SearchTerm1TextBox.Text, SearchTerm2TextBox.Text, SearchTerm3TextBox.Text };

            AnalysisRunLogger.LogNewRun();
            AnalysisRunLogger.UpdateCurrentRunID();

            Console.WriteLine("Collecting registry data");

            List<RegistryKey> keys_to_search = new List<RegistryKey>();
            if (Convert.ToBoolean(CheckboxCurrentUser.IsChecked))
            {
                RegistryKey opened_key = Registry.CurrentUser.OpenSubKey("SOFTWARE", false);
                if (opened_key != null)
                {
                    keys_to_search.Add(opened_key);
                }
            }
            if (Convert.ToBoolean(CheckboxLocalMachine.IsChecked))
            {
                RegistryKey opened_key = Registry.LocalMachine.OpenSubKey("SOFTWARE", false);
                if (opened_key != null)
                {
                    keys_to_search.Add(opened_key);
                }                
            }
            if (Convert.ToBoolean(CheckboxRecentApps.IsChecked))
            {                
                RegistryKey opened_key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Search\RecentApps");
                if (opened_key != null)
                {
                    keys_to_search.Add(opened_key);
                }
                else
                {
                    Console.WriteLine("Recent apps is null.");
                }
            }
            if (Convert.ToBoolean(CheckboxRecentAppsDocs.IsChecked))
            {
                RegistryKey opened_key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32");
                if (opened_key != null)
                {
                    keys_to_search.Add(opened_key);
                }
                else
                {
                    Console.WriteLine("Recent apps docs is null.");
                }
            }
            if (Convert.ToBoolean(CheckboxRecentTorrents.IsChecked))
            {
                RegistryKey opened_key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs");
                if (opened_key != null)
                {
                    keys_to_search.Add(opened_key);
                }
                else
                {
                    Console.WriteLine("Recent Torrents is null.");
                }
            }
            
            Thread collection_thread = new Thread(new ThreadStart(() =>
            {
                RegistryDataCollector[] collected_datas = MainApp.Analyzer.CollectRegistryData(keys_to_search);
                List<RegistryEntry> recorded_entries = new List<RegistryEntry>();
                foreach (RegistryDataCollector collector in collected_datas)
                {
                    foreach (RegistryEntry entry in collector.RegistryEntries)
                    {
                        recorded_entries.Add(entry);
                    }
                }

                EntryLogger.LogEntries(recorded_entries);

                OnDataCollectionIsFinished();
            }));

            collection_thread.Start();
        }

        public void SetUIMode(bool target_enabled)
        {          
            Dispatcher.Invoke(() => {
                if (target_enabled)
                {
                    RunAnalysisButton.IsEnabled = true;
                    AnalysisProgressBar.IsIndeterminate = false;
                }
                else
                {
                    RunAnalysisButton.IsEnabled = false;
                    AnalysisProgressBar.IsIndeterminate = true;
                }

                CheckboxCurrentUser.IsEnabled = target_enabled;
                CheckboxLocalMachine.IsEnabled = target_enabled;

                SearchTerm1TextBox.IsEnabled = target_enabled;
                SearchTerm2TextBox.IsEnabled = target_enabled;
                SearchTerm3TextBox.IsEnabled = target_enabled;
            });           
        }

        public void OnDataCollectionIsFinished()
        {            
            SetUIMode(true);
        }

        private void OutputHtmlButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
