using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FipeConsumer.Domain.Dtos;
using FipeConsumer.Domain.Exceptions;

namespace FipeConsumer.Domain.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; private set; }

        public int Code { get; private set; } = 0;

        public string Name { get; private set; } = String.Empty;

        [ForeignKey("Brand")]
        public int? BrandId { get; private set; } = null;

        public virtual Brand? Brand { get; private set; } = null;

        public Model() { }

        public Model(int code, string name)
        {
            SetCode(code);
            SetName(name);            
        }

        public void SetCode(int code)
        {
            if (code <= 0)
            {
                throw new DomainException("Model code must be greater than zero.");
            }

            Code = code;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Model name cannot be null or empty.");
            }

            Name = name;
        }

        public void SetBrandId(int brandId)
        {
            if (brandId <= 0)
            {
                throw new DomainException("BrandId must be greater than zero.");
            }
            BrandId = brandId;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }

    public readonly struct ModelResponse(List<ModelDto> models, List<YearDto> years)
    {
        [JsonPropertyName("modelos")]
        public required List<ModelDto> Models { get; init; } = models;

        [JsonPropertyName("anos")]
        public required List<YearDto> Years { get; init; } = years;
    }
}
