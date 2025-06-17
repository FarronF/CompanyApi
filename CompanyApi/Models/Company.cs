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
        [RegularExpression(@"^[^\d]{2}.{10}$", ErrorMessage = "ISIN must be exactly 12 characters and start with two non-numeric characters.")]
        public string Isin { get; set; }

        public string? Website { get; set; }
    }
}
