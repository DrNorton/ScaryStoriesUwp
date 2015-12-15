using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class SettingsViewModel:LoadingScreen
    {
        private readonly ISettingsProvider _settingsProvider;
        private List<string> _fonts;
        private TextInfoSettings _textSettings;

        public SettingsViewModel(ISettingsProvider settingsProvider) 
            : base("Настройки")
        {
            _settingsProvider = settingsProvider;
            _textSettings = settingsProvider.TextSettings;
            CreateFontFamilyList();
        }

        private void CreateFontFamilyList()
        {
            _fonts = new List<string>();
            _fonts.Add("Arial");
            _fonts.Add("Calibri");
            _fonts.Add("Comic Sans MS");
            _fonts.Add("Courier New");
            _fonts.Add("Georgia");
            _fonts.Add("Lucida Sans Unicode");
            _fonts.Add("Malgun Gothic");
            _fonts.Add("Meiryo UI");
            _fonts.Add("Segoe UI");
            _fonts.Add("Tahoma");
            _fonts.Add("Times New Roman");
            _fonts.Add("Trebuchet MS");
            _fonts.Add("Verdana");
            _fonts.Add("Webdings");
        }

        public List<string> Fonts
        {
            get { return _fonts; }
            set
            {
                _fonts = value;
               
                base.RaisePropertyChanged(() => Fonts);
            }
        }

        public string CurrentFont
        {
            get { return _textSettings.Font; }
            set
            {
                _textSettings.Font = value;
                Save();
                base.RaisePropertyChanged(() => CurrentFont);
            }
        }

        public double TextSize
        {
            get { return _textSettings.Size; }
            set
            {
                _textSettings.Size = value;
                Save();
                base.RaisePropertyChanged(() => TextSize);
            }
        }

        public double LineHeight
        {
            get { return _textSettings.LineHeight; }
            set
            {

                _textSettings.LineHeight = value;
                Save();
                base.RaisePropertyChanged(() => LineHeight);
            }
        }

        private void Save()
        {
            _settingsProvider.TextSettings = _textSettings;
        }
    }
}
