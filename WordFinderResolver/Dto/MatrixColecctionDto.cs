using System.Text.Json.Serialization;

namespace WordFinderResolver.Dto
{
    public class MatrixColecctionDto
    {
        [JsonPropertyName("matrix")]
        public string[][] Matrix { get; set; }

        [JsonPropertyName("words")]
        public IEnumerable<string> Words { get; set; }
    }
}
