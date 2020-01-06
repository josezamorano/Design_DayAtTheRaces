using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

namespace DayAtTheRaces.Library.Commands
{
    public class CommandRaceGo : ICommandRaceGo
    {
        private IRacetrack _racetrack;
        public ActionCommandEnum ActionCommand { get; set; }

        public CommandRaceGo(IRacetrack racetrack)
        {
            ActionCommand = ActionCommandEnum.RaceGo;
            _racetrack = racetrack;
        }

        public Notification Execute(object obj = null)
        {
            int totalDistance = (int)obj, iteration = 1;
            Notification notification = new Notification();
            List<IDog> dogs = _racetrack.GetAllDogsInRacetrack();
            Stopwatch timer = Stopwatch.StartNew();
            bool keepRacing = true;
            while (keepRacing)
            {
                foreach (var dog in dogs)
                {
                    double initialDistanceCoveredByADog = dog.GetStandardMaxMetresCoveredPer1Second();
                    var distance = _racetrack.RaceGo(initialDistanceCoveredByADog);
                    dog.DistanceCoveredByDog += distance;
                    dog.RaceIteration = iteration;
                    //==Moving the picture BEGIN==========================================
                    var positionImage = Canvas.GetLeft(dog.DogImage);
                    var newPosition = positionImage + distance;
                    Canvas.SetLeft(dog.DogImage, newPosition);
                    //==Moving the picture END============================================
                }
                var winnerDogName = DogWonTheRace(totalDistance, dogs);
                if (winnerDogName != 0)
                {
                    var winnerdog = dogs.Where(a => a.Name == winnerDogName).FirstOrDefault();
                    winnerdog.WinnerDog = true;
                    break;
                }
                iteration++;
            }
            timer.Stop();
            TimeSpan timespan = timer.Elapsed;
            var totalRaceTime = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
            var winnerDog = dogs.Where(a => a.WinnerDog == true).FirstOrDefault();
            notification.Code = Common.Enums.NotificationEnum.Ok;
            notification.Description = $"{winnerDog.Name} - {totalDistance} - {totalRaceTime}";
            notification.Items = dogs.Count();
            return notification;
        }

        private DogNameEnum DogWonTheRace(int totalDistance,List<IDog> dogs)
        {
            //We order the distances from the highest to the lowest.
            var topDistanceDog = dogs.OrderByDescending(c => c.DistanceCoveredByDog).FirstOrDefault();
            var distanceCovered = topDistanceDog.DistanceCoveredByDog;
            //And check if only one of more than one dog has covered the same distance
            //In case more than one dog has covered the same distance, we add randomly 1 metre to any dog.
            List<IDog> dogsTopDistanceList = dogs.Where(a => a.DistanceCoveredByDog == distanceCovered).ToList();
            if (dogsTopDistanceList.Count > 1)
            {
                Random rnd = new Random();
                int maxVal = dogsTopDistanceList.Count + 1;
                int value = rnd.Next(1, maxVal);
                dogsTopDistanceList[value - 1].DistanceCoveredByDog += 1;
            }
            //Now when we are sure there is only one dog which is covering the longest distance, we select it from the list of dogs racing
            var winDog = dogs.OrderByDescending(c => c.DistanceCoveredByDog).FirstOrDefault();
            //if the dog covered the total distance already, we break the loop
            if (totalDistance <= winDog.DistanceCoveredByDog)
                return winDog.Name;
           
            return 0;
        }
    }
}
