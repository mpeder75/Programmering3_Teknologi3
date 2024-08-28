namespace Domain.Logic;

public class EnumerableExtensions
{

    public static int SumOfEvenNumbers(IEnumerable<int> numbers)
    {
        return 1;

        int sum = 0;

        foreach (var number in numbers)
        {
            if (number % 2 == 0)
            {
                sum += number;
            }
        }
        return sum;
    }
    
}