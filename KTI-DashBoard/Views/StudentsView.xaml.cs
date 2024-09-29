using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using KTI_DashBoard.Helpers;
using KTI_DashBoard.Models;
using System.Diagnostics;
using Windows.Networking.Connectivity;
using Windows.Storage.Pickers;
using OfficeOpenXml;
using Windows.Storage;
using WinRT.Interop;


namespace KTI_DashBoard.Views
{
    public sealed partial class StudentsView : Page
    {
        bool doSearch = false;
        List<StudentTableModel> _student = new List<StudentTableModel>();
        public StudentsView()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.current.Refresh.Click += Refresh_Click;
            Load();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        async void Load()
        {
            studentListView.ItemsSource = null;
            await DbConnectionHelper.LoadStudent();
            _student.Clear();
            int x = 0;
            foreach (var student in Helper_PersonalStudent._Student)
            {
                x++;
                var model = new StudentTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    department = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == student.Id).Department,
                    Acceptance = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == student.Id).AcceptanceType,
                    Stage = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == student.Id).Stage,
                    StartingYear = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == student.Id).startinYear,
                    RowCount = x
                };
                _student.Add(model);
            }
            studentListView.ItemsSource = _student;
            List<string> list = new List<string>();
            list.Add("Department");
            foreach (var item in _student.Select(x => x.department).Distinct().ToList())
            {
                list.Add(item);
            }

            List<string> startingYearList = new List<string>();
            startingYearList.Add("Starting Year");
            foreach (var item in _student.Select(x => x.StartingYear).Distinct().ToList())
            {
                startingYearList.Add(item);
            }
            StartingYearCombo.ItemsSource = startingYearList;
            DepartmentCombo.ItemsSource = list;
            AcceptancCombo.SelectedIndex = 0;
            DepartmentCombo.SelectedIndex = 0;
            MainWindow.current.TitleBarSearchBox.TextChanged += TitleBarSearchBox_TextChanged;
            doSearch = true;
        }

        private void TitleBarSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (sender.Text.Length > 0)
            {
                studentListView.ItemsSource = _student.Where(x => x.Name.Contains(sender.Text));
            }
            else
            {
                studentListView.ItemsSource = _student;
            }
        }

        private void AcceptancCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doSearch)
            {
                if (AcceptancCombo.SelectedIndex > 0)
                {
                    studentListView.ItemsSource = _student.Where(x => x.Acceptance.Contains(AcceptancCombo.SelectedItem.ToString()));
                }
                else
                {
                    studentListView.ItemsSource = _student;
                }
            }
        }

        private void DepartmentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doSearch)
            {
                if (DepartmentCombo.SelectedIndex > 0)
                {
                    studentListView.ItemsSource = _student.Where(x => x.department.Contains(DepartmentCombo.SelectedItem.ToString()));
                }
                else
                {
                    studentListView.ItemsSource = _student;
                }
            }
        }

        private void PrintDocument_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int id = Int32.Parse(button.Tag.ToString());
                string url = $"http://p4165386.eero.online/kti/Print/DoLoadPrint/{id}";

                // Open the URL in the default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int id = Int32.Parse(button.Tag.ToString());
                await Helper_PersonalStudent.DeleteStudent(id);
                Load();
            }
        }

        private void ToggleMutliBttn_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleMutliBttn.IsChecked == true)
            {
                studentListView.SelectionMode = ListViewSelectionMode.Multiple;
                DeleteMultiBttn.Visibility = Visibility.Visible;
                ToExcelExportBttn.Visibility = Visibility.Visible;
            }
            else
            {
                studentListView.SelectionMode = ListViewSelectionMode.Single;
                DeleteMultiBttn.Visibility = Visibility.Collapsed;
                ToExcelExportBttn.Visibility = Visibility.Collapsed;
            }
        }

        private void studentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToExcelExportBttn.IsEnabled = studentListView.SelectedItems.Count > 0;
            DeleteMultiBttn.IsEnabled = studentListView.SelectedItems.Count > 0;
        }

        private async void DeleteMultiBttn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in studentListView.SelectedItems)
            {
                var model = item as StudentTableModel;
                await Helper_PersonalStudent.DeleteStudent(model.id);
            }
            Load();
        }

        private async void ToExcelExportBttn_Click(object sender, RoutedEventArgs e)
        {
            List<StudentTableModel> list = new List<StudentTableModel>();
            foreach (var item in studentListView.SelectedItems)
            {
                var model = item as StudentTableModel;
                list.Add(model);
            }
            ExcelPackage excelPackage = await ToExcelPrint.DoPrint(list);
            var savePicker = new FileSavePicker();

            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(savePicker, hwnd);

            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Excel files", new List<string>() { ".xlsx" });
            savePicker.DefaultFileExtension = ".xlsx";
            savePicker.SuggestedFileName = $"لیسی خوێندکاران {DateTime.Now:yyyy-MM-dd-HH-mm-ss}";

            StorageFile saveFile = await savePicker.PickSaveFileAsync();
            if (saveFile != null)
            {
                using (Stream stream = await saveFile.OpenStreamForWriteAsync())
                {
                    excelPackage.SaveAs(stream);
                }
            }
        }

        private void StageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doSearch)
            {
                if (StageCombo.SelectedIndex > 0)
                {
                    studentListView.ItemsSource = _student.Where(x => x.Stage.ToString() == StageCombo.SelectedItem.ToString());
                }
                else
                {
                    studentListView.ItemsSource = _student;
                }
            }
        }

        private void StartingYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doSearch)
            {
                if (StartingYearCombo.SelectedIndex > 0)
                {
                    studentListView.ItemsSource = _student.Where(x => x.StartingYear.ToString() == StartingYearCombo.SelectedItem.ToString());
                }
                else
                {
                    studentListView.ItemsSource = _student;
                }
            }
        }
    }
}
