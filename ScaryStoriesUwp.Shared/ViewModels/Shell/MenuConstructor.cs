using System.Collections.Generic;

namespace ScaryStoriesUwp.Shared.ViewModels.Shell
{
    public class MenuConstructor
    {
        private List<NavMenuItem> _topMenu;
        private List<NavMenuItem> _bottomMenu;

        public List<NavMenuItem> GetTopMenu()
        {
            if (_topMenu == null)
            {
                _topMenu = new List<NavMenuItem>(
                    new[]
                    {
                        new NavMenuItem()
                        {
                            Symbol = 57811,
                            Label = "Библиотека",
                            Background = "#0f5757",
                            ViewModelType = typeof (MainViewModel)
                        },
                        new NavMenuItem()
                        {
                            Symbol = 57622,
                            Label = "Кинематограф",
                            Background = "#00FFFFFFF",
                            ViewModelType = typeof (VideosViewModel)
                        },
                          new NavMenuItem()
                        {
                            Symbol = 57619,
                            Label = "Избранные истории",
                            Background = "#00FFFFFFF",
                            ViewModelType = typeof (FavoritesViewModel)
                        }
                    });
            }
            return _topMenu;
        }


        public List<NavMenuItem> GetBottomMenu()
        {
            if (_bottomMenu == null)
            {
                _bottomMenu = new List<NavMenuItem>(
                    new[]
                    {
                        new NavMenuItem()
                        {
                            Symbol = 57621,
                            Label = "Настройки",
                            Background = "#00FFFFFFF",
                            ViewModelType = typeof (SettingsViewModel)
                        },

                    });
            }
            return _bottomMenu;
        }
    }
}
