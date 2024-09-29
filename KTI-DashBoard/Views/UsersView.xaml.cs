using ABI.Windows.ApplicationModel.Activation;
using KTI_DashBoard.Editors;
using KTI_DashBoard.Helpers;
using KTI_DashBoard.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace KTI_DashBoard.Views
{
    public sealed partial class UsersView : Page
    {
        public UsersView()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.current.Refresh.Click += Refresh_Click;
            load();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            load();
        }

        async void load()
        {
            await Task.WhenAll(
                EmployesHelper.GetEmployees(),
                UsersHelper.GetUsers(),
                UsersAccessToDepsHelper.getAccess(),
                WebPropertiesHelper.GetAllProps()
                );
            EmployeesList.ItemsSource = EmployesHelper._employees;
            UsersList.ItemsSource = UsersHelper._Users;
            AccessList.ItemsSource = UsersAccessToDepsHelper._auths;

            foreach (var item in UsersHelper._Users)
            {
                item.EmpName = EmployesHelper._employees.Any(x => x.id == item.EMPID) ? EmployesHelper._employees.FirstOrDefault(x => x.id == item.EMPID).Name : $"Emp Name not Found the userName is {item.UserName}";
            }

            foreach (var item in UsersAccessToDepsHelper._auths)
            {
                item.EmpName = UsersHelper._Users.Any(x => x.id == item.usrid) ? UsersHelper._Users.FirstOrDefault(x => x.id == item.usrid).EmpName : $"Emp Name Not Found usrid is {item.usrid}";
                item.DepName = WebPropertiesHelper._Department.Any(x => x.id == item.depid) ? WebPropertiesHelper._Department.FirstOrDefault(x => x.id == item.depid).Name : $"Dep Name Not Found depid is {item.depid}";
            }
            EmpListForUsers.ItemsSource = EmployesHelper._employees.Where(x => !UsersHelper._Users.Any(z => x.id == z.EMPID) && x.Suspended == false);
            EmpListForUsers.DisplayMemberPath = "Name";
            EmpListForUsers.SelectedValuePath = "id";
            EmployeesList.SelectedIndex = 0;

            bool isEmpAvailable = EmployesHelper._employees.Any(x => !UsersHelper._Users.Any(z => x.id == z.EMPID) && x.Suspended == false);
            ShowNewUserFlyout.IsEnabled = isEmpAvailable;
            AddUserTitle.Text = isEmpAvailable ? "Add User" : "Not Available";
            ShowUserAccessFlyout.IsEnabled = checkAnyFurtherAccess();
            AddUserAccessTitle.Text = checkAnyFurtherAccess() ? "New Access" : "Not Available";
            UsersListForAccess.ItemsSource = UsersHelper._Users.Where(x => !EmployesHelper._employees.Any(z => x.EMPID == z.id && z.Suspended == true));
            UsersListForAccess.DisplayMemberPath = "EmpName";
            UsersListForAccess.SelectedValuePath = "id";
            UsersListForAccess.SelectedIndex = 0;
            loaddeps();

        }
        #region Employee CRUD
        private void NewEmployeename_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            NewEmployeSave.IsEnabled = sender.Text.Length > 0 && !EmployesHelper._employees.Any(x => x.Name.Contains(NewEmployeename.Text));
        }

        private async void NewEmployeSave_Click(object sender, RoutedEventArgs e)
        {
            var model = new employeesModel
            {
                Name = NewEmployeename.Text
            };
            await EmployesHelper.AddEmploye(model);
            NewEmployeename.Text = "";
            load();
        }

        private void ShowEmpEditor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                EmployeesEditorTip.IsOpen = !EmployeesEditorTip.IsOpen;
                EmployeesEditorTip.Target = button;
                int id = Int32.Parse(button.Tag.ToString());
                EmployeesEditor.current.load(id);
            }
        }

        private void EmpSusBttn_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleButton tg = sender as ToggleButton;
            if (tg != null)
            {
                int id = Int32.Parse(tg.Tag.ToString());
                var emp = EmployesHelper._employees.FirstOrDefault(x => x.id == id);
                tg.IsChecked = emp.Suspended;
                tg.Content = emp.Suspended ? "Suspended" : "NOT Suspend";
            }
        }

        private async void EmpSusBttn_Click(object sender, RoutedEventArgs e)
        {

            ToggleButton tg = sender as ToggleButton;
            if (tg != null)
            {
                tg.IsEnabled = false;
                int id = Int32.Parse(tg.Tag.ToString());
                var emp = EmployesHelper._employees.FirstOrDefault(x => x.id == id);

                await EmployesHelper.SuspendEmployee((employeesModel)emp);
                tg.Content = tg.IsChecked == true ? "Suspended" : "NOT Suspend";
                load();
                tg.IsEnabled = true;
            }
        }

        #endregion
        #region New User
        private async void SaveNewUser_Click(object sender, RoutedEventArgs e)
        {
            var model = new UsersModel
            {
                UserName = NewUserName.Text,
                Password = NewUserPassword.Password,
                EMPID = Int32.Parse(EmpListForUsers.SelectedValue.ToString())
            };
            await UsersHelper.AddUser(model);
            load();
        }
        private void NewUserName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            validateUserInputs();
        }
        private void NewUserPassword_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            validateUserInputs();
        }
        void validateUserInputs()
        {
            SaveNewUser.IsEnabled = EmpListForUsers.SelectedIndex >= 0
                && NewUserName.Text.Length >= 4
                && !UsersHelper._Users.Any(x => x.UserName == NewUserName.Text)
                && NewUserPassword.Password.Length >= 4;
        }
        private void EmpListForUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            validateUserInputs();

        }
        private void ShowNewUserFlyout_Click(object sender, RoutedEventArgs e)
        {
            // make sure at least one user is checked on bttn click
            EmpListForUsers.SelectedIndex = 0;
        }

        private void ShowUsersEditor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                UsersEditorTip.IsOpen = !UsersEditorTip.IsOpen;
                UsersEditorTip.Target = button;
                int id = Int32.Parse(button.Tag.ToString());
                UsersEditor.current.Load(id);
            }

        }
        #endregion
        #region Access Create
        bool checkAnyFurtherAccess()
        {
            return UsersHelper._Users.Any(usr => WebPropertiesHelper._Department.Any(dep => !UsersAccessToDepsHelper._auths.Any(z => dep.id == z.depid && usr.id == z.usrid)));
        }
        void loaddeps()
        {
            try
            {
                if (UsersListForAccess.SelectedIndex >= 0)
                {

                    DepsListForAccess.ItemsSource = WebPropertiesHelper._Department.Where(x =>
                        !UsersAccessToDepsHelper._auths.Any(z =>
                              z.usrid == Int32.Parse(UsersListForAccess.SelectedValue.ToString()) && z.depid == x.id)
                        && x.isActive == true);

                    DepsListForAccess.DisplayMemberPath = "Name";
                    DepsListForAccess.SelectedValuePath = "id";
                    DepsListForAccess.SelectedIndex = 0;
                }
                else
                {
                    DepsListForAccess.ItemsSource = null;
                }
            }
            catch
            {
                DepsListForAccess.ItemsSource = null;
            }
        }
        private void UsersListForAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            validateNewAccessBttn();
            loaddeps();
        }
        private void ShowUserAccessFlyout_Click(object sender, RoutedEventArgs e)
        {
            UsersListForAccess.SelectedIndex = 0;
            loaddeps();

        }
        private void DepsListForAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            validateNewAccessBttn();
        }
        void validateNewAccessBttn()
        {
            SaveNewAccess.IsEnabled = DepsListForAccess.SelectedIndex >= 0 && UsersListForAccess.SelectedIndex >= 0;
        }
        private async void SaveNewAccess_Click(object sender, RoutedEventArgs e)
        {
            var model = new UsersAccessToDepsModel
            {
                usrid = Int32.Parse(UsersListForAccess.SelectedValue.ToString()),
                depid = Int32.Parse(DepsListForAccess.SelectedValue.ToString()),
            };
            await UsersAccessToDepsHelper.setAccess(model);
            load();
        }
        #endregion

        private async void DeleteAccess_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int id = Int32.Parse(button.Tag.ToString());
                await UsersAccessToDepsHelper.DeleteAccess(id);
            }
            load();
        }
    }
}
