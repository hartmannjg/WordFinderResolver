using Microsoft.AspNetCore.Mvc;
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
            // Arrange
            var expectedData = new List<string> { "ABC", "ADG", "DEF" };

            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "A", "B", "C", "A" },
                    { "A", "B", "C", "B"},
                    { "D", "E", "F", "C" },
                    { "G", "A", "B", "C" },
                    { "O", "A", "D", "G" }
                }, 
                Words = new List<string>() { "ADG", "DEF", "CGI", "ABC" }
            };

            // GetWords
            var result = _controller.Find(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result.Result);
            var okResult = result.Result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var actualData = okResult.Value as IEnumerable<string>;
            Assert.That(actualData, Is.Not.Null);

            Assert.That(actualData, Has.Exactly(expectedData.Count).Items);

            Assert.That(actualData, Is.EqualTo(expectedData).AsCollection);
        }

        [Test]
        public void TestLenghtMatrixError()
        {
            // Arrange
            var exceptionMessage = "{ Message = An error occurred while processing your request., Details = Wrong size of the matrix }";

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
            var result = _controller.Find(dto);

            // Assert
            Assert.That(result.Result.Result, Is.InstanceOf<ObjectResult>());
            var objectResult = result.Result.Result as ObjectResult;
            Assert.That(objectResult, Is.Not.Null);
            Assert.That(objectResult.StatusCode, Is.EqualTo(500));

            var errorResponse = objectResult.Value;
            Assert.That(errorResponse?.ToString(), Is.EqualTo(exceptionMessage));
        }

        [Test]
        public void ChallengeTest()
        {
            // Arrange
            var expectedData = new List<string> { "cold", "wind", "chill" };

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
            var result = _controller.Find(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result.Result);
            var okResult = result.Result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var actualData = okResult.Value as IEnumerable<string>;
            Assert.That(actualData, Is.Not.Null);

            Assert.That(actualData, Has.Exactly(expectedData.Count).Items);

            Assert.That(actualData, Is.EqualTo(expectedData).AsCollection);
        }

        [Test]
        public void ChallengeTestRandomRepetitions()
        {
            // Arrange
            var expectedData = new List<string> { "chill", "cold", "wind" };

            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[,]
                {
                    { "a", "b", "c", "d", "c", "a", "b", "c", "o", "l", "d" },
                    { "r", "g", "w", "i", "o", "c", "h", "i", "l", "l", "d" },
                    { "c", "h", "i", "l", "l",  "a", "b", "w", "i", "n", "d"},
                    { "p", "q", "n", "s", "d" , "w", "i", "n", "d", "d", "d"},
                    { "u", "v", "d", "x", "y", "c", "h", "x", "l", "l", "d" },
                    { "a", "b", "c", "d", "c", "c", "h", "d", "l", "l", "d" },
                    { "r", "g", "w", "i", "o", "c", "h", "i", "l", "l", "d" },
                    { "c", "h", "i", "l", "l", "d", "c", "h", "i", "l", "l" },
                    { "p", "q", "n", "s", "d", "c", "o", "l", "d", "d", "g" },
                    { "u", "v", "d", "x", "y", "c", "h", "i", "l", "l", "g" }

                },
                Words = new List<string>() { "cold", "wind", "snow", "chill" }
            };

            // GetWords
            var result = _controller.Find(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result.Result);
            var okResult = result.Result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var actualData = okResult.Value as IEnumerable<string>;
            Assert.That(actualData, Is.Not.Null);

            Assert.That(actualData, Has.Exactly(expectedData.Count).Items);

            Assert.That(actualData, Is.EqualTo(expectedData).AsCollection);

        }
    }
}