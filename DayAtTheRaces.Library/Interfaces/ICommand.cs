using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Models;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface ICommand
    {
        ActionCommandEnum ActionCommand { get; set; }
        Notification Execute(object obj = null);
    }
}
