using KTI_DashBoard.Helpers;
using KTI_DashBoard.Models;
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
    public sealed partial class EmployeesEditor : Page
    {
        public static EmployeesEditor current;
        int eid;
        public EmployeesEditor()
        {
            this.InitializeComponent();
            current = this;
        }

        private async void EmployeSave_Click(object sender, RoutedEventArgs e)
        {
            var model = new employeesModel
            {
                id = eid,
                Name = EmpName.Text
            };
            await EmployesHelper.UpdateEmployees(model);
        }
        public void load(int e)
        {
            eid = e;
            EmpName.Text = EmployesHelper._employees.FirstOrDefault(x=>x.id == e).Name;
        }

        private void EmpName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            EmployeSave.IsEnabled = sender.Text.Length > 0 && !EmployesHelper._employees.Any(x => x.Name.Contains(EmpName.Text));
        }
    }
}
