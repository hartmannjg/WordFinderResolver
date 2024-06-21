using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WordFinderResolver.Controllers;
using WordFinderResolver.Dto;
using WordFinderResolver.Service;
using WordFinderResolver.Service.Validations.Chains;
using WordFinderResolver.Service.Validations.Rules;

namespace WordFinderResolverTest
{
    public class Tests
    {
        private WordFinderController _controller;
        private Mock<ILogger<WordFinderController>> _loggerMock;
        private Mock<WordFinderService> _wordFinderServiceMock;
        private Mock<WordFinderFactory> _wordFinderFactoryMock;
        private Mock<MatrixValidationsChains> _matrixValidationsChainsMock;
        private Mock<MatrixLengthValidation> _matrixLengthValidationMock;
        private Mock<MatrixSquareValidation> _matrixSquareValidationMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WordFinderController>>();
            _wordFinderFactoryMock = new Mock<WordFinderFactory>();
            _matrixLengthValidationMock = new Mock<MatrixLengthValidation>();
            _matrixLengthValidationMock.CallBase = true;
            _matrixSquareValidationMock = new Mock<MatrixSquareValidation>();
            _matrixSquareValidationMock.CallBase = true;
            _matrixValidationsChainsMock = new Mock<MatrixValidationsChains>(_matrixLengthValidationMock.Object, _matrixSquareValidationMock.Object);
            _wordFinderServiceMock = new Mock<WordFinderService>(_wordFinderFactoryMock.Object, _matrixValidationsChainsMock.Object);
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
                Matrix = new string[][]
                {
                    new string[] { "A", "B", "C", "A" },
                    new string[] { "A", "B", "C", "B"},
                    new string[] { "D", "E", "F", "C" },
                    new string[] { "G", "A", "B", "C" }
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
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, $@"..\..\..\JsonMock\MatrixSize68Dto.json");
            var jsonString = File.ReadAllText(relativePath);

            var dto = JsonConvert.DeserializeObject<MatrixColecctionDto>(jsonString);
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
        public void TestSquaretMatrixError()
        {
            // Arrange
            var exceptionMessage = "{ Message = An error occurred while processing your request., Details = The matrix is not a square matrix }";

            // Preparing request
            var dto = new MatrixColecctionDto()
            {
                Matrix = new string[][]
                {
                    new string[] { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    new string[] { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    new string[] { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" },
                    new string[] { "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A", "A", "B", "C", "A" }
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
                Matrix = new string[][]
                {
                    new string[]  { "a", "b", "c", "d", "c" },
                    new string[] { "r", "g", "w", "i", "o" },
                    new string[] { "c", "h", "i", "l", "l" },
                    new string[]  { "p", "q", "n", "s", "d" },
                    new string[]  { "u", "v", "d", "x", "y" }

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
                Matrix = new string[][]
                {
                    new string[] { "a", "b", "c", "d", "c", "a", "b", "c", "o", "l", "d" },
                    new string[] { "r", "g", "w", "i", "o", "c", "h", "i", "l", "l", "d" },
                    new string[] { "c", "h", "i", "l", "l",  "a", "b", "w", "i", "n", "d"},
                    new string[] { "p", "q", "n", "s", "d" , "w", "i", "n", "d", "d", "d"},
                    new string[] { "u", "v", "d", "x", "y", "c", "h", "x", "l", "l", "d" },
                    new string[] { "a", "b", "c", "d", "c", "c", "h", "d", "l", "l", "d" },
                    new string[] { "r", "g", "w", "i", "o", "c", "h", "i", "l", "l", "d" },
                    new string[] { "c", "h", "i", "l", "l", "d", "c", "h", "i", "l", "l" },
                    new string[] { "p", "q", "n", "s", "d", "c", "o", "l", "d", "d", "g" },
                    new string[] { "u", "v", "d", "x", "y", "c", "h", "i", "l", "l", "g" },
                    new string[] { "u", "v", "d", "x", "y", "c", "h", "i", "l", "l", "g" }

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