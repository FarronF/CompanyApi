using System.ComponentModel.DataAnnotations;

namespace CompanyApi.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Exchange { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        [RegularExpression(@"^[A-Z]{2}[A-Z0-9]{10}$", ErrorMessage = "ISIN must be 12 characters, start with two uppercase letters, and contain only uppercase letters or numbers.")]
        public string Isin { get; set; }

        public string? Website { get; set; }
    }
}
