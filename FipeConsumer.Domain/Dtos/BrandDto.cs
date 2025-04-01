using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public class BrandDto
    {
        [JsonPropertyName("codigo")]
        public required string Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }
    }
}