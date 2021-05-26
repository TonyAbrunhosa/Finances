using System;
using System.Globalization;
using System.Windows.Data;

namespace FinancasDesktop.Classes.Convertions
{
    public class ConvertDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if ((value.GetType() == typeof(DateTime)))
                        return ((DateTime)value).ToString();
                    else if (value != null)
                    {
                        DateTime? vdata = value.ToString().Date();
                        if (vdata != null)
                            return vdata.Value.ToString();
                    }
                }

                return "";
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
