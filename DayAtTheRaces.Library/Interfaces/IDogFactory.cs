using DayAtTheRaces.Library.Enums;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IDogFactory
    {
        IDog CreateDog(DogBreedEnum dogBreed);
    }
}
