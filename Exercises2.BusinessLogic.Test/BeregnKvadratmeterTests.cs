using Exercise2.BusinessLogic;
using Moq;
using Xunit;

namespace Exercises2.BusinessLogic.Test
{
    public class BeregnKvadratmeterTests
    {
        [Fact]
        public void Kvadratmeter_Sum_Stemmer_Med_Lejemaalsum()
        {
            // Arrange
            var lejemaals = new List<Lejemaal>
            {
                new Lejemaal { Kvadratmeter = 5 },
                new Lejemaal { Kvadratmeter = 20 },
                new Lejemaal { Kvadratmeter = 30 }
            };
            var expected = lejemaals.Sum(l => l.Kvadratmeter);

            // Ny mock på lejemålsrepository
            var mockRepository = new Mock<ILejemaalRepository>();    
            // Setup fortæller HVAD mock data skal. Man henter HentLejemaal igennem interface ILejemaal, som skal returnere alle lejemaal 
            mockRepository.Setup(repo => repo.HentLejemaal()).Returns(lejemaals);
            // setup expected
            var sut = new EjendomBeregnerService(mockRepository.Object); 

            // Act 
            var actual = sut.BeregnKvadratmeter();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
    

    public class LejemaalFileRepositoryTests
    {
        [Theory]
        [InlineData("1, 5, 2", 1, 5, 2)]
        [InlineData("2, 10.8, 3", 2, 10.8, 3)]
        [InlineData("3, 20.5, 4.5", 3, 20.5, 4.5)]
        public void Given_Csv_Lines_Valid__Then_Lejemaal_Is_Returned(string csvLine, int expectedLejlighednummer,
            double expectedKvadratmeter, double expectedRum)
        {
            // Arrange
            var mockFileWrapper = new Mock<IFileWrapper>(); // Mock sættes op
            mockFileWrapper.Setup(fw => fw.ReadAllLines(It.IsAny<string>())).Returns(new[] { csvLine });

            var sut = new LejemaalFileRepository("dummyFilename", mockFileWrapper.Object);

            // Act
            var actual = sut.HentLejemaal().First();

            // Assert
            Assert.Equal(expectedLejlighednummer, actual.Lejlighednummer);
            Assert.Equal(expectedKvadratmeter, actual.Kvadratmeter, 5);
            Assert.Equal(expectedRum, actual.Rum, 5);
        }
    }
}