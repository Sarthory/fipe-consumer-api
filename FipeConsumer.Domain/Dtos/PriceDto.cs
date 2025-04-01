using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public class PriceDto
    {
        [JsonPropertyName("Valor")]
        public required string Value { get; set; }

        [JsonPropertyName("Marca")]
        public required string BrandName { get; set; }

        [JsonPropertyName("Modelo")]
        public required string ModelName { get; set; }

        [JsonPropertyName("AnoModelo")]
        public required int ModelYear { get; set; }

        [JsonPropertyName("Combustivel")]
        public required string Fuel { get; set; }

        [JsonPropertyName("CodigoFipe")]
        public required string FipeCode { get; set; }

        [JsonPropertyName("MesReferencia")]
        public required string ReferenceMonth { get; set; }

        [JsonPropertyName("SiglaCombustivel")]
        public required string FuelAbbreviation { get; set; }

    }
}