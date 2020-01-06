using DayAtTheRaces.Library.Enums;
using System.Windows.Controls;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IDog
    {
        Image DogImage{ get; set; }
        DogNameEnum Name { get; set; }
        double DistanceCoveredByDog { get; set; }
        int GatePosition { get; set; }
        int RaceIteration { get; set; }
        bool WinnerDog { get; set; }
        void SetMaxSpeedInKmsPer1Hour(double speed);
        double GetMaxSpeedKmsPer1Hour();
        double GetStandardMaxMetresCoveredPer1Second();
    }
}
