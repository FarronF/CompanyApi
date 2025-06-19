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

        [Theory]
        [InlineData("EG1111111111", true)]   // valid
        [InlineData("EGEGEGEGEGEG", true)]   // valid
        [InlineData("EG111111111", false)]   // too short
        [InlineData("EG11111111111", false)] // too long
        [InlineData("111111111111", false)]  // invalid format
        [InlineData("E11111111111", false)]  // invalid format
        [InlineData("1G1111111111", false)]  // invalid format
        [InlineData("eg1111111111", false)]  // lowercase, invalid
        [InlineData("EG11!1111111", false)]  // special char, invalid
        public void Isin_Validation_WorksAsExpected(string isin, bool expectedValid)
        {
            var dto = new CreateCompanyDto
            {
                Name = validName,
                Exchange = validExchange,
                Ticker = validTicker,
                Isin = isin,
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.Equal(expectedValid, isValid);
        }
    }
}