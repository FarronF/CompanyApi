using System.ComponentModel.DataAnnotations;
using CompanyApi.DTOs;
using Xunit;

namespace CompanyApi.Tests.DTOs
{

    public class CreateCompanyDtoValidationTests
    {
        string validName = "Test Company";
        string validExchange = "Test Exchange";
        string validTicker = "TEST";
        string validIsin = "EG1111111111";

        [Fact]
        public void Isin_WhenValidFormat_ThenPassesValidation()
        {
            var dto = new CreateCompanyDto
            {
                Name = validName,
                Exchange = validExchange,
                Ticker = validTicker,
                Isin = validIsin,
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Isin_WhenShorterThan12_ThenFailsValidation()
        {
            var dto = new CreateCompanyDto
            {
                Name = validName,
                Exchange = validExchange,
                Ticker = validTicker,
                Isin = "EG111111111",
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
        }

        [Fact]
        public void Isin_WhenLongerThan12_ThenFailsValidation()
        {
            var dto = new CreateCompanyDto
            {
                Name = validName,
                Exchange = validExchange,
                Ticker = validTicker,
                Isin = "EG11111111111",
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
        }

        [Fact]
        public void Isin_WhenInvalidFormat_ThenFailsValidation()
        {
            var dto = new CreateCompanyDto
            {
                Name = validName,
                Exchange = validExchange,
                Ticker = validTicker,
                Isin = "111111111111",
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage!.Contains("ISIN must start with two non-numeric characters"));
        }



    }
}