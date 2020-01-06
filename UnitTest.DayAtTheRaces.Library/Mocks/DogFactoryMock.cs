using DayAtTheRaces.Library;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;

namespace UnitTest.DayAtTheRaces.Library.Mocks
{
    public class DogFactoryMock : IDogFactory
    {
        public IDog CreateDog(DogBreedEnum dogBreed)
        {
             IDog dog = new Dog();
            return dog;
        }
    }
}
