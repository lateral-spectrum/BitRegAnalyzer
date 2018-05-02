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
        public bool LogOnlyMatches;
        public IntPtr ConsoleHandle;

        public MainWindow(App main)
        {
            MainApp = main;
            InitializeComponent();

            ConsoleHandle = ConsoleManager.GetConsoleWindow();           

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
            LogScrollViewer.Visibility = Visibility.Hidden;
            SetOutputButtonEnabledStatus(false);
            LogOnlyMatches = Convert.ToBoolean(CheckboxLogOnlyMatches.IsChecked);

            MainApp.Analyzer = new RegistryAnalyzer(this);
            MainApp.Analyzer.SearchTerms = new List<string>() { SearchTerm1TextBox.Text, SearchTerm2TextBox.Text, SearchTerm3TextBox.Text };
            EntryLogger.main_window = this;

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
                MainApp.Analyzer.EntryCollectors = new List<RegistryDataCollector>(collected_datas);

                List<RegistryEntry> recorded_entries = new List<RegistryEntry>();
                foreach (RegistryDataCollector collector in collected_datas)
                {
                    foreach (RegistryEntry entry in collector.RegistryEntries)
                    {
                        recorded_entries.Add(entry);
                    }
                }

                new Thread(new ThreadStart(() =>
                {                  
                    if (!LogOnlyMatches)
                    {
                        SetUIMode(false);
                        EntryLogger.LogEntries(recorded_entries);
                        SetUIMode(true);
                        SetOutputButtonEnabledStatus(false);
                    }
                })).Start();
                

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
            SetOutputButtonEnabledStatus(true);
        }

        public void SetOutputButtonEnabledStatus(bool enabled)
        {
            Dispatcher.Invoke(() =>
            {
                OutputHtmlButton.IsEnabled = enabled;
                OutputCSVButton.IsEnabled = enabled;
                OutputTxtButton.IsEnabled = enabled; 
            });            
        }

        private void OutputHtmlButton_Click(object sender, RoutedEventArgs e)
        {
            new ResultHtmlOutput(MainApp.Analyzer);
        }       

        private void OutputTxtButton_Click(object sender, RoutedEventArgs e)
        {
            //new ResultTxtOutput(MainApp.Analyzer);
            //ConsoleManager.ShowWindow(ConsoleHandle, ConsoleManager.SW_SHOW);
            //Console.WriteLine("Cicken");
        }

        private void OutputCSVButton_Click(object sender, RoutedEventArgs e)
        {
            new ResultCSVOutput(MainApp.Analyzer);
        }
    }
}
