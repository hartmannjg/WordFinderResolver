using Microsoft.AspNetCore.Mvc;
using WordFinderResolver.Dto;
using WordFinderResolver.Service;

namespace WordFinderResolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly WordFinderService _wordFinderService;
        private readonly ILogger<WordFinderController> _logger;

        public WordFinderController(ILogger<WordFinderController> logger, WordFinderService wordFinderService)
        {
            _logger = logger;
            _wordFinderService = wordFinderService;
        }

        [HttpGet(Name = "GetResult")]
        public async Task<ActionResult<IEnumerable<string>>> Get([FromBody] MatrixColecctionDto dto)
        {
            try
            {
                var result = await _wordFinderService.Resolve(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resolving the matrix collection");
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }

        }
    }
}
