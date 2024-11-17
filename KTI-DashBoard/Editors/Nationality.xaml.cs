using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;


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
