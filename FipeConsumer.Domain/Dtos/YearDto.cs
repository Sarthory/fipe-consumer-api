using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public class YearDto
    {
        [JsonPropertyName("codigo")]
        public required string Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }
    }
}