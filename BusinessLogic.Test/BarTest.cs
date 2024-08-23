using BusinessLogic;
using Moq;

namespace BusinessLogic.Test

    
    public class BarTest
    {
        // Fact er for at teste met et datas�t 
        //[Fact]

        // Thery er for flere datas�t
        [Theory]
        [InlineData("Hello", "BarHello")] // Datas�t
        [InlineData("Kode", "BarKode")]   // Datas�t

        public void Given_Foo_is_correct_Then_Bar_correct(string foo, string expected)
        {
            // Arrange
            // Mock ops�ttes - vi laver mock p� IFoo
            var fooMock = new Mock<IFoo>();  // Mock builder
            fooMock.Setup(x => x.GetName()).Returns("foo");

            //var expected = "BarHello";
            
            IBar sut = new Bar(fooMock.Object);  // objektet tr�kkes ud fra mock builder

            // Act
            var actual = sut.GetName();

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}