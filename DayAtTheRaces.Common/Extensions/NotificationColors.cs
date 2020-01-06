using DayAtTheRaces.Common.Enums;
using System.Windows.Media;

namespace DayAtTheRaces.Common.Extensions
{
    public class NotificationColors
    {
        public static SolidColorBrush GetBrushColor(NotificationEnum notificationCode)
        {
            if (notificationCode == NotificationEnum.Warning) { return Brushes.Red; }
            if (notificationCode == NotificationEnum.Ok) { return Brushes.DarkGreen; }
            if (notificationCode == NotificationEnum.Standard) { return Brushes.Black; }

            return Brushes.Black;
        }
    }
}
