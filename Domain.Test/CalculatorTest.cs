using Xunit.Sdk;
using Domain.Logic;

namespace Domain.Test
{
    public class CalculatorTest
    {
        [Fact]
        public void Given_TwoNumbers_When_Summed_Then_ReturnsCorrectResult()
        {
            var calculator = new Calculator();

            if (calculator.Sum(2, 2) != 4)
            {
                throw new Exception();
            }
        }
    }
}