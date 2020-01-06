
using DayAtTheRaces.Library.Enums;
using System.Collections.Generic;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace DayAtTheRaces.WpfApp.Interfaces
{
    public interface IUIClientValidation
    {
        int TrackDistanceValidation(Label labelNotification, ComboBox comboBox);
        int CashValidation(Label labelNotification, IntegerUpDown integerUpDownControl);
        GuyNameEnum MakeDepositGuySelectedValidation(Label labelNotification, List<RadioButton> guysRadioButtons);
        int PlaceBetMinimum2DogsInRacetrackValidation(Label labelNotification, ComboBox comboBox);
        GuyNameEnum PlaceBetNoBettingGuySelectedValidation(Label labelNotification, ComboBox comboBox);
        DogNameEnum PlaceBetNoDogSelectedValidation(Label labelNotification, ComboBox comboBox);
        int PlaceBetMinimumAndMaximumBetValidation(int betAmount, Label labelNotification);
    }
}
