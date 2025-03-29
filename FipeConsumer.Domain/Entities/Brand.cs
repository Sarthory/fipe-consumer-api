using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Entities
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        [JsonPropertyName("codigo")]
        public required string Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }
    }

    public class BrandComparer : IEqualityComparer<Brand>
    {
        public bool Equals(Brand? x, Brand? y)
        {
            if (x == null || y == null)
                return false;

            return x.Code == y.Code;
        }

        public int GetHashCode(Brand obj)
        {
            return obj.Code.GetHashCode();
        }
    }
}
