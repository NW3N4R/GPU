using KTI_DashBoard.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;


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
