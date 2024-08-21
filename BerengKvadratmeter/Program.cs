
double BeregnKvadratmeter(string lejemaalDataFilename)
{
    var lejemaalene = File.ReadAllLines(lejemaalDataFilename);
    var kvadratmeter = 0.0;
    foreach (var lejemaal in lejemaalene)
    {
        var lejemaalParts = lejemaal.Split(',');
        double.TryParse(RemoveQuotes(lejemaalParts[1]), out var lejemaalKvadratmeter);
        kvadratmeter += lejemaalKvadratmeter;
    }
    return kvadratmeter;
}

string RemoveQuotes(string lejemaalPart)
{
    return lejemaalPart.Replace('"', ' ').Trim();
}