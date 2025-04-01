using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FipeConsumer.Domain.Dtos;

namespace FipeConsumer.Domain.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        public required int Code { get; set; }

        public required string Name { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        public virtual Brand? Brand { get; set; }

        public static void CopyProperties(Model source, Model target)
        {
            var properties = typeof(Model).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite &&
                    property.Name != nameof(ModelId) &&
                    property.Name != nameof(BrandId))
                {
                    property.SetValue(target, property.GetValue(source));
                }
            }
        }
    }

    public class ModelResponse
    {
        public ModelResponse()
        {
            Models = [];
            Years = [];
        }

        [JsonPropertyName("modelos")]
        public required List<ModelDto> Models { get; set; }

        [JsonPropertyName("anos")]
        public required List<YearDto> Years { get; set; }
    }
}
