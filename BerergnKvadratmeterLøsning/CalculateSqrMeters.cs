using BerergnKvadratmeterLøsning.Model;

namespace BerergnKvadratmeterLøsning
{
    public class CalculateSqrMeters
    {
        public double CalculateTotalSqrMeters(List<Lejemaal> lejemaalList)
        {
            double totalSquareMeters = 0.0;

            foreach (var lejemaal in lejemaalList)
            {
                totalSquareMeters += lejemaal.Kvadratmeter;
            }
            return totalSquareMeters;
        }
    }
}
