using System;
using System.Globalization;
using System.Windows.Data;

namespace FinancasDesktop.Classes.Convertions
{
    public class ConvertMoeda : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal valor = 0;
            CultureInfo ci = new CultureInfo("pt-BR");
            try
            {
                valor = value.ToDecimal();
                return string.Format(ci, "{0:C}", valor);
            }
            catch
            {
                valor = 0;
                return string.Format(ci, "{0:C}", valor);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
