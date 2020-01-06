using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.Library.Models;
using System.Collections.Generic;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IBettingBank
    {
        void AddBettingAccountToBettingBank(BettingAccount bettingAccount);
        List<BettingAccount> GetWinnerBets(DogNameEnum winnerDog);
        string ClearBets();
        IEnumerable<BettingAccount> GetAllActiveBets();
        IEnumerable<string> GetAllBetsAsDescriptions();
    }
}
