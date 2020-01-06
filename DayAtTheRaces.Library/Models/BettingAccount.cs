using DayAtTheRaces.Library.Enums;

namespace DayAtTheRaces.Library.Models
{
    public class BettingAccount
    {
        public GuyNameEnum BettingAccountHolderName { get; set; }
        public int BettingAmount { get; set; }
        public DogNameEnum BettingDogNameSelected { get; set; }

        
    }
}
