using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Linq;
using System.Reflection;
using Windows.Foundation;

namespace KTI_DashBoard
{

    public sealed partial class MainWindow : Window
    {
        private AppWindow m_AppWindow;
        public static MainWindow current;
        private DesktopAcrylicBackdrop _backdrop;

        public MainWindow()
        {
            this.InitializeComponent();
            m_AppWindow = this.AppWindow;
            AppTitleBar.Loaded += AppTitleBar_Loaded;
            AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
            ExtendsContentIntoTitleBar = true;
            TitleBarTextBlock.Text = "KTI Control Panel";
            TrySetAcrylicBackdrop();
            current = this;
        }

        private void TrySetAcrylicBackdrop()
        {

            var micaBackdrop = new MicaBackdrop();
            this.SystemBackdrop = micaBackdrop;

        }
        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ExtendsContentIntoTitleBar == true)
            {
                // Set the initial interactive regions.
                SetRegionsForCustomTitleBar();
            }
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ExtendsContentIntoTitleBar == true)
            {
                // Update interactive regions if the size of the window changes.
                SetRegionsForCustomTitleBar();
            }
        }
        private void SetRegionsForCustomTitleBar()
        {
            // Specify the interactive regions of the title bar.

            double scaleAdjustment = AppTitleBar.XamlRoot.RasterizationScale;

            RightPaddingColumn.Width = new GridLength(m_AppWindow.TitleBar.RightInset / scaleAdjustment);
            LeftPaddingColumn.Width = new GridLength(m_AppWindow.TitleBar.LeftInset / scaleAdjustment);

            GeneralTransform transform = TitleBarSearchBox.TransformToVisual(null);
            Rect bounds = transform.TransformBounds(new Rect(0, 0,
                                                             TitleBarSearchBox.ActualWidth,
                                                             TitleBarSearchBox.ActualHeight));
            Windows.Graphics.RectInt32 SearchBoxRect = GetRect(bounds, scaleAdjustment);

            transform = PersonPic.TransformToVisual(null);
            bounds = transform.TransformBounds(new Rect(0, 0,
                                                        PersonPic.ActualWidth,
                                                        PersonPic.ActualHeight));
            Windows.Graphics.RectInt32 PersonPicRect = GetRect(bounds, scaleAdjustment);

            transform = Refresh.TransformToVisual(null);
            bounds = transform.TransformBounds(new Rect(0, 0,
                                                        Refresh.ActualWidth,
                                                        Refresh.ActualHeight));
            Windows.Graphics.RectInt32 RefreshRect = GetRect(bounds, scaleAdjustment);

            transform = Settings.TransformToVisual(null);
            bounds = transform.TransformBounds(new Rect(0, 0,
                                                        Settings.ActualWidth,
                                                        Settings.ActualHeight));

            Windows.Graphics.RectInt32 SettingsRect = GetRect(bounds, scaleAdjustment);


            var rectArray = new Windows.Graphics.RectInt32[] { SearchBoxRect, PersonPicRect, RefreshRect, SettingsRect };

            InputNonClientPointerSource nonClientInputSrc =
                InputNonClientPointerSource.GetForWindowId(this.AppWindow.Id);
            nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rectArray);
        }
        private Windows.Graphics.RectInt32 GetRect(Rect bounds, double scale)
        {
            return new Windows.Graphics.RectInt32(
                _X: (int)Math.Round(bounds.X * scale),
                _Y: (int)Math.Round(bounds.Y * scale),
                _Width: (int)Math.Round(bounds.Width * scale),
                _Height: (int)Math.Round(bounds.Height * scale)
            );
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

            //await WebPropertiesHelper.GetAllProps();

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
