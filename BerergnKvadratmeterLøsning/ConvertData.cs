using BerergnKvadratmeterLøsning.Model;

namespace BerergnKvadratmeterLøsning
{
    public class ConvertData
    {
        public List<Lejemaal> ConvertToList(string[] toBeConverted)
        {
            var lejemaalList = new List<Lejemaal>();

            foreach (var lejemaal in toBeConverted)
            {
                var lejemaalParts = lejemaal.Split(',');
                var lejlighedsnummer = RemoveQuotes(lejemaalParts[0]);
                double.TryParse(RemoveQuotes(lejemaalParts[1]), out var lejemaalKvadratmeter);
                int.TryParse(RemoveQuotes(lejemaalParts[2]), out var antalRum);

                var lejemaalObj = new Lejemaal(lejlighedsnummer, lejemaalKvadratmeter, antalRum);
                lejemaalList.Add(lejemaalObj);
            }
            return lejemaalList;
        }

        private string RemoveQuotes(string lejemaalPart)
        {
            return lejemaalPart.Replace('"', ' ').Trim();
        }
    }
}