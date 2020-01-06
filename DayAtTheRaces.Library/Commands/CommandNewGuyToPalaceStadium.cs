using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using DayAtTheRaces.Library.Notifications;
using System.Linq;

namespace DayAtTheRaces.Library.Commands
{
    public class CommandNewGuyToPalaceStadium : ICommandNewGuyToPalaceStadium
    {
        public ActionCommandEnum ActionCommand { get; set; }
        IDogRacePalaceStadium _dogRacePalaceStadium;
        public CommandNewGuyToPalaceStadium(IDogRacePalaceStadium dogRacePalaceStadium)
        {
            ActionCommand = ActionCommandEnum.NewGuyToPalaceStadium;
            _dogRacePalaceStadium = dogRacePalaceStadium;
        }

        public Notification Execute(object obj = null)
        {
            var guyName = (GuyNameEnum)obj;
            var notification = new Notification();
            var allGuys = _dogRacePalaceStadium.GetAllGuysInTheStadium();
            var existingGuy = allGuys.Where(a => a.Name == guyName).FirstOrDefault();
            if(existingGuy != null)
            {
                notification.Code = NotificationEnum.Warning;
                notification.Description = AllNotifications.GuyAddedToStadiumWarning(existingGuy.Name.ToString());
                notification.Items = allGuys.Count();
            }
            else
            {
                IGuy newGuy = _dogRacePalaceStadium.CreateGuy(guyName);
                _dogRacePalaceStadium.AddGuyToTheStadium(newGuy);
                notification.Code = NotificationEnum.Ok;
                notification.Description = AllNotifications.GuyAddedToStadiumOk(newGuy.Name.ToString());
                notification.Items = _dogRacePalaceStadium.GetAllGuysInTheStadium().Count();
            }
            return notification;
        }
    }
}
