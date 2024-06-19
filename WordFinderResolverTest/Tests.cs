using Microsoft.Extensions.Logging;
using Moq;
using WordFinderResolver.Controllers;
using WordFinderResolver.Dto;
using WordFinderResolver.Service;

namespace WordFinderResolverTest
{
    public class Tests
    {
        private WordFinderController _controller;
        private Mock<ILogger<WordFinderController>> _loggerMock;
        private Mock<WordFinderService> _wordFinderServiceMock;
        private Mock<WordFinderFactory> _wordFinderFactoryMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WordFinderController>>();
            _wordFinderFactoryMock = new Mock<WordFinderFactory>();
            _wordFinderServiceMock = new Mock<WordFinderService>(_wordFinderFactoryMock.Object);
            _controller = new WordFinderController(_loggerMock.Object, _wordFinderServiceMock.Object);
        }

        [Test]
        public void TestTwoWordsOk()
        {
            // Arrange
            var expectedResult = new string[] { "word1", "word2", "word3" };
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "A", "B", "C" },
                    { "D", "E", "F" },
                    { "G", "H", "I" }
                }, 
                Words = new List<string>() { "ADG", "DEF", "CGI" }
            };

            // Act
            var result = _controller.Get(dto);

            // Assert
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            var okResult = result as IEnumerable<string>;
            Assert.IsNotNull(okResult);
            //Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}