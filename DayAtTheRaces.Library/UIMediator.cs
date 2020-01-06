using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Common.Extensions;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using DayAtTheRaces.Library.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DayAtTheRaces.Library
{
    public class UIMediator: IUIMediator
    {
        ICommandInvoker _commandInvoker;
        IDogRacePalaceStadium _dogRacePalaceStadium;
        IRacetrack _racetrack;
        IBettingBank _bettingBank;
        public UIMediator(ICommandInvoker commandInvoker, IDogRacePalaceStadium dogRacePalaceStadium,IRacetrack racetrack, IBettingBank bettingBank)
        {
            _commandInvoker = commandInvoker;
            _dogRacePalaceStadium = dogRacePalaceStadium;
            _racetrack = racetrack;
            _bettingBank = bettingBank;
        }
        private int racetrackDistance = -1;
        
        public void TrackDistanceComboBox_SelectionChanged(int selectedItemValue,Label labelNotification)
        {
            racetrackDistance = selectedItemValue;
            labelNotification.Content = AllNotifications.DistanceSetOk();
            labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Ok);
        }

        public int BtnAddDog_Click(ComboBox comboBoxDogs, Label labelNotification, List<Image> dogImages)
        {
            ICommand newDogCommand =_commandInvoker.GetCommand(ActionCommandEnum.NewDogToRacetrack);
            var notification = newDogCommand.Execute(dogImages);
            if (notification.Items < 2)
            {
                labelNotification.Content = notification.Description +"\nMin number of dogs for a race is 2";
                labelNotification.Foreground = NotificationColors.GetBrushColor(notification.Code);
            }
            else if(notification.Items>=2)
            {
                labelNotification.Content = notification.Description;
                labelNotification.Foreground = NotificationColors.GetBrushColor(notification.Code);
            }
            var allDogsNames = _racetrack.GetAllDogsInRacetrack().Select(a => a.Name).ToList();
            comboBoxDogs.ItemsSource = allDogsNames;
            return notification.Items;
        }

        public void BtnAddGuy_Click(ComboBox comboBoxGuys, Label labelNotification, GuyNameEnum guyName)
        {
            ICommand newGuyCommand = _commandInvoker.GetCommand(ActionCommandEnum.NewGuyToPalaceStadium);
            var notification = newGuyCommand.Execute( guyName);
            var allGuys = (List<IGuy>)_dogRacePalaceStadium.GetAllGuysInTheStadium();
            comboBoxGuys.ItemsSource = allGuys.Select(a => a.Name).ToList();
            labelNotification.Content = notification.Description;
            labelNotification.Foreground =NotificationColors.GetBrushColor(notification.Code);
        }

        public void BtnMakeDeposit_Click(Label labelNotification, GuyNameEnum guyName, int amountDeposit)
        {
            ICommand makeDepositCommand = _commandInvoker.GetCommand(ActionCommandEnum.DepositMoneyInGuyPocket);
            CashDeposit deposit = new CashDeposit() { GuyName = guyName, CashAmount = amountDeposit };
            var notification = makeDepositCommand.Execute(deposit);
            
            labelNotification.Content = notification.Description;
            labelNotification.Foreground = NotificationColors.GetBrushColor(notification.Code);
        }

        public void BtnPlaceBet_Click(Label LblBetNotification,List<Label> allGuysLabels,ListBox listBoxBetsPlaced ,GuyNameEnum guyName,DogNameEnum dogName,int betAmount)
        {
            ICommand placeBetCommand = _commandInvoker.GetCommand(ActionCommandEnum.PlaceBet);
            BettingAccount bettingAccount = new BettingAccount() { BettingAccountHolderName=guyName,BettingDogNameSelected=dogName,BettingAmount=betAmount};
            var notification = placeBetCommand.Execute(bettingAccount);

            LblBetNotification.Content = notification.Description;
            LblBetNotification.Foreground = NotificationColors.GetBrushColor(notification.Code);
            listBoxBetsPlaced.ItemsSource = _bettingBank.GetAllBetsAsDescriptions();
            UpdateGuysLabels(allGuysLabels);
        }

        public void BtnStartRace_Click(ListBox ListBoxBetsPlaced, List<Label> allGuysLabels, List<Label> winnerDogLabels, List<Label>standardLabels)
        {
            ICommand RaceGoCommand = _commandInvoker.GetCommand(ActionCommandEnum.RaceGo);
            Notification notification = RaceGoCommand.Execute(racetrackDistance);
            if(notification.Code == NotificationEnum.Ok)
            {
                CollectWinningBets(notification, ListBoxBetsPlaced);
                NotifyWinningDog(winnerDogLabels, notification.Description);
                SetStandardNotifications(standardLabels);
            }
            UpdateGuysLabels(allGuysLabels);
        }


        #region Helper Methods
        private void NotifyWinningDog(List<Label> WinnerDogLabels,string NotificationInfo)
        {
            var info = NotificationInfo.Split('-');
            for(var a = 0; a<WinnerDogLabels.Count; a++)
            {
                WinnerDogLabels[a].Content = info[a];
            }
        }

        private void CollectWinningBets(Notification notification, ListBox ListBoxBetsPlaced)
        {
            var winnerInfo = notification.Description.Split('-');
            var dogNameString = winnerInfo[0].Trim();
            List<DogNameEnum> allDogs = Enum.GetValues(typeof(DogNameEnum)).Cast<DogNameEnum>().ToList();
            var winnerDog = allDogs.Where(a => a.ToString() == dogNameString).FirstOrDefault();
            var winnerBettingGuys = _bettingBank.GetWinnerBets(winnerDog);
            var allGuysInTheStadium = _dogRacePalaceStadium.GetAllGuysInTheStadium();
            foreach (var winner in winnerBettingGuys)
            {
                var winnerGuy = allGuysInTheStadium.Where(a => a.Name == winner.BettingAccountHolderName).FirstOrDefault();
                var winningAmount = winner.BettingAmount * 2;
                winnerGuy.CollectWinningBet(winningAmount);
            }

            _bettingBank.ClearBets();
            ListBoxBetsPlaced.ItemsSource = new List<string>() { };
        }

        private void SetStandardNotifications(List<Label> standardLabels)
        {
            foreach(var label in standardLabels)
            {
                label.Content = ClientValidation.StandardNotification();
                label.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Standard);
            }
        }

        private void UpdateGuysLabels(List<Label> allGuysLabels)
        {
            List<IGuy> guys = (List<IGuy>)_dogRacePalaceStadium.GetAllGuysInTheStadium();
            var Al = guys.Where(a => a.Name == GuyNameEnum.Al).FirstOrDefault();
            var Joe = guys.Where(a => a.Name == GuyNameEnum.Joe).FirstOrDefault();
            var Bob = guys.Where(a => a.Name == GuyNameEnum.Bob).FirstOrDefault();
            allGuysLabels[0].Content = (Al != null) ? Al.GetGuyDescription() : string.Empty;
            allGuysLabels[1].Content = (Joe != null) ? Joe.GetGuyDescription() : string.Empty;
            allGuysLabels[2].Content = (Bob != null) ? Bob.GetGuyDescription() : string.Empty;
        }
        #endregion
    }
}
