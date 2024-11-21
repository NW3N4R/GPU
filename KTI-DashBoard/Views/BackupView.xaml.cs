using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using WinRT.Interop;
using KTI_DashBoard.LocalStore;
using Newtonsoft;
using Newtonsoft.Json;
using System.Diagnostics;
using ABI.Windows.AI.MachineLearning;
using KTI_DashBoard.Helpers;
using System.Threading.Tasks;
using System.Linq;

namespace KTI_DashBoard.Views
{
    public sealed partial class BackupView : Page
    {
        bool isDirectory = false;
        BackupJobModel _Jobs;
        CrudWithJobs _CrudWithJobs;
        public BackupView()
        {
            this.InitializeComponent();
            _Jobs = new BackupJobModel();
            NewJobGrid.DataContext = _Jobs;
            _CrudWithJobs = new CrudWithJobs();
            JobsListView.ItemsSource = _CrudWithJobs._BackupJobs;

        }

        private async void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {

            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;

            // Get the window handle (HWND) from the main window
            var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(folderPicker, windowHandle);
            // Show the picker and get the folder chosen by the user
            StorageFolder selectedFolder = await folderPicker.PickSingleFolderAsync();
            if (selectedFolder != null)
            {
                string folderPath = selectedFolder.Path;
                DirectoryButton.Content = folderPath;
                isDirectory = true;
                SaveNewJob.IsEnabled = NewJobName.Text.Length >= 3;
            }
            else
            {
                DirectoryButton.Content = "No Folder Selected";
                SaveNewJob.IsEnabled = false;
            }
        }

        private async void SaveNewJob_Click(object sender, RoutedEventArgs e)
        {
            _Jobs.JobName = NewJobName.Text;
            _Jobs.Directory = DirectoryButton.Content.ToString();
            await _CrudWithJobs.AddAsync(_Jobs);
            JobsListView.ItemsSource = null;
            JobsListView.ItemsSource = _CrudWithJobs._BackupJobs;
        }

        private void NewJobName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            SaveNewJob.IsEnabled = NewJobName.Text.Length >= 3 && isDirectory;
            DirectoryButton.IsEnabled = NewJobName.Text.Length >= 3;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ImageReaders._imageCollection.Clear();
            await _CrudWithJobs.ReadAll();
            await Helper_PersonalStudent.GetStudents();
            var rows = Helper_PersonalStudent._Student;
            foreach (var item in rows)
            {
                string url = $"http://p4165386.eero.online/desktopfiles/{item.Id}.jpg";
                await ImageReaders.LoadImageFromUrlAsync(url, item.Id.ToString());
                MyGridView.ItemsSource = ImageReaders._imageCollection;
            }
        }

        private async void DeletButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int id = Int32.Parse(btn.Tag.ToString());
                await _CrudWithJobs.DeleteAsync(id);
            }
            JobsListView.ItemsSource = null;
            JobsListView.ItemsSource = _CrudWithJobs._BackupJobs;
        }
    }
}
