using DayAtTheRaces.Library.Enums;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DayAtTheRaces.Library.Interfaces
{
    public interface IUIMediator
    {
        void TrackDistanceComboBox_SelectionChanged(int selectedItemValue, Label label);
        int BtnAddDog_Click(ComboBox comboBoxDogs, Label labelNotification, List<Image> dogImages);
        void BtnAddGuy_Click(ComboBox comboBoxGuys, Label labelNotification, GuyNameEnum guyName);
        void BtnMakeDeposit_Click(Label labelNotification, GuyNameEnum guyName, int amountDeposit);
        void BtnPlaceBet_Click(Label LblBetNotification, List<Label> allGuysLabels, ListBox listBoxBetsPlaced, GuyNameEnum guyName, DogNameEnum dogName, int betAmount);
        void BtnStartRace_Click(ListBox ListBoxBetsPlaced, List<Label> allGuysLabels, List<Label> winnerDogLabels, List<Label> standardLabels);
    }
}
