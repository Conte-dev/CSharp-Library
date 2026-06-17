using System;
using System.Globalization;
using System.Windows.Data;
using BibliotecaWPF.Models;

namespace BibliotecaWPF.Converters
{
    public class StatoPrestitoToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not StatoPrestito stato)
                return string.Empty;

            return stato switch
            {
                StatoPrestito.Attivo => "⏳",
                StatoPrestito.InRitardo => "⚠️",
                StatoPrestito.Restituito => "✔️",
                _ => string.Empty
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}