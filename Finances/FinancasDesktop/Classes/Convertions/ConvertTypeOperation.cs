using System;
using System.Globalization;
using System.Windows.Data;

namespace FinancasDesktop.Classes.Convertions
{
    public class ConvertTypeOperation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.Format("{0}", value).Equals("C")) return "COMPRA";
            else return "VENDA";
         }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
