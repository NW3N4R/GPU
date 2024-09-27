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
    public sealed partial class MartialUpdater : Page
    {
        int id;
        public static MartialUpdater current;

        public MartialUpdater()
        {
            this.InitializeComponent();
            current = this;
        }
     
        private void NewMartialName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            UpdateNewMartialNameBttn.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name == sender.Text);
        }
        private async void SaveNewMartialName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.UpdateProp("martialstatus", id, UpdateMartialNameTXT.Text);
            UpdateMartialNameTXT.Text = "";
        }
        public void load(int e)
        {
            var prop = WebPropertiesHelper._Martial.FirstOrDefault(x => x.id == e);
            UpdateMartialNameTXT.Text = prop.Name;
            id = e;
        }
    }
}
