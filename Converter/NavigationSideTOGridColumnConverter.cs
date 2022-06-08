using CoffeeApplication.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CoffeeApplication.Converter
{
    public class NavigationSideTOGridColumnConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var navigationSide = (NavigationSide)value;
            return navigationSide == NavigationSide.LEFT
                ? 0
                : 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
