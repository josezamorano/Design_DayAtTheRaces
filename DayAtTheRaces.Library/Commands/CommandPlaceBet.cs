using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Common.Extensions;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using DayAtTheRaces.Library.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace DayAtTheRaces.Library.Commands
{
    public class CommandPlaceBet : ICommandPlaceBet
    {
        public ActionCommandEnum ActionCommand { get; set; }
        IBettingBank _bettingBank;
        IDogRacePalaceStadium _dogRacePalaceStadium;
        IRacetrack _racetrack;
        public CommandPlaceBet(IBettingBank bettingBank, IDogRacePalaceStadium dogRacePalaceStadium, IRacetrack racetrack)
        {
            ActionCommand = ActionCommandEnum.PlaceBet;
            _bettingBank = bettingBank;
            _dogRacePalaceStadium = dogRacePalaceStadium;
            _racetrack = racetrack;
        }
        public Notification Execute(object obj = null)
        {
            Notification notification = new Notification();
            BettingAccount bettingAccount = (BettingAccount)obj;
            GuyNameEnum betGuyName = bettingAccount.BettingAccountHolderName;
            DogNameEnum betDogName = bettingAccount.BettingDogNameSelected;
            int betAmount = bettingAccount.BettingAmount;
            IEnumerable<IGuy> allGuysInTheStadium = _dogRacePalaceStadium.GetAllGuysInTheStadium();
            IGuy selectedGuyInStadium = allGuysInTheStadium.Where(a => a.Name == betGuyName).FirstOrDefault();
           
            if (selectedGuyInStadium == null)
            {
                notification.Description = AllNotifications.NoGuyAddedToTheStadiumWarning();
                notification.Code = NotificationEnum.Warning;
            }
            else 
            {
                var guyCashBalance = selectedGuyInStadium.GetMoneyBalance();
                if(guyCashBalance< betAmount)
                {
                    notification.Description = AllNotifications.BetNotEnoughCashWarning(betGuyName.ToString(), betAmount, (int)guyCashBalance);
                    notification.Code = NotificationEnum.Warning;
                }
                else
                {
                    selectedGuyInStadium.WithdrawMoney(betAmount);
                    _bettingBank.AddBettingAccountToBettingBank(bettingAccount);

                    notification.Description = ClientValidation.StandardBettingNotification();
                    notification.Code = NotificationEnum.Ok;
                }
            }
            notification.Items = allGuysInTheStadium.Count();
            return notification;
        }
    }
}
