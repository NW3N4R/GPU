using KTI_DashBoard.Helpers;
using KTI_DashBoard.Views;
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
    public sealed partial class ProvinceUpdater : Page
    {
        int id;
        public static ProvinceUpdater current;
        public ProvinceUpdater()
        {
            this.InitializeComponent();
            current = this;
        }
        private void NewProvinceName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewProvinceNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewProvinceName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("province", id, UpdateProvinceNameTXT.Text);
            UpdateProvinceNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._Province.FirstOrDefault(x => x.id == e);
            UpdateProvinceNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
