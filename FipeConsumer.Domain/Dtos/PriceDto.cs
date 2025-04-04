using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Dtos
{
    public struct PriceDto
    {
        [JsonPropertyName("Valor")]
        public string Value { get; set; }

        [JsonPropertyName("Marca")]
        public string BrandName { get; set; }

        [JsonPropertyName("Modelo")]
        public string ModelName { get; set; }

        [JsonPropertyName("AnoModelo")]
        public int ModelYear { get; set; }

        [JsonPropertyName("Combustivel")]
        public string Fuel { get; set; }

        [JsonPropertyName("CodigoFipe")]
        public string FipeCode { get; set; }

        [JsonPropertyName("MesReferencia")]
        public string ReferenceMonth { get; set; }

        [JsonPropertyName("SiglaCombustivel")]
        public string FuelAbbreviation { get; set; }
    }
}
