using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FipeConsumer.Domain.Exceptions;

namespace FipeConsumer.Domain.Entities
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        private Brand() { }

        public Brand(string code, string name)
        {
            SetCode(code);
            SetName(name);
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new DomainException("Brand code cannot be null or empty.");
            }

            Code = code;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Brand name cannot be null or empty.");
            }
            
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
