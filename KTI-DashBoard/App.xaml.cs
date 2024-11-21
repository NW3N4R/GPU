using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;
using Windows.Storage;
using KTI_DashBoard.LocalStore;
using Windows.ApplicationModel.Background;
using System.Linq;

namespace KTI_DashBoard
{
    public partial class App : Application
    {
        public static Window MainWindow { get; set; }
        public static Window m_window;

        public App()
        {
            this.InitializeComponent();
            m_window = new MainWindow();
            MainWindow = m_window;
            m_window.Activate();
            SetupLocalJson();
        }
        async void load()
        {
            await DbConnectionHelper.OpenConnection();

        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            load();
        }

        private async void SetupLocalJson()
        {
            string fileName = "LocalSettings.json";  // Your JSON file name

            // Get the path to the local folder (where your file is copied)
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // Try to get the file from the local folder (or create it if it doesn't exist)
            StorageFile jsonFile;
            try
            {
                // Check if the file already exists in the local folder
                jsonFile = await localFolder.GetFileAsync(fileName);
            }
            catch (FileNotFoundException)
            {
                // If the file doesn't exist, create it with an empty list
                jsonFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                var people = new List<BackupJobModel>();  // Empty list
                string jsonString = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true });
                await FileIO.WriteTextAsync(jsonFile, jsonString);
            }

        }


   
    }
}
