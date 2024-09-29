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
using KTI_DashBoard.Models;
using OfficeOpenXml;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using KTI_DashBoard.Helpers;


namespace KTI_DashBoard.Views
{
    public sealed partial class StaticalView : Page
    {
        bool doSearch = false;
        List<StudentTableModel> _student = new List<StudentTableModel>();
        public StaticalView()
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

            await DbConnectionHelper.LoadAll();
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
                    Graduate = "-",
                    RowCount = x
                };
                _student.Add(model);
            }
            foreach (var student in Helper_PersonalStudent.ar_Student)
            {
                x++;
                var model = new StudentTableModel
                {
                    id = student.Id,
                    Name = student.Name,
                    department = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id).Department,
                    Acceptance = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id).AcceptanceType,
                    Stage = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id).Stage,
                    StartingYear = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id).startinYear,
                    Graduate = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == student.Id).Graduate,
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

            List<string> GraduationList = new List<string>();
            GraduationList.Add("Graduation");
            foreach (var item in _student.Select(x => x.Graduate).Distinct().ToList())
            {
                GraduationList.Add(item);
            }
            GraduationCombo.ItemsSource = GraduationList;
            StartingYearCombo.ItemsSource = startingYearList;
            DepartmentCombo.ItemsSource = list;
            AcceptancCombo.SelectedIndex = 0;
            DepartmentCombo.SelectedIndex = 0;
            StartingYearCombo.SelectedIndex = 0;
            GraduationCombo.SelectedIndex = 0;
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

        private async void ToExcelExportBttn_Click(object sender, RoutedEventArgs e)
        {
            List<StudentTableModel> list = new List<StudentTableModel>();
            foreach (var item in studentListView.SelectedItems)
            {
                var model = item as StudentTableModel;
                list.Add(model);
            }
            ExcelPackage excelPackage = await ToExcelPrint.DoPrint(list, 2);
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

        private void GraduationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doSearch)
            {
                if (GraduationCombo.SelectedIndex > 0)
                {
                    studentListView.ItemsSource = _student.Where(x => x.Graduate.ToString() == GraduationCombo.SelectedItem.ToString());
                }
                else
                {
                    studentListView.ItemsSource = _student;
                }
            }
        }
    }
}
