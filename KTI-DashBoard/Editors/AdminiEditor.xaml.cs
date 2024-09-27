using KTI_DashBoard.Helpers;
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
    public sealed partial class AdminiEditor : Page
    {
        int id;
        public static AdminiEditor current;

        public AdminiEditor()
        {
            this.InitializeComponent();
            current = this;
        }

        private void NewAdminiName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewAdminiNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._EduAdmini.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewAdminiName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("EducationAdministrator", id, UpdateAdminiNameTXT.Text);
            UpdateAdminiNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._EduAdmini.FirstOrDefault(x => x.id == e);
            UpdateAdminiNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
