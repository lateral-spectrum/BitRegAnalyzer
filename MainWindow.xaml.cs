using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.Win32;

namespace BitRegAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrySearch searcher = new RegistrySearch();
            //searcher.DoSearchOne();
            RegistryKey search_one_top_key = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            searcher.SearchRegistrySect(search_one_top_key, @"C:\Program Files (x86)\AviSynth+\plugins64+");
        }
    }
}
