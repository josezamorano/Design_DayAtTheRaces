using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using System.Windows.Controls;

namespace DayAtTheRaces.Library
{
    public class Dog: IDog
    {
        private double _maxSpeedKms;
        public Image DogImage { get; set; }
        public DogNameEnum Name { get; set; }
        public double DistanceCoveredByDog { get; set; }
        public int GatePosition { get; set; }
        public int RaceIteration { get; set; }
        public bool WinnerDog { get; set; }
        
        public void SetMaxSpeedInKmsPer1Hour(double speed)
        {
            this._maxSpeedKms = speed;
        }

        public double GetMaxSpeedKmsPer1Hour()
        {
            return this._maxSpeedKms;
        }

        public double GetStandardMaxMetresCoveredPer1Second()
        {
            //Speed = Total KMs / 1 hour
            //1 hour = 60 min
            //1 min = 60 seconds
            //1 hour = 3600 seconds
            // -------------------------
            //1 km = 1000 mts
            //Max Speed mts per second = Speed Km *(1000 mts / 3600 seconds)
            double totalMtsIn1Km = 1000.00;
            double totalSecondsIn1Hour = 3600.00;

            double totalMtsIn1Second = this._maxSpeedKms * (totalMtsIn1Km / (totalSecondsIn1Hour));
            return totalMtsIn1Second;
        }
        
    }
}
