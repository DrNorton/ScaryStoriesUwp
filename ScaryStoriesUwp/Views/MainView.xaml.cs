using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using MvvmCross.WindowsUWP.Views;
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
    public sealed partial class MainView : MvxWindowsPage, INotifyPropertyChanged
    {
        private StoryIncrementalSource _stories;

        public MainView()
        {
            this.InitializeComponent();
            this.Loaded += MainView_Loaded;

        }

        public StoryIncrementalSource Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;
                OnPropertyChanged("Stories");
            }
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            Stories= new StoryIncrementalSource(this.DataContext as IIncrementalContainer<Story>);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
