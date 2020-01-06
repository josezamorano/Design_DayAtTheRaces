using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Common.Extensions;
using DayAtTheRaces.Library.Enums;
using DayAtTheRaces.WpfApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace DayAtTheRaces.WpfApp.Validation
{
    public class UIClientValidation : IUIClientValidation
    {
        public int TrackDistanceValidation(Label labelNotification, ComboBox comboBox)
        {
            int defaultValue = -1;
            object item = comboBox.SelectedItem;
            if (item != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)item;
                bool parseSuccess = Int32.TryParse(comboBoxItem.Content.ToString(), out defaultValue);
                if (!parseSuccess)
                {
                    labelNotification.Content = ClientValidation.DistanceSetWarning();
                    labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                }
            }
            else
            {
                labelNotification.Content = ClientValidation.DistanceSetWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
            return defaultValue;
        }

        public int CashValidation(Label labelNotification, IntegerUpDown integerUpDownControl)
        {
            int fail = -1;
            int? cash = integerUpDownControl.Value;
            if (cash == null)
            {
                labelNotification.Content = ClientValidation.CashValidationNoCashWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                return fail;
            }
            if (cash < 0)
            {
                labelNotification.Content = ClientValidation.CashValidationCashMustBePositiveValueWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                return fail;
            }
            return (int)cash;
        }

        public GuyNameEnum MakeDepositGuySelectedValidation(Label labelNotification, List<RadioButton> guysRadioButtons)
        {
            RadioButton selectedRadioBtnGuy = guysRadioButtons.Where(a => a.IsChecked == true).FirstOrDefault();
            if (selectedRadioBtnGuy == null)
            {
                labelNotification.Content = ClientValidation.CashDepositValidationRadioBtnNoGuyNameWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
            else
            {
                var selectedGuy = selectedRadioBtnGuy.Content.ToString();
                var allGuys = Enum.GetValues(typeof(GuyNameEnum)).Cast<GuyNameEnum>().ToList();
                var guyForCashDeposit = allGuys.Where(name => name.ToString() == selectedGuy).FirstOrDefault();
                return guyForCashDeposit;
            }
            return 0;
        }

        public int PlaceBetMinimum2DogsInRacetrackValidation(Label labelNotification, ComboBox comboBox)
        {
            var dogsInRacetrackCount = comboBox.Items.Count;
            if (dogsInRacetrackCount < 2)
            {
                labelNotification.Content = ClientValidation.BetValidationLessThan2DogsInRacetrackWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
            return dogsInRacetrackCount;
        }

        public GuyNameEnum PlaceBetNoBettingGuySelectedValidation(Label labelNotification, ComboBox comboBox)
        {
            object selectedItem = comboBox.SelectedItem;
            if (selectedItem != null)
            {
                var guyBetting = (GuyNameEnum)selectedItem;
                if (guyBetting == 0)
                {
                    labelNotification.Content = ClientValidation.BetValidationComboBoxNoGuySelectedWarning();
                    labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                }
                else
                {
                    var allGuys = Enum.GetValues(typeof(GuyNameEnum)).Cast<GuyNameEnum>().ToList();
                    var selectedGuyName = allGuys.Where(a => a.ToString() == guyBetting.ToString()).FirstOrDefault();
                    return selectedGuyName;
                }
            }
            else
            {
                labelNotification.Content = ClientValidation.BetValidationComboBoxNoGuySelectedWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
            return 0;
        }

        public DogNameEnum PlaceBetNoDogSelectedValidation(Label labelNotification, ComboBox comboBox)
        {
            object selectedItem = comboBox.SelectedItem;
            if (selectedItem != null)
            {
                DogNameEnum dogSelected = (DogNameEnum)selectedItem;
                if (dogSelected == 0)
                {
                    labelNotification.Content = ClientValidation.BetValidationComboBoxNoDogSelectedWarning();
                    labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                }
                else
                {
                    var allDogs = Enum.GetValues(typeof(DogNameEnum)).Cast<DogNameEnum>().ToList();
                    var selectedDogName = allDogs.Where(a => a.ToString() == dogSelected.ToString()).FirstOrDefault();
                    return selectedDogName;
                }
            }
            else
            {
                labelNotification.Content = ClientValidation.BetValidationComboBoxNoDogSelectedWarning();
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
            return 0;
        }

        public int PlaceBetMinimumAndMaximumBetValidation(int betAmount, Label labelNotification)
        {
            int fail = -1, minimumBet = 5, maximumBet = 15;
            if (betAmount < minimumBet)
            {
                labelNotification.Content = ClientValidation.BetValidationWrongAmountLessThanMinimumWarning(minimumBet);
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                return fail;
            }

            else if (betAmount > maximumBet)
            {
                labelNotification.Content = ClientValidation.BetValidationWrongAmountMoreThanMaximumWarning(maximumBet);
                labelNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
                return fail;
            }
            return betAmount;
        }
    }
}
