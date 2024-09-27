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
    public sealed partial class Nationality : Page
    {
        int id;
        public static Nationality current;
        public Nationality()
        {
            this.InitializeComponent();
            current = this;
        }

        private void NewNationalityName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewNationalityNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewNationalityName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("Nationality", id, UpdateNationalityNameTXT.Text);
            UpdateNationalityNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._Nationality.FirstOrDefault(x => x.id == e);
            UpdateNationalityNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
