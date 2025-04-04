using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public struct ModelDto
    {
        [JsonPropertyName("codigo")]
        public int Code { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }
    }}
