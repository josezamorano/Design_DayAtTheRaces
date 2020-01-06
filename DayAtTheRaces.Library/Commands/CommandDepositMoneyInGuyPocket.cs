using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using DayAtTheRaces.Library.Notifications;
using System.Linq;

namespace DayAtTheRaces.Library.Commands
{
    public class CommandDepositMoneyInGuyPocket : ICommandDepositMoneyInGuyPocket
    {
        public ActionCommandEnum ActionCommand { get; set; }
      
        private IDogRacePalaceStadium _dogRacePalaceStadium;
        private double _depositAmount;

        public CommandDepositMoneyInGuyPocket(IDogRacePalaceStadium dogRacePalaceStadium)
        {
            ActionCommand = ActionCommandEnum.DepositMoneyInGuyPocket;
            _dogRacePalaceStadium = dogRacePalaceStadium;
        }

        public CommandDepositMoneyInGuyPocket(IDogRacePalaceStadium dogRacePalaceStadium,GuyNameEnum guyName, double depositAmount)
        {
            ActionCommand = ActionCommandEnum.DepositMoneyInGuyPocket;
            _dogRacePalaceStadium = dogRacePalaceStadium;
            _depositAmount = depositAmount;
        }
        public Notification Execute(object obj = null)
        {
            var notification = new Notification();
            CashDeposit cashDeposit = (CashDeposit)obj;
            var allGuysInTheStadium = _dogRacePalaceStadium.GetAllGuysInTheStadium();
            var selectedGuyInStadium = allGuysInTheStadium.Where(a => a.Name == cashDeposit.GuyName).FirstOrDefault();
            if(selectedGuyInStadium != null)
            {
                selectedGuyInStadium.SetDeposit(cashDeposit.CashAmount);
                var cashBalance = selectedGuyInStadium.GetMoneyBalance();
                notification.Code = NotificationEnum.Ok;
                notification.Description = AllNotifications.CashDepositOk(cashDeposit.GuyName.ToString(), (int)cashDeposit.CashAmount, (int)cashBalance);
                notification.Items = allGuysInTheStadium.Count();
            }
            else
            {
                notification.Code = NotificationEnum.Warning;
                notification.Description = AllNotifications.NoGuyAddedToTheStadiumWarning();
                notification.Items = allGuysInTheStadium.Count();
            }
            return notification;
        }
    }
}
