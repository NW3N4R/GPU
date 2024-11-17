using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;

namespace KTI_DashBoard
{
    public partial class App : Application
    {
        public static Window MainWindow { get; set; }

        public App()
        {
            this.InitializeComponent();


        }
        async void load()
        {
            await DbConnectionHelper.OpenConnection();

        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            load();
            m_window = new MainWindow();
            MainWindow = m_window;
            m_window.Activate();
        }

        private Window m_window;
    }
}
