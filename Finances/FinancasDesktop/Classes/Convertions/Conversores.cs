using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FinancasDesktop.Classes.Convertions
{
    public static class Conversores
    {
        private static CultureInfo provider = new CultureInfo("pt-BR");
        public static int? ToIntOrNull(this string valor)
        {
            int resultado;

            if (string.IsNullOrEmpty(valor))
            {
                return null;
            }
            else if (int.TryParse(valor, out resultado))
            {
                return resultado;
            }
            return null;
        }
        public static DateTime? Date(this string value)
        {
            return Data(value, 'A');
        }

        private static DateTime? Data(String value, char formato)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (formato)
                    {
                        case 'A': return DateTime.Parse(value, provider);
                        case 'B': return DateTime.Parse(String.Format("{0}-{1}-{2}", value.Substring(0, 4), value.Substring(4, 2), value.Substring(6)));
                        case 'C': return DateTime.Parse(String.Format("{0}-{1}-{2}", value.Substring(4, 4), value.Substring(2, 2), value.Substring(0, 2)));
                        case 'D': return DateTime.Parse(value, provider);
                    }
                }
            }
            catch { }

            return null;
        }
        public static int ToInt(this object valor)
        {
            int resultado;

            if (valor != null)
            {
                int.TryParse(valor.ToString(), out resultado);
            }
            else
            {
                return 0;
            }
            return resultado;
        }

        public static decimal ToDecimal(this object value)
        {
            try
            {
                string resultado;
                if (value == null)
                {
                    return 0;
                }

                resultado = value.ToString().Replace("R$ ", "").Replace("%", "");

                if (Decimal(resultado))
                {
                    return decimal.Parse(resultado, provider);
                }
            }
            catch { }
            return 0;
        }

        public static decimal? ToDecimalOrNull(this object value)
        {
            try
            {
                string resultado;
                if (value == null)
                {
                    return null;
                }

                resultado = value.ToString().Replace("R$ ", "").Replace("%", "");

                if (Decimal(resultado))
                {
                    return decimal.Parse(resultado, provider);
                }
            }
            catch { }
            return null;
        }
        private static bool Decimal(string valor)
        {
            try
            {
                if (decimal.TryParse(valor, out decimal resultado))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public static string EncodeToBase64(this string texto)
        {
            try
            {
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception ex)
            {
                LogEx.GeraLogSimples("Erro EncodeToBase64\n" + ex.StackTrace);
                return null;
            }
        }
        //converte de base64 para texto
        public static string DecodeFrom64(this string dados)
        {
            try
            {
                byte[] dadosAsBytes = System.Convert.FromBase64String(dados);
                string resultado = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                return resultado;
            }
            catch (Exception ex)
            {
                LogEx.GeraLogSimples("Erro DecodeFrom64\n" + ex.StackTrace);
                return null;
            }
        }
       
        public static DateTime? ToDateTimeOrNull(this object value)
        {
            if (value == null)
                return null;

            DateTime result;

            if (DateTime.TryParse(value.ToString(), out result))
                return (DateTime?)result;

            return null;
        }
    }
}
