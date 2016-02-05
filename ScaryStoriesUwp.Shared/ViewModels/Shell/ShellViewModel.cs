using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ScaryStoriesUwp.Shared.Services;

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
        private ISettingsProvider _settingsProvider;
       
        private IMessageProvider _messageProvider;


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

        public bool IsOffline
        {
            get { return !_settingsProvider.IsOffline; }
            set
            {
                SetOffline(value);
                base.RaisePropertyChanged(()=>IsOffline);
            }
        }

        private async void SetOffline(bool value)
        {
            if (value == true)
            {
                //Проверяем скачана ли база
                if (_settingsProvider.DatabaseVersion != 0)
                {
                    _settingsProvider.IsOffline = true;
                }
                else
                {
                    var dialogResult=await
                        _messageProvider.ShowCustomOkNoMessageBox(
                            "Для включения оффлайн режима, нужно перейти в настройки и скачать локальную базу данных. Переходим?",
                            "Информация");
                    if (dialogResult)
                    {
                        ShowViewModel<SettingsViewModel>();
                    }
                    else
                    {
                        IsOffline = false;
                    }

                }
            }
            else
            {
                _settingsProvider.IsOffline = false;
            }
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
            _settingsProvider = Mvx.Resolve<ISettingsProvider>();
            _messageProvider= Mvx.Resolve<IMessageProvider>();
            _settingsProvider.PropertyChanged += _settingsProvider_PropertyChanged;
            Debug.WriteLine("dad");
        }

        private void _settingsProvider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOffline")
            {
                base.RaisePropertyChanged(()=>IsOffline);
            }
        }

        private void Online()
        {
            var provider = Mvx.Resolve<ISettingsProvider>();
            provider.IsOffline = !provider.IsOffline;
        }
    }
}
