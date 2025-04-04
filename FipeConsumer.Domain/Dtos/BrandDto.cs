using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public struct BrandDto
    {
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }
    }
}
