using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        [JsonPropertyName("codigo")]
        public required int Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
    }

    public class ModelComparer : IEqualityComparer<Model>
    {
        public bool Equals(Model? x, Model? y)
        {
            if (x == null || y == null)
                return false;

            return x.Code == y.Code;
        }

        public int GetHashCode(Model obj)
        {
            return obj.Code.GetHashCode();
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
        public required List<Model> Models { get; set; }

        [JsonPropertyName("anos")]
        public required List<Year> Years { get; set; }
    }
}
