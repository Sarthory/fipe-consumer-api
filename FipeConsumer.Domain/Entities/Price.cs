using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FipeConsumer.Domain.Entities
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceId { get; set; }

        public required string Value { get; set; }

        public required string BrandName { get; set; }

        public required string ModelName { get; set; }

        public required int ModelYear { get; set; }

        public required string Fuel { get; set; }

        public required string FipeCode { get; set; }

        public required string ReferenceMonth { get; set; }

        public required string FuelAbbreviation { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public virtual Model? Model { get; set; }

        [ForeignKey("Year")]
        public int YearId { get; set; }
        public virtual Year? Year { get; set; }

        public static void CopyProperties(Price source, Price target)
        {
            var properties = typeof(Price).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite &&
                    property.Name != nameof(PriceId) &&
                    property.Name != nameof(BrandId) &&
                    property.Name != nameof(ModelId) &&
                    property.Name != nameof(YearId))
                {
                    property.SetValue(target, property.GetValue(source));
                }
            }
        }
    }


}