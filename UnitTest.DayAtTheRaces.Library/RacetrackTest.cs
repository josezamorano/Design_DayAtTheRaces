using DayAtTheRaces.Library;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.DayAtTheRaces.Library.Mocks;

namespace UnitTest.DayAtTheRaces.Library
{
    [TestClass]
    public class RacetrackTest
    {
        IDogState _dogState = new DogStateMock();
        IDogFactory _dogFactory = new DogFactoryMock();
        IRacetrack _racetrack;

        public RacetrackTest()
        {
            _racetrack = new Racetrack(_dogState, _dogFactory);
        }

        [TestMethod]
        public void CreateDog_Test()
        {
            IDog newDog = _racetrack.CreateDog(DogBreedEnum.Greyhound);
            var result = (newDog).GetType() ;
            newDog.Should().BeOfType(typeof(Dog));
            newDog.Should().NotBeNull();
        }

        [TestMethod]
        public void AddDogToRacetrack_Test()
        {
            var newDog = _racetrack.CreateDog(DogBreedEnum.Greyhound);
            _racetrack.AddDogToRacetrack(newDog);
            var result = _racetrack.GetAllDogsInRacetrack();

            (result.Count == 1).Should().BeTrue();
        }

        [TestMethod]
        public void RemoveAllDogsFromRacetrack_Test()
        {
            _racetrack.RemoveAllDogsFromRacetrack();
            var result = _racetrack.GetAllDogsInRacetrack();

            (result.Count == 0).Should().BeTrue();
        }

        [TestMethod]
        public void GetAllDogsInRacetrack_Test()
        {
            _racetrack.RemoveAllDogsFromRacetrack();
            var newDog = _racetrack.CreateDog(DogBreedEnum.Greyhound);
            _racetrack.AddDogToRacetrack(newDog);
            var result = _racetrack.GetAllDogsInRacetrack();

            (result.Count == 1).Should().BeTrue();
        }

        [TestMethod]
        public void RaceGo_Test()
        {
           var result = _racetrack.RaceGo(10);

            (result >= 0).Should().BeTrue();
        }
    }
}
