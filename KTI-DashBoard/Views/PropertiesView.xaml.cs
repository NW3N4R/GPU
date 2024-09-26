using KTI_DashBoard.Helpers;
using KTI_DashBoard.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace KTI_DashBoard.Views
{
    public sealed partial class PropertiesView : Page
    {
        public PropertiesView()
        {
            this.InitializeComponent();
        }

        private void NewProvinceName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            SaveNewProvinceName.IsEnabled = sender.Text.Length > 0 && !WebPropertiesHelper._Province.Any(x => x.Name.Contains(sender.Text));
        }

        private async void SaveNewProvinceName_Click(object sender, RoutedEventArgs e)
        {
            await WebPropertiesHelper.addProperties("province", NewProvinceName.Text);
            NewProvinceName.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            provinceList.ItemsSource = WebPropertiesHelper._Province;
        }

        private void provinceList_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in WebPropertiesHelper._Province.Where(x => x.isActive == true))
            {
                var listViewItem = provinceList.ContainerFromItem(item) as ListViewItem;
                if (listViewItem != null)
                {
                    listViewItem.IsSelected = item.isActive;
                }
            }
        }

        private  void provinceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null && listView.SelectedItem != null)
            {
                foreach (var item in e.AddedItems)
                {
                    var province = item as WebProperties;
                    if (province != null)
                    {
                        Debug.WriteLine($"{province.Name} is selected.");
                    }
                }

                foreach (var item in e.RemovedItems)
                {
                    var province = item as WebProperties;
                    if (province != null)
                    {
                        Debug.WriteLine($"{province.Name} is not selected.");
                    }
                }
            }
        }

        private async void provinceList_ItemClick(object sender, ItemClickEventArgs e)
        {

            //var item = e.ClickedItem as WebProperties;
            //var listView = sender as ListView;

            //if (item != null && listView != null)
            //{
            //    bool isSelected = listView.SelectedItems.Contains(item);

            //    if (isSelected)
            //    {
            //        Debug.WriteLine($"{item.Name} is selected.");
            //    }
            //    else
            //    {
            //        Debug.WriteLine($"{item.Name} is not selected.");
            //    }
            //    await WebPropertiesHelper.UpdatePropStatus("province", item.id, true);

            //}
           
        }
    }
}
