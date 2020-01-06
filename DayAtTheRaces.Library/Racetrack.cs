using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayAtTheRaces.Library
{
    public class Racetrack : IRacetrack
    {
        IDogState _dogState;
        IDogFactory _dogFactory;
        private List<IDog> racingDogs = new List<IDog>();
        public Racetrack(IDogState dogState, IDogFactory dogFactory)
        {
            _dogState = dogState;
            _dogFactory = dogFactory;
        }
        
        public IDog CreateDog(DogBreedEnum dogBreedEnum)
        {
            IDog newDog = _dogFactory.CreateDog(dogBreedEnum);
            return ProvidePropertiesToDog(newDog);
        }

        public void AddDogToRacetrack(IDog dog)
        {
            racingDogs.Add(dog);
        }

        public void RemoveAllDogsFromRacetrack()
        {
            racingDogs.Clear();
        }

        public List<IDog> GetAllDogsInRacetrack()
        {
            return racingDogs;
        }

        public double RaceGo(double initialMtsCoveredByDogIn1Sec)
        {
            Random rand = new Random();
            double distanceCovered = 0;
            double secondsTimeDelay = rand.Next(0, 5);

            int dogState = rand.Next(1, 6);
            switch (dogState)
            {
                case (1): //Dog runs ats its standard speed
                    distanceCovered = initialMtsCoveredByDogIn1Sec;
                    break;
                case (2): //Dog runs at maximum speed
                    distanceCovered = _dogState.GetsFaster(initialMtsCoveredByDogIn1Sec);
                    break;
                case (3): //Dog Gets tired
                    distanceCovered = _dogState.GetsTired(initialMtsCoveredByDogIn1Sec);
                    break;
                case (4): //Dog gets distracted
                    distanceCovered = _dogState.GetsDistracted(initialMtsCoveredByDogIn1Sec, secondsTimeDelay);
                    break;
                case (5): //Dog gets delayed and covers no distance
                    distanceCovered = 0;
                    break;
            }
            return distanceCovered;
        }


        #region Private Helper Methods
        private IDog ProvidePropertiesToDog(IDog dog)
        {
            var allDogsNames = Enum.GetValues(typeof(DogNameEnum)).Cast<DogNameEnum>().ToList();

            if (allDogsNames.Count >= racingDogs.Count)
                dog.Name = allDogsNames[racingDogs.Count];
            else return null;
            
            dog.GatePosition = racingDogs.Count + 1;
            dog.DistanceCoveredByDog = 0;
            Random rand = new Random();
            double speedKmsHr = rand.Next(40, 45);
            dog.SetMaxSpeedInKmsPer1Hour(speedKmsHr);
            return dog;
        }
        #endregion
    }
}
