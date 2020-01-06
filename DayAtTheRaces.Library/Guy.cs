using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Interfaces;

namespace DayAtTheRaces.Library
{
    public class Guy : IGuy
    {
        private double moneyInPocket;

        public GuyNameEnum Name { get; set; }
        public double GetMoneyBalance()
        {
            return moneyInPocket;
        }

        public void SetDeposit(double money)
        {
            moneyInPocket += money;
        }

        public void WithdrawMoney(double money)
        {
            moneyInPocket -= money;
        }
        
        public void CollectWinningBet(double money)
        {
            moneyInPocket += money;
        }

        public string GetGuyDescription()
        {
            return $"{Name} has ${moneyInPocket} Bucks";
        }
    }
}
