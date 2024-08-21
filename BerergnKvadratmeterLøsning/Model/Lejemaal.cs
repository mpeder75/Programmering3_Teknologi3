namespace BerergnKvadratmeterLøsning.Model
{
    public class Lejemaal(string lejlighedsnummer, double lejemaalKvadratmeter, int antalRum)
    {
        public string Lejlighedsnummer { get; set; }
        public double Kvadratmeter { get; set; }
        public int AntalRum { get; set; }
    }
}
