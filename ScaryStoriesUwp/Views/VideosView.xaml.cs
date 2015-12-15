using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Annotations;
using ScaryStoriesUwp.Helpers;
using ScaryStoriesUwp.Shared.ViewModels.Base;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ScaryStoriesUwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideosView : MvxWindowsPage, INotifyPropertyChanged
    {
        private VideoIncrementalSource _videos;

        public VideosView()
        {
            this.InitializeComponent();
        }

        private void VideosView_OnLoaded(object sender, RoutedEventArgs e)
        {
            Videos = new VideoIncrementalSource(this.DataContext as IIncrementalContainer<Video>);
        }

        public VideoIncrementalSource Videos
        {
            get { return _videos; }
            set
            {
                _videos = value;
                OnPropertyChanged("Videos");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
