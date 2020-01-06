using DayAtTheRaces.Library.DependencyInjection;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using System.Collections.Generic;

namespace DayAtTheRaces.Library
{
    public class CommandInvoker :ICommandInvoker
    {
        private List<ICommand> allCommands = new List<ICommand>();
        public CommandInvoker()
        {
            allCommands.Add(ContainerConfig.GetInstance<ICommandDepositMoneyInGuyPocket>());
            allCommands.Add(ContainerConfig.GetInstance<ICommandNewDogToRacetrack>());
            allCommands.Add(ContainerConfig.GetInstance<ICommandNewGuyToPalaceStadium>());
            allCommands.Add(ContainerConfig.GetInstance<ICommandPlaceBet>());
            allCommands.Add(ContainerConfig.GetInstance<ICommandRaceGo>());
        }

        public ICommand GetCommand(ActionCommandEnum action)
        {
            foreach(var commandObj in allCommands)
            {
                if (commandObj.ActionCommand == action)
                {
                    return commandObj;
                }
            }
            return null;
        }
    }
}
