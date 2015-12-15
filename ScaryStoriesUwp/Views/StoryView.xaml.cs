using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Cirrious.MvvmCross.WindowsCommon.Views;
using ScaryStoriesUwp.Shared.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ScaryStoriesUwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StoryView : MvxWindowsPage
    {
        public StoryView()
        {
            this.InitializeComponent();
            this.Loaded += StoryView_Loaded;
        }


        private void StoryView_Loaded(object sender, RoutedEventArgs e)
        {
              (this.DataContext as StoryViewModel).SetMediaElement(MediaElement);
        }

        private void MediaElement_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            Seek.Maximum = MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
           
        }

      
    }
}
