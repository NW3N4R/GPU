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
    public sealed partial class ReligionEditor : Page
    {
        int id;
        public static ReligionEditor current;
        public ReligionEditor()
        {
            this.InitializeComponent();
            current = this;

        }

        private void NewReligionName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewReligionNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewReligionName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("religion", id, UpdatereligionNameTXT.Text);
            UpdatereligionNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._Religion.FirstOrDefault(x => x.id == e);
            UpdatereligionNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
