using RaceSimulatorShared.Models.Participants;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RaceSimulatorWPF;

public class TeamColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TeamColor teamColor)
        {
            Color color = teamColor switch
            {
                TeamColor.Red => Colors.Red,
                TeamColor.Green => Colors.Green,
                TeamColor.Blue => Colors.Blue,
                TeamColor.Yellow => Colors.Yellow,
                TeamColor.Grey => Colors.Gray,
                _ => Colors.Transparent,
            };

            Color darkerColor = Color.Multiply(color, 0.7f);

            return new SolidColorBrush(darkerColor);
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
