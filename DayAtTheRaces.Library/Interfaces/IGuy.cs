using DayAtTheRaces.Library.Enums;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IGuy
    {
        GuyNameEnum Name { get; set; }
        double GetMoneyBalance();
        void SetDeposit(double money);
        void WithdrawMoney(double money);
        void CollectWinningBet(double money);
        string GetGuyDescription();
    }
}
