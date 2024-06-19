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
        public async Task<IEnumerable<string>> Get([FromBody] MatrixColecctionDto dto)
        {
            return await _wordFinderService.Resolve(dto);
        }
    }
}
