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
        public void TestThreeMatchOrderWrods()
        {
            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "A", "B", "C", "A" },
                    { "A", "B", "C", "B"},
                    { "D", "E", "F", "C" },
                    { "G", "H", "I", "D" },
                    { "O", "A", "D", "G" }
                }, 
                Words = new List<string>() { "ADG", "DEF", "CGI", "ABC" }
            };

            // GetWords
            var result = _controller.Get(dto);

            // Assert
            Assert.IsInstanceOf<IEnumerable<string>>(result.Result);
            var okResult = result.Result.ToList();
            Assert.IsNotNull(okResult);

            Assert.AreEqual("ABC", okResult[0]);
            Assert.AreEqual("ADG", okResult[1]);
            Assert.AreEqual("DEF", okResult[2]);
        }

        [Test]
        public void TestLenghtMatrixError()
        {
            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" }
                },
                Words = new List<string>() { "ADG", "DEF", "CGI", "ABC" }
            };

            // GetWords
            var result = _controller.Get(dto);


            // Assert
            Assert.AreEqual("Wrong size of the matrix", result.Exception.InnerException.Message);
        }

        [Test]
        public void ChallengeTest()
        {
            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "a", "b", "c", "d", "c" },
                    { "r", "g", "w", "i", "o" },
                    { "c", "h", "i", "l", "l" },
                    { "p", "q", "n", "s", "d" },
                    { "u", "v", "d", "x", "y" }

                },
                Words = new List<string>() { "cold", "wind", "snow", "chill" }
            };

            // GetWords
            var result = _controller.Get(dto);

            // Assert
            Assert.IsInstanceOf<IEnumerable<string>>(result.Result);
            var okResult = result.Result.ToList();
            Assert.IsNotNull(okResult);

            Assert.AreEqual("cold", okResult[0]);
            Assert.AreEqual("wind", okResult[1]);
            Assert.AreEqual("chill", okResult[2]);
        }
    }
}