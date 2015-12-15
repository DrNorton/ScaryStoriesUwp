using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace ScaryStoriesUwp.Shared.ViewModels.Shell
{
    public class ShellViewModel : MvxViewModel
    {
        private List<NavMenuItem> _topMenu;
        private NavMenuItem _selectedItem;
        private ICommand _goHomeCommand;
        private ICommand _goToSheduleCommand;
        private ICommand _goToMapCommand;
        private List<NavMenuItem> _bottomMenu;

        public ICommand GoHomeCommand
        {
            get
            {
                _goHomeCommand = _goHomeCommand ?? new MvxCommand(GoHome);
                return _goHomeCommand;
            }
        }
        public ICommand GoToSheduleCommand
        {
            get
            {
                _goToSheduleCommand = _goToSheduleCommand ?? new MvxCommand(GoToShedule);
                return _goToSheduleCommand;
            }
        }

        public NavMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (value != null)
                    Navigate(value);
                base.RaisePropertyChanged(() => SelectedItem);
            }
        }

        public List<NavMenuItem> TopMenu
        {
            get { return _topMenu; }
            set
            {
                _topMenu = value;
                base.RaisePropertyChanged(() => TopMenu);
            }
        }

        public List<NavMenuItem> BottomMenu
        {
            get { return _bottomMenu; }
        }

        private void Navigate(NavMenuItem value)
        {
            ShowViewModel(value.ViewModelType);
        }


        private void GoToShedule()
        {
           // ShowViewModel<SheduleWizardViewModel>();
        }

        private void GoHome()
        {
            ShowViewModel<MainViewModel>();
        }

        public ShellViewModel(MenuConstructor menu)
        {
            _topMenu = menu.GetTopMenu();
            _bottomMenu = menu.GetBottomMenu();
            Debug.WriteLine("dad");
        }
    }
}
