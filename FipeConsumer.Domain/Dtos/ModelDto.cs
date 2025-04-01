using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public class ModelDto
    {
        [JsonPropertyName("codigo")]
        public required int Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }
    }
}