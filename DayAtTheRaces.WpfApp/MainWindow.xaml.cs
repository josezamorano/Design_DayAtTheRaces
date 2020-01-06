using DayAtTheRaces.Common.Enums;
using DayAtTheRaces.Common.Extensions;
using DayAtTheRaces.Library.DependencyInjection;
using DayAtTheRaces.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using System.Threading.Tasks;
using DayAtTheRaces.WpfApp.Validation;
using DayAtTheRaces.WpfApp.Interfaces;
using DayAtTheRaces.Library.Enums;

namespace DayAtTheRaces.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUIMediator objMediator;
        private IUIClientValidation _objValidator;
        public MainWindow()
        {
            InitializeComponent();
            SetImagesInitialPositions();
            SetDependencyInjectionLib();
            objMediator = ContainerConfig.GetInstance<IUIMediator>();
            _objValidator = DayAtTheRaces.WpfApp.DependencyInjection.ContainerConfig.GetInstance<IUIClientValidation>();
        }


        #region Private Methods 
        private void SetDependencyInjectionLib()
        {
            DayAtTheRaces.Library.DependencyInjection.ContainerConfig.Configure();
        }
        private void SetDependencyInjectionUI()
        {
           DayAtTheRaces.WpfApp.DependencyInjection.ContainerConfig.Configure();
        }

        private void SetImagesInitialPositions()
        {
            Canvas.SetLeft(pictureBoxDog1, -10);

            Canvas.SetLeft(pictureBoxDog2, -10);

            Canvas.SetLeft(pictureBoxDog3, -10);

            Canvas.SetLeft(pictureBoxDog4, -10);
        }

        private List<Label> AllGuysLabelsList()
        {
            List<Label> allGuysLabels = new List<Label>() { LblNotificationAl, LblNotificationJoe, LblNotificationBob };
            return allGuysLabels;
        }

        private List<Label> AllWinnerNotificationLabelsList()
        {
            var winningNotificationLabels = new List<Label>() { LblInfoWinnerDogName, LblInfoDistanceRacetrack, LblInfoTotalTime };
            return winningNotificationLabels;
        }

        private List<Label> AllStandardNotificationsLabelsList()
        {
            var standardNotifications = new List<Label>() { LblDeposit , LblAddNewDog , LblNotificationTrackDistance };
            return standardNotifications;
        }

        private List<RadioButton> allGuysRadioButtonsList()
        {
            List<RadioButton> guysRadioButtons = new List<RadioButton>() { RadioBtnAl, RadioBtnBob, RadioBtnJoe };
            return guysRadioButtons;
        }

        private void DisplayStandardBetNotification()
        {
            LblBetNotification.Content = ClientValidation.StandardBettingNotification();
            LblBetNotification.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Standard);
        }
        private void DisplayStandardRaceGoNotification()
        {
            LblRace.Content = ClientValidation.StandardNotification();
            LblRace.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Standard);
        }

        
        #endregion

        #region Private Events
        private void TrackDistanceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayStandardRaceGoNotification();
            DisplayStandardBetNotification();
            var validatedTrackdistance = _objValidator.TrackDistanceValidation(LblNotificationTrackDistance, TrackDistanceComboBox);
            if (validatedTrackdistance>0)
                objMediator.TrackDistanceComboBox_SelectionChanged(validatedTrackdistance, LblNotificationTrackDistance);
        }

        private void BtnAddDog_Click(object sender, RoutedEventArgs e)
        {
            DisplayStandardBetNotification();
           
            List<Image> allDogImages = new List<Image> { pictureBoxDog1, pictureBoxDog2 , pictureBoxDog3 , pictureBoxDog4 };
            int dogPosition = objMediator.BtnAddDog_Click(ComboBoxRacingDogNames, LblAddNewDog, allDogImages);
            List<DogNameEnum> dogsNames = Enum.GetValues(typeof(DogNameEnum)).Cast<DogNameEnum>().ToList();
            DogNameEnum dogName = dogsNames[dogPosition - 1];
            var labelDescription = $"Dog{ dogPosition}-{dogName}";
            if (dogPosition == 1) { LblDog1.Content = labelDescription; }
            if (dogPosition == 2) { LblDog2.Content = labelDescription; }
            if (dogPosition == 3) { LblDog3.Content = labelDescription; }
            if (dogPosition == 4) { LblDog4.Content = labelDescription; }
        }

        private void BtnAddAl_Click(object sender, RoutedEventArgs e)
        {
            DisplayStandardBetNotification();
            objMediator.BtnAddGuy_Click(ComboBoxGuyBetting, LblNotificationAl, GuyNameEnum.Al);
        }

        private void BtnAddJoe_Click(object sender, RoutedEventArgs e)
        {
            DisplayStandardBetNotification();
            objMediator.BtnAddGuy_Click(ComboBoxGuyBetting, LblNotificationJoe, GuyNameEnum.Joe);
        }

        private void BtnAddBob_Click(object sender, RoutedEventArgs e)
        {
            DisplayStandardBetNotification();
            objMediator.BtnAddGuy_Click(ComboBoxGuyBetting, LblNotificationBob, GuyNameEnum.Bob);
        }

        private void BtnMakeDeposit_Click(object sender, RoutedEventArgs e)
        {
            DisplayStandardBetNotification();
            var guyForCashDeposit = _objValidator.MakeDepositGuySelectedValidation(LblDeposit, allGuysRadioButtonsList());
            var cashDeposit = _objValidator.CashValidation(LblDeposit, moneyDepositUpDownControl);
            if(cashDeposit>0 && guyForCashDeposit !=0)
                 objMediator.BtnMakeDeposit_Click(LblDeposit, guyForCashDeposit, cashDeposit);
        }

        private void BtnPlaceBet_Click(object sender, RoutedEventArgs e)
        {
            SetImagesInitialPositions();
            var minimumDogsInRacetrack = _objValidator.PlaceBetMinimum2DogsInRacetrackValidation(LblBetNotification, ComboBoxRacingDogNames);
            var guyNameSelected = _objValidator.PlaceBetNoBettingGuySelectedValidation(LblBetNotification, ComboBoxGuyBetting);
            var dogNameSelected = _objValidator.PlaceBetNoDogSelectedValidation(LblBetNotification, ComboBoxRacingDogNames);
            var bettingCash = _objValidator.CashValidation(LblBetNotification, cashBetUpDownControl);
            var betAmount = _objValidator.PlaceBetMinimumAndMaximumBetValidation(bettingCash,LblBetNotification);
            if (minimumDogsInRacetrack>=2 && guyNameSelected > 0 && dogNameSelected>0 && betAmount> 0)
            {
                objMediator.BtnPlaceBet_Click(LblBetNotification,AllGuysLabelsList(), ListBoxBetsPlaced, guyNameSelected, dogNameSelected, betAmount);
            }
        }

       
        private void BtnStartRace_Click(object sender, RoutedEventArgs e)
        {
            var validatedTrackdistance = _objValidator.TrackDistanceValidation(LblNotificationTrackDistance, TrackDistanceComboBox);
            if (validatedTrackdistance > 0)
            {
                Dispatcher.Invoke(() => {
                    objMediator.BtnStartRace_Click(ListBoxBetsPlaced, AllGuysLabelsList(), AllWinnerNotificationLabelsList(), AllStandardNotificationsLabelsList());
                });
            }
            else
            {
                LblRace.Content = ClientValidation.DistanceSetWarning();
                LblRace.Foreground = NotificationColors.GetBrushColor(NotificationEnum.Warning);
            }
        }
        #endregion
        
    }
}
