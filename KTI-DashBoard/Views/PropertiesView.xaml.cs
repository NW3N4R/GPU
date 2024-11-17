using KTI_DashBoard.Editors;
using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;


namespace KTI_DashBoard.Views
{
    public sealed partial class PropertiesView : Page
    {

        public PropertiesView()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            load();
            MainWindow.current.Refresh.Click += Refresh_Click;
        }
        async void load()
        {

            provinceList.ItemsSource = null;
            MartialList.ItemsSource = null;
            AdminiList.ItemsSource = null;
            NationalityList.ItemsSource = null;
            ReligionList.ItemsSource = null;
            DepartmentList.ItemsSource = null;
            await WebPropertiesHelper.GetAllProps();
            provinceList.ItemsSource = WebPropertiesHelper._Province;
            MartialList.ItemsSource = WebPropertiesHelper._Martial;
            AdminiList.ItemsSource = WebPropertiesHelper._EduAdmini;
            NationalityList.ItemsSource = WebPropertiesHelper._Nationality;
            ReligionList.ItemsSource = WebPropertiesHelper._Religion;
            DepartmentList.ItemsSource = WebPropertiesHelper._Department;
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void NewProvinceName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (sender == NewProvinceName)
            {
                SaveNewProvinceName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name.Contains(sender.Text));
            }
            else if (sender == NewMartiaName)
            {
                SaveNewMartialName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Martial.Any(x => x.Name.Contains(sender.Text));
            }
            else if (sender == NewAdminiName)
            {
                SaveNewAdminiName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._EduAdmini.Any(x => x.Name.Contains(sender.Text));
            }
            else if (sender == NewNationalityName)
            {
                SaveNationalityName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Nationality.Any(x => x.Name.Contains(sender.Text));
            }
            else if (sender == NewReligionName)
            {
                SaveReligionName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Religion.Any(x => x.Name.Contains(sender.Text));
            }
            else
            {
                SaveNewDepartmentName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Department.Any(x => x.Name.Contains(sender.Text));

            }
        }
        #region Province
        private async void SaveNewProvinceName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("province", NewProvinceName.Text);
            NewProvinceName.Text = "";
            load();
        }
        private async void ProvinceCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("province", id, isActive);
            }
            load();
        }
        private void OpenProvinceUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            ProvinceUpdaterTip.Target = sender as Button;
            ProvinceUpdaterTip.IsOpen = !ProvinceUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            ProvinceUpdater.current.load(id);
        }

        #endregion
        #region Martial Status
        private async void SaveNewMartialName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("martialstatus", NewMartiaName.Text);
            NewMartiaName.Text = "";
            load();
        }
        private void OpenMartialUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            MartialUpdaterTip.Target = sender as Button;
            MartialUpdaterTip.IsOpen = !MartialUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            MartialUpdater.current.load(id);
        }
        private async void MartialCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("martialstatus", id, isActive);
            }
            load();
        }
        #endregion
        #region Admini
        private async void SaveNewAdminiName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("EducationAdministrator", NewAdminiName.Text);
            NewAdminiName.Text = "";
        }
        private async void AdminiCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("EducationAdministrator", id, isActive);
            }
        }
        private void OpenAdminiUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            AdminiUpdaterTip.Target = sender as Button;
            AdminiUpdaterTip.IsOpen = !AdminiUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            AdminiEditor.current.load(id);
        }
        #endregion
        #region Nationality
        private async void SaveNewNationalityName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("Nationality", NewNationalityName.Text);
            NewNationalityName.Text = "";
        }
        private async void NationalityCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("Nationality", id, isActive);
            }
        }
        private void OpenNationalityUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            NationalityUpdaterTip.Target = sender as Button;
            NationalityUpdaterTip.IsOpen = !NationalityUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            Nationality.current.load(id);
        }

        #endregion
        #region Religion
        private async void SaveNewReligionName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("Religion", NewReligionName.Text);
            NewReligionName.Text = "";
        }
        private async void ReligionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("Religion", id, isActive);
            }
        }
        private void OpenreligionUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            ReligionUpdaterTip.Target = sender as Button;
            ReligionUpdaterTip.IsOpen = !ReligionUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            ReligionEditor.current.load(id);
        }


        #endregion
        private async void SaveNewDepartmentName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("Department", NewDepartmentName.Text);
            NewDepartmentName.Text = "";
        }
        private async void DepartmentCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            if (ch != null)
            {
                int id = Int32.Parse(ch.Tag.ToString());
                bool isActive = ch.IsChecked == true ? true : false;
                await WebPropertiesHelper.UpdatePropStatus("Department", id, isActive);
            }
        }
        private void OpenDepartmentUpdaterTip_Click(object sender, RoutedEventArgs e)
        {
            departmentUpdaterTip.Target = sender as Button;
            departmentUpdaterTip.IsOpen = !departmentUpdaterTip.IsOpen;
            int id = Int32.Parse((sender as Button).Tag.ToString());
            DepartmentEditor.current.load(id);
        }

    }
}
