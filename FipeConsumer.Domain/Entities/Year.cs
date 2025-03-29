using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FipeConsumer.Domain.Entities
{
    public class Year
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YearId { get; set; }

        [JsonPropertyName("codigo")]
        public required string Code { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }

        public virtual Model? Model { get; set; }
    }

    public class YearComparer : IEqualityComparer<Year>
    {
        public bool Equals(Year? x, Year? y)
        {
            if (x == null || y == null)
                return false;

            return x.Code == y.Code;
        }

        public int GetHashCode(Year obj)
        {
            return obj.Code.GetHashCode();
        }
    }
}
