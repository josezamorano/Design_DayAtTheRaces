using DayAtTheRaces.Library.Enums;
using System.Collections.Generic;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IDogRacePalaceStadium
    {
        IGuy CreateGuy(GuyNameEnum guyName);
        void AddGuyToTheStadium(IGuy guy);
        void RemoveGuyFromTheStadium(IGuy guy);
        IEnumerable<IGuy> GetAllGuysInTheStadium();
    }
}
