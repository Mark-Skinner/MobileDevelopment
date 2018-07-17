using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace WillowTree.NameGame.Win.Converters
{
    public class EqualsValueConverter : IValueConverter
    {
        public EqualsValueConverter()
        {

        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || parameter == null)
                return false;

            var active = value.ToString() == parameter.ToString();
            return active;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
