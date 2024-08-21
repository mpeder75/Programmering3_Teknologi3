namespace BerergnKvadratmeterLøsning
{
    public class Reader
    {
        public string[] ReadFromFile(string filePath)
        {
            string[] lejemaalene = File.ReadAllLines(filePath);
            return lejemaalene;
        }
    }
}
