using DayAtTheRaces.Library.Enums;
using System.Collections.Generic;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IRacetrack
    {
        IDog CreateDog(DogBreedEnum dogBreedEnum);
        void AddDogToRacetrack(IDog dog);
        void RemoveAllDogsFromRacetrack();
        List<IDog> GetAllDogsInRacetrack();
        double RaceGo(double initialMtsCoveredByDogIn1Sec);
    }
}
