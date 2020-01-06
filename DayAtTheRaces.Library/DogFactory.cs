using DayAtTheRaces.Library.DependencyInjection;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;

namespace DayAtTheRaces.Library
{
    public class DogFactory: IDogFactory
    {
        public IDog CreateDog(DogBreedEnum dogBreed)
        {
            if(dogBreed == DogBreedEnum.Greyhound)
            {
                return ContainerConfig.GetInstance<IDog>();
            }
            return null;
        }
    }
}
