using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FipeConsumer.Domain.Entities
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public static void CopyProperties(Brand source, Brand target)
        {
            var properties = typeof(Brand).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite && property.Name != nameof(BrandId))
                {
                    property.SetValue(target, property.GetValue(source));
                }
            }
        }
    }
}
