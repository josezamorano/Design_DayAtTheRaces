using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;
using DayAtTheRaces.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace DayAtTheRaces.Library
{
    public class BettingBank : IBettingBank
    {
        private List<BettingAccount> bettingAccountsHolder = new List<BettingAccount>();
        private bool _WinningBetNotified = false;
        
        public void AddBettingAccountToBettingBank(BettingAccount bettingAccount)
        {
            bettingAccountsHolder.Add(bettingAccount);
        }

        public List<BettingAccount> GetWinnerBets(DogNameEnum winnerDog)
        {
            var winningAccounts = bettingAccountsHolder.Where(a => a.BettingDogNameSelected == winnerDog).ToList();
            _WinningBetNotified = true;
            return winningAccounts;
        }

        public IEnumerable<BettingAccount> GetAllActiveBets()
        {
            return bettingAccountsHolder;
        }

        public IEnumerable<string> GetAllBetsAsDescriptions()
        {
            List<string> betsDescriptions = new List<string>();
            foreach(var bet in bettingAccountsHolder)
            {
                var betDescription = $"{bet.BettingAccountHolderName} has placed a bet of{bet.BettingAmount} Bucks in {bet.BettingDogNameSelected}";
                betsDescriptions.Add(betDescription);
            }
            return betsDescriptions;
        }

        public string ClearBets()
        {
            string notification = string.Empty;
            if(_WinningBetNotified == true)
            {
                bettingAccountsHolder.Clear();
                _WinningBetNotified = false;
                notification = "All Bets Cleared";
            }

            return notification;
        }
    }
}
