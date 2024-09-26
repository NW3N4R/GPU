using KTI_DashBoard.Helpers;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace KTI_DashBoard
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

           
        }

        private async void NavView_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (NavigationViewItemBase item in NavView.MenuItems.Cast<NavigationViewItemBase>())
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
            NavView.SelectedItem = NavView.MenuItems.FirstOrDefault();
            ContentFrame.Navigate(typeof(KTI_DashBoard.Views.Home));
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            NavView.IsPaneOpen = false;

            await DbConnectionHelper.OpenConnection();
            await WebPropertiesHelper.GetAllProps();

            //await DbConnectionHelper.LoadAll();
        }
        private NavigationViewItem _lastItem;
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null)
                return;

            var ClickedView = item.Tag.ToString();
            if (!NavigateToView(ClickedView)) return;
            _lastItem = item;
            NavView.IsPaneOpen = false;

        }
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
                NavView.IsBackEnabled = ContentFrame.CanGoBack;
                NavView.SelectedItem = _lastItem;
            }
            NavView.IsPaneOpen = false;

        }
        private bool NavigateToView(string ClickedView)
        {
            var View = Assembly.GetExecutingAssembly().GetType($"KTI_DashBoard.Views.{ClickedView}");
            if (string.IsNullOrEmpty(ClickedView) || View == null)
                return false;

            ContentFrame.Navigate(View, null, new EntranceNavigationTransitionInfo());
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
            NavView.IsPaneOpen = false;

            return true;
        }

        private void Window_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint pointerPoint = e.GetCurrentPoint((UIElement)sender);
            PointerPointProperties properties = pointerPoint.Properties;
            if (properties.IsXButton1Pressed)
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                    NavView.IsBackEnabled = ContentFrame.CanGoBack;
                    NavView.SelectedItem = _lastItem;
                    NavView.IsPaneOpen = false;

                }
            }
        }

    }
}
