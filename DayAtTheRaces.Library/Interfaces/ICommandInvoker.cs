using DayAtTheRaces.Library.Enums;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface ICommandInvoker
    {
        ICommand GetCommand(ActionCommandEnum action);
    }
}
