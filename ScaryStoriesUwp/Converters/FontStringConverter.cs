using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ScaryStoriesUwp.Converters
{
    public class FontStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var strFont = value as string;
            return new FontFamily(strFont);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var val = value as FontFamily;
            return val.Source;
        }
    }
}
