namespace DayAtTheRaces.Library.Notifications
{
    public class AllNotifications
    {
        public static string DistanceSetOk()
        {
            return "Distance Set Successfully";
        }

        public static string NewDogToRacetrackOk(string newDogName)
        {
            return $"the new dog {newDogName} has been added to the racetrack.";
        }

        public static string NewDogToRacetrackWarning()
        {
            return $"NO Dog has been added to the racetrack. \n4 dogs max.";
        }

        public static string GuyAddedToStadiumWarning(string guyName)
        {
            return $"{guyName} has already been Added to the stadium";
        }

        public static string GuyAddedToStadiumOk(string guyName)
        {
            return $"{guyName} Added to the stadium Successfully.";
        }

        //==CASH DEPOSIT
        public static string CashDepositOk(string guyName, int amount, int balance)
        {
            return $"${amount} deposited in {guyName}'s pocket. Cash Balance is now {balance}.";
        }

        public static string NoGuyAddedToTheStadiumWarning()
        {
            return $"Selected guy not found in the Stadium. A guy must be added to the Stadium First!";
        }

        //==BET PLACED
        public static string BetPlacedOk(string betGuyName,int betAmount, string betDogName)
        {
            return $"{ betGuyName} placed a bet of {betAmount} Bucks in Dog {betDogName}";
        }
        
        public static string BetNotEnoughCashWarning(string betGuyName,int betAmount, int cashBalance)
        {
            return $"{betGuyName} cannot place a bet for {betAmount} Bucks. His pocket cash is {cashBalance}.";
        }
    }
}
