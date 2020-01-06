using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using DayAtTheRaces.Library.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DayAtTheRaces.Library.Commands
{
    public class CommandNewDogToRacetrack : ICommandNewDogToRacetrack
    {
        IRacetrack _racetrack;
        public ActionCommandEnum ActionCommand { get; set; }
        public CommandNewDogToRacetrack(IRacetrack racetrack)
        {
            _racetrack = racetrack;
            ActionCommand = ActionCommandEnum.NewDogToRacetrack;
        }
        
        private int maxNumberOfDogsInRacetrack = 4;
        public Notification Execute(object obj = null)
        {
            List<Image> dogImages = (List<Image>)obj;
            var notification = new Notification();
            var totalDogs = _racetrack.GetAllDogsInRacetrack().Count;
            if (totalDogs < maxNumberOfDogsInRacetrack)
            {
                IDog newDog = _racetrack.CreateDog(DogBreedEnum.Greyhound);
                newDog.DogImage = dogImages[totalDogs];
                _racetrack.AddDogToRacetrack(newDog);
                notification.Code = NotificationEnum.Ok;
                notification.Description = AllNotifications.NewDogToRacetrackOk(newDog.Name.ToString());
                notification.Items = _racetrack.GetAllDogsInRacetrack().Count();
                return notification;
            }

            notification.Code = NotificationEnum.Warning;
            notification.Description = AllNotifications.NewDogToRacetrackWarning();
            notification.Items = totalDogs;
            return notification;
        }
    }
}
