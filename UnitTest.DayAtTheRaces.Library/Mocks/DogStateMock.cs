using DayAtTheRaces.Library.Interfaces;
using System;

namespace UnitTest.DayAtTheRaces.Library.Mocks
{
    public class DogStateMock : IDogState
    {
        public double GetsDistracted(double dogStandardMtsCoveredIn1Second, double secondsDelayed)
        {
            return 0;
        }

        public double GetsFaster(double dogStandardMtsCoveredIn1Second)
        {
            return 10.5;
        }

        public double GetsTired(double dogStandardMtsCoveredIn1Second)
        {
            return 8;
        }
    }
}
