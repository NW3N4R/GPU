using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using KTI_DashBoard.Views;
using Windows.ApplicationModel.VoiceCommands;
using KTI_DashBoard.Helpers;

namespace KTI_DashBoard
{
    public partial class App : Application
    {
        private AppWindow _appWindow;
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
