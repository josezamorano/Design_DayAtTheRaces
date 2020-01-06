using DayAtTheRaces.Library.DependencyInjection;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using System.Collections.Generic;

namespace DayAtTheRaces.Library
{
    public class DogRacePalaceStadium: IDogRacePalaceStadium
    {
        private List<IGuy> guysInTheStadium = new List<IGuy>();

        public IGuy CreateGuy(GuyNameEnum guyName)
        {
            IGuy guy = ContainerConfig.GetInstance<IGuy>();
            guy.Name = guyName;
            guy.SetDeposit(0);

            return guy;
        }

        public void AddGuyToTheStadium(IGuy guy)
        {
            guysInTheStadium.Add(guy);
        }

        public void RemoveGuyFromTheStadium(IGuy guy)
        {
            guysInTheStadium.Remove(guy);
        }

        public IEnumerable<IGuy> GetAllGuysInTheStadium()
        {
            return guysInTheStadium;
        }
    }
}
