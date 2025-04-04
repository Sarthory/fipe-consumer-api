using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FipeConsumer.Domain.Exceptions;

namespace FipeConsumer.Domain.Entities
{
    public class Price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceId { get; private set; }

        public string Value { get; private set; } = string.Empty;

        public string BrandName { get; private set; } = string.Empty;
        
        public string ModelName { get; private set; } = string.Empty;
        
        public int ModelYear { get; private set; } = 0;
    
        public string Fuel { get; private set; } = string.Empty;
        
        public string FipeCode { get; private set; } = string.Empty;

        public string ReferenceMonth { get; private set; } = string.Empty;
        
        public string FuelAbbreviation { get; private set; } = string.Empty;
        
        [ForeignKey("Brand")]
        public int? BrandId { get; private set; }
        public virtual Brand? Brand { get; private set; }

        [ForeignKey("Model")]
        public int? ModelId { get; private set; }
        public virtual Model? Model { get; private set; }

        [ForeignKey("Year")]
        public int? YearId { get; private set; }
        public virtual Year? Year { get; private set; }

        private Price() { }

        public Price(
            string value,
            string brandName,
            string modelName,
            int modelYear,
            string fuel,
            string fipeCode,
            string referenceMonth,
            string fuelAbbreviation)
        {
            SetValue(value);
            SetBrandName(brandName);
            SetModelName(modelName);
            SetModelYear(modelYear);
            SetFuel(fuel);
            SetFipeCode(fipeCode);
            SetReferenceMonth(referenceMonth);
            SetFuelAbbreviation(fuelAbbreviation);            
        }

        public void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Price value cannot be null or empty.");
            }
            Value = value;
        }

        public void SetBrandName(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
            {
                throw new DomainException("Brand name cannot be null or empty.");
            }
            BrandName = brandName;
        }

        public void SetModelName(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                throw new DomainException("Model name cannot be null or empty.");
            }
            ModelName = modelName;
        }

        public void SetModelYear(int modelYear)
        {
            if (modelYear <= 0)
            {
                throw new DomainException("Model year must be greater than zero.");
            }
            ModelYear = modelYear;
        }

        public void SetFuel(string fuel)
        {
            if (string.IsNullOrWhiteSpace(fuel))
            {
                throw new DomainException("Fuel cannot be null or empty.");
            }
            Fuel = fuel;
        }

        public void SetFipeCode(string fipeCode)
        {
            if (string.IsNullOrWhiteSpace(fipeCode))
            {
                throw new DomainException("Fipe code cannot be null or empty.");
            }
            FipeCode = fipeCode;
        }

        public void SetReferenceMonth(string referenceMonth)
        {
            if (string.IsNullOrWhiteSpace(referenceMonth))
            {
                throw new DomainException("Reference month cannot be null or empty.");
            }
            ReferenceMonth = referenceMonth;
        }

        public void SetFuelAbbreviation(string fuelAbbreviation)
        {
            if (string.IsNullOrWhiteSpace(fuelAbbreviation))
            {
                throw new DomainException("Fuel abbreviation cannot be null or empty.");
            }
            FuelAbbreviation = fuelAbbreviation;
        }

        public void SetBrandId(int brandId)
        {
            if (brandId <= 0)
            {
                throw new DomainException("BrandId must be greater than zero.");
            }
            BrandId = brandId;
        }

        public void SetModelId(int modelId)
        {
            if (modelId <= 0)
            {
                throw new DomainException("ModelId must be greater than zero.");
            }
            ModelId = modelId;
        }

        public void SetYearId(int yearId)
        {
            if (yearId <= 0)
            {
                throw new DomainException("YearId must be greater than zero.");
            }
            YearId = yearId;
        }
    }
}
