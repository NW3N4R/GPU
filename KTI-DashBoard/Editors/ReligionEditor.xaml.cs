using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;


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
