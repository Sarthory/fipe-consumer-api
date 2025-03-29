using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Entities
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceId { get; set; }

        [JsonPropertyName("Valor")]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName("Marca")]
        public string BrandName { get; set; } = string.Empty;

        [JsonPropertyName("Modelo")]
        public string ModelName { get; set; } = string.Empty;

        [JsonPropertyName("AnoModelo")]
        public int ModelYear { get; set; }

        [JsonPropertyName("Combustivel")]
        public string Fuel { get; set; } = string.Empty;

        [JsonPropertyName("CodigoFipe")]
        public string FipeCode { get; set; } = string.Empty;

        [JsonPropertyName("MesReferencia")]
        public string ReferenceMonth { get; set; } = string.Empty;

        [JsonPropertyName("SiglaCombustivel")]
        public string FuelAbbreviation { get; set; } = string.Empty;

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public virtual Model? Model { get; set; }

        [ForeignKey("Year")]
        public int YearId { get; set; }
        public virtual Year? Year { get; set; }
    }
}