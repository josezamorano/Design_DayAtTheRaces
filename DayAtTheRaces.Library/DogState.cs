using DayAtTheRaces.Library.Interfaces;
using System;

namespace DayAtTheRaces.Library
{
    public class DogState : IDogState
    {
        public double GetsFaster(double dogStandardMtsCoveredIn1Second)
        {
            Random rand = new Random();
            int mtsFaster = rand.Next(3, 30);
            return (dogStandardMtsCoveredIn1Second + mtsFaster);
        }

        public double GetsTired(double dogStandardMtsCoveredIn1Second)
        {
            int standardMts = (int)dogStandardMtsCoveredIn1Second;
            Random rand = new Random();
            int mtsSlower = rand.Next(0, standardMts);
            return (dogStandardMtsCoveredIn1Second - mtsSlower);
        }

        public double GetsDistracted(double dogStandardMtsCoveredIn1Second, double secondsDelayed)
        {
            Random rand = new Random();
            double secondsSlower = rand.Next(2, 5);
            double penaltyTimeDelay = (secondsDelayed + secondsSlower / 10);
            double lessMetresCovered = dogStandardMtsCoveredIn1Second / penaltyTimeDelay;
            return ((int)secondsDelayed != 0) ? lessMetresCovered : dogStandardMtsCoveredIn1Second;
        }
    }
}
