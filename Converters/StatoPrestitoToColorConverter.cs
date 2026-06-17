using BibliotecaWPF.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BibliotecaWPF.Converters
{
    public class StatoPrestitoToColorConverter : IValueConverter
    {
        private static readonly Brush AttivoBrush = Brushes.White;
        private static readonly Brush InRitardoBrush =
            (Brush)new BrushConverter().ConvertFrom("#FFDDDD");
        private static readonly Brush RestituitoBrush =
            (Brush)new BrushConverter().ConvertFrom("#DDFFDD");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not StatoPrestito stato)
                return AttivoBrush;

            return stato switch
            {
                StatoPrestito.InRitardo => InRitardoBrush,
                StatoPrestito.Restituito => RestituitoBrush,
                _ => AttivoBrush
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}