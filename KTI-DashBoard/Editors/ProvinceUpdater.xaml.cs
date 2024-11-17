using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;


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
