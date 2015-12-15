using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.WindowsCommon.Views;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.ViewModels.Base;
using ScaryStoriesUwp.Shared.ViewModels.Shell;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ScaryStoriesUwp.Views.Shell
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : MvxWindowsPage
    {
        public static ShellView Current = null;

        private Frame _currentFrame;
        public ShellView(Frame frame)
        {
            _currentFrame = frame;
            this.InitializeComponent();
            var transition = new NavigationThemeTransition();
            frame.ContentTransitions = new TransitionCollection();
            frame.ContentTransitions.Add(transition);
            SetTitleBarColor();
            _currentFrame.Navigated += _currentFrame_Navigated;
            Grid.SetRow(_currentFrame, 1);
            this.RootContent.Children.Add(frame);
            this.Loaded += (sender, args) =>
            {
                this.DataContext = Mvx.Resolve<ShellViewModel>();
                TopNavMenuList.ItemsSource = (this.DataContext as ShellViewModel).TopMenu;
                BottomNavMenuList.ItemsSource = (this.DataContext as ShellViewModel).BottomMenu;
                Current = this;
             
                this.TogglePaneButton.Focus(FocusState.Programmatic);
            };
            Mvx.GetSingleton<IGlobalProgressProvider>().OnProgressChanged += ShellView_OnProgressChanged;
          

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;

            // If on a phone device that has hardware buttons then we hide the app's back button.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.BackButton.Visibility = Visibility.Collapsed;
            }

        }

        private void ShellView_OnProgressChanged(bool status, string text)
        {
            if (status)
            {
                ProgressText.Text = text;
            }
            else
            {
                ProgressText.Text = "";
            }
          
            ProgressRing.IsActive = status;
        }

        private void _currentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = (e.Content as MvxWindowsPage);
            var title = page.Tag;
            if (title != null)
                Title.Text = title.ToString();
            var context=page.DataContext;

        }

        private void SetTitleBarColor()
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            // Title bar colors. Alpha must be 255.
            var brush = new SolidColorBrush(new Color() { R = 23, G = 23, B = 23, A = 1 });
            titleBar.BackgroundColor = brush.Color;
            titleBar.ForegroundColor = brush.Color;
            titleBar.InactiveBackgroundColor = brush.Color;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = brush.Color;
            titleBar.ButtonHoverBackgroundColor = Colors.White;
            titleBar.ButtonPressedBackgroundColor = brush.Color;
            titleBar.ButtonInactiveBackgroundColor = brush.Color;
        }

        private void AppShell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            FocusNavigationDirection direction = FocusNavigationDirection.None;
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Left:
                case Windows.System.VirtualKey.GamepadDPadLeft:
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                case Windows.System.VirtualKey.NavigationLeft:
                    direction = FocusNavigationDirection.Left;
                    break;
                case Windows.System.VirtualKey.Right:
                case Windows.System.VirtualKey.GamepadDPadRight:
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                case Windows.System.VirtualKey.NavigationRight:
                    direction = FocusNavigationDirection.Right;
                    break;

                case Windows.System.VirtualKey.Up:
                case Windows.System.VirtualKey.GamepadDPadUp:
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                case Windows.System.VirtualKey.NavigationUp:
                    direction = FocusNavigationDirection.Up;
                    break;

                case Windows.System.VirtualKey.Down:
                case Windows.System.VirtualKey.GamepadDPadDown:
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                case Windows.System.VirtualKey.NavigationDown:
                    direction = FocusNavigationDirection.Down;
                    break;
            }

            if (direction != FocusNavigationDirection.None)
            {
                var control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(FocusState.Programmatic);
                    e.Handled = true;
                }
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bool ignored = false;
            this.BackRequested(ref ignored);
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.
            var currentViewModel = ((_currentFrame.Content as MvxWindowsPage).DataContext as LoadingScreen);
            currentViewModel.Close();
        }

        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {

        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {

            }
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
            this.CheckTogglePaneButtonSizeChanged();
        }



        public Rect TogglePaneButtonRect
        {
            get;
            private set;
        }

        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<ShellView, Rect> TogglePaneButtonRectChanged;

        /// <summary>
        /// Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            if (this.RootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                this.RootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = this.TogglePaneButton.TransformToVisual(this);
                var rect = transform.TransformBounds(new Rect(0, 0, this.TogglePaneButton.ActualWidth, this.TogglePaneButton.ActualHeight));
                this.TogglePaneButtonRect = rect;
            }
            else
            {
                this.TogglePaneButtonRect = new Rect();
            }

            var handler = this.TogglePaneButtonRectChanged;
            if (handler != null)
            {
                // handler(this, this.TogglePaneButtonRect);
                handler.DynamicInvoke(this, this.TogglePaneButtonRect);
            }
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

    }
}
