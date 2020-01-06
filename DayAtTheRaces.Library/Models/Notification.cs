using DayAtTheRaces.Common.Enums;

namespace DayAtTheRaces.Library.Models
{
    public class Notification
    {
        public NotificationEnum Code { get; set; }
        public string Description { get; set; }
        public int Items { get; set; }
    }
}
