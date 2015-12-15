using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ScaryStoriesUwp.Converters
{
    public class PlayerDoubleToTimeSpanConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (TimeSpan)value;
            return val.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var val = (double)value;
            return TimeSpan.FromSeconds(val);
        }
    }
}
