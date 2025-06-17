using System.ComponentModel.DataAnnotations;

namespace CompanyApi.DTOs
{
    public class CreateCompanyDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Exchange { get; set; }
        
        [Required]
        public string Ticker { get; set; }

        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        [RegularExpression(@"^[^\d]{2}.*$", ErrorMessage = "ISIN must start with two non-numeric characters.")]
        public string Isin { get; set; }
        
        public string? Website { get; set; }
    }
}
