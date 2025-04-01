using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FipeConsumer.Domain.Entities
{
    public class Year
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YearId { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }

        public virtual Model? Model { get; set; }

        public static void CopyProperties(Year source, Year target)
        {
            var properties = typeof(Year).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite &&
                    property.Name != nameof(YearId) &&
                    property.Name != nameof(ModelId))
                {
                    property.SetValue(target, property.GetValue(source));
                }
            }
        }
    }
}
