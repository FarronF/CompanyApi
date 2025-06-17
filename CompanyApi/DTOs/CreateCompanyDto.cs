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
        public string Isin { get; set; }
        public string? Website { get; set; }
    }
}
