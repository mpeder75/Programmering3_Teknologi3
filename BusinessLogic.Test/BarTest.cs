using BusinessLogic;
using Moq;

namespace BusinessLogic.Test

    
    public class BarTest
    {
        // Fact er for at teste met et datasæt 
        //[Fact]

        // Thery er for flere datasæt
        [Theory]
        [InlineData("Hello", "BarHello")] // Datasæt
        [InlineData("Kode", "BarKode")]   // Datasæt

        public void Given_Foo_is_correct_Then_Bar_correct(string foo, string expected)
        {
            // Arrange
            // Mock opsættes - vi laver mock på IFoo
            var fooMock = new Mock<IFoo>();  // Mock builder
            fooMock.Setup(x => x.GetName()).Returns("foo");

            //var expected = "BarHello";
            
            IBar sut = new Bar(fooMock.Object);  // objektet trækkes ud fra mock builder

            // Act
            var actual = sut.GetName();

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}