using Xunit.Sdk;
using Domain.Logic;

namespace Domain.Test
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Test1()
        {

            // Arrange - Skaber en tom Enumerable af datatypen int
            var input = Enumerable.Empty<int>();

            // Act
            var result = EnumerableExtensions.SumOfEvenNumbers(input);

            // Assert - vi forventer 0
            Assert.Equal(0, result);
        }
    }
}
