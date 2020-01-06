
namespace DayAtTheRaces.Common.Extensions
{
    public class ClientValidation
    {
        public static string DistanceSetWarning()
        {
            return "Distance Must Be set";
        }

        public static string StandardNotification()
        {
            return $"Notification";
        }

        public static string CashValidationNoCashWarning()
        {
            return $"Please Define a Cash amount First!";
        }

        public static string CashValidationCashMustBePositiveValueWarning()
        {
            return $"The Cash amount must be a positive value!";
        }

        public static string CashDepositValidationRadioBtnNoGuyNameWarning()
        {
            return $"NO Cash was deposited. You must Select a guy first";
        }

        public static string StandardBettingNotification()
        {
            return $"Notification: bet must be between $5 and $15 bucks.";
        }

        public static string BetValidationLessThan2DogsInRacetrackWarning()
        {
            return $"There Needs to be at least 2 dogs in the racetrack. Once there are 2 dogs, a bet can be placed.";
        }

        public static string BetValidationComboBoxNoGuySelectedWarning()
        {
            return $"NO bet was placed. You must Select a guy first. If there is no Guy for selection please add a Guy";
        }

        public static string BetValidationComboBoxNoDogSelectedWarning()
        {
            return $"NO bet was placed. You must Select a dog first. If there is no dog for selection please add min 2 Dogs";
        }

        public static string BetValidationWrongAmountLessThanMinimumWarning(int minimumBet)
        {
            return $"The bet cannot be placed. The amount is less than {minimumBet} Bucks.";
        }

        public static string BetValidationWrongAmountMoreThanMaximumWarning(int maximumBet)
        {
            return $"The bet cannot be placed. The amount is more than {maximumBet}";
        }
    }
}
