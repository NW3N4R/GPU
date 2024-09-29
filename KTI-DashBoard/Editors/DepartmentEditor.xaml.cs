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
    public sealed partial class DepartmentEditor : Page
    {
        int id;
        public static DepartmentEditor current;

        public DepartmentEditor()
        {
            this.InitializeComponent();
            current = this;

        }

        private void NewDepartmentName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewDepartmentNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Department.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewDepartmentName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("Department", id, UpdateDepartmentNameTXT.Text);
            UpdateDepartmentNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._Department.FirstOrDefault(x => x.id == e);
            UpdateDepartmentNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
