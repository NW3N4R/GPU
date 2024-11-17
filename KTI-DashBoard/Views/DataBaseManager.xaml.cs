using KTI_DashBoard.Helpers;
using KTI_DashBoard.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Linq;


namespace KTI_DashBoard.Views
{
    public sealed partial class DataBaseManager : Page
    {
        DispatcherTimer _UpTimer = new DispatcherTimer();
        public DataBaseManager()
        {
            this.InitializeComponent();
            _UpTimer.Tick += _UpTimer_Tick;
            _UpTimer.Interval = TimeSpan.FromSeconds(1);
        }

        int x = 0;
        private void _UpTimer_Tick(object sender, object e)
        {
            x++;
            if (x == 3)
            {
                x = 0;
                _UpTimer.Stop();
                WebStatToggle.IsEnabled = true;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            MainWindow.current.Refresh.Click += RefreshButton_Click;
        }

        async void Load()
        {
            bool[] all = new bool[3];

            for (int i = 0; i <= 2; i++)
            {
                all[i] = false;
            }
            all[0] = DbConnectionHelper.con.State == System.Data.ConnectionState.Open;
            all[1] = await WebStat.GetStatAsync();
            all[2] = await WebStat.IsWebRespondingAsync("http://p4165386.eero.online");

            ServerStatus.Text = all[0] ? "Good" : "Down";
            WebStatToggle.IsChecked = all[1] ? true : false;
            WebStatToggle.Content = all[1] ? "Good" : "Down";
            IisStatus.Text = all[2] ? "Good" : "Down";
            AllServiceStatus.Text = !all.Any(x => x == false) ? "All Services are Good" : "Some of The Feature Might Not Work Properly";

            await MyWebView.EnsureCoreWebView2Async();

            MyWebView.CoreWebView2.Navigate("http://p4165386.eero.online");

        }
        private async void WebStatToggle_Click(object sender, RoutedEventArgs e)
        {
            WebStatToggle.IsEnabled = false;
            _UpTimer.Start();
            await WebStat.UpdateWebStat();
            Load();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void ToWeb_Click(object sender, RoutedEventArgs e)
        {
            OpenBrowser("http://p4165386.eero.online");
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                // Use the Process.Start method to open the default browser with the specified URL
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // This is important for opening the URL in the default browser
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
