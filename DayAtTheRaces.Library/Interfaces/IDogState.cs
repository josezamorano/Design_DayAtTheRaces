namespace DayAtTheRaces.Library.Interfaces
{
    public interface IDogState
    {
        double GetsFaster(double dogStandardMtsCoveredIn1Second);
        double GetsTired(double dogStandardMtsCoveredIn1Second);
        double GetsDistracted(double dogStandardMtsCoveredIn1Second, double secondsDelayed);
    }
}
