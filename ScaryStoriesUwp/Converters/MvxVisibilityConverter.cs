using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Cirrious.CrossCore.UI;

namespace ScaryStoriesUwp.Converters
{
    public class MvxVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vis = (MvxVisibility)value;
            switch (vis)
            {
                case MvxVisibility.Collapsed:
                    return Visibility.Collapsed;
                    break;

                case MvxVisibility.Visible:
                    return Visibility.Visible;
                    break;
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
