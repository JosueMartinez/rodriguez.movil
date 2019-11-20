using System;
using System.Globalization;
using Xamarin.Forms;

namespace rodriguez.Classes
{
    public class ChangeCompradoText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isComprado = (bool)value;
            return isComprado ? "Pendiente" : "Comprado";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
