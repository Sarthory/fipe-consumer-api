using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FipeConsumer.Domain.Exceptions;

namespace FipeConsumer.Domain.Entities
{
    public class Year
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YearId { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        [ForeignKey("Model")]
        public int? ModelId { get; private set; }

        public virtual Model? Model { get; private set; }

        private Year() { } 

        public Year(string code, string name)
        {
            SetCode(code);
            SetName(name);
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new DomainException("Year code cannot be null or empty.");
            }

            Code = code;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Year name cannot be null or empty.");
            }

            Name = name;
        }

        public void SetModelId(int modelId)
        {
            if (modelId <= 0)
            {
                throw new DomainException("ModelId must be greater than zero.");
            }
            ModelId = modelId;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
