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
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace KTI_DashBoard.Editors
{
    public sealed partial class UsersEditor : Page
    {
        int eid;
        public static UsersEditor current;
        public UsersEditor()
        {
            this.InitializeComponent();
            current = this;
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
            SaveNewUser.IsEnabled = NewUserName.Text.Length >= 4
                && !UsersHelper._Users.Any(x => x.UserName == NewUserName.Text)
                && NewUserPassword.Password.Length >= 4;
        }

        private async void SaveNewUser_Click(object sender, RoutedEventArgs e)
        {
            var model = new UsersModel
            {
                id = eid,
                UserName = NewUserName.Text,
                Password = NewUserPassword.Password,
            };
            await UsersHelper.UpdateUser(model);
        }
        public void Load(int e)
        {
            var user = UsersHelper._Users.FirstOrDefault(x => x.id == e);
            NewUserName.Text = user.UserName;
            NewUserPassword.Password = user.Password;
            eid = e;
        }
    }
}
