using System.ComponentModel.DataAnnotations;

public class UpdateCompanyDto
{
    public string? Name { get; set; }
    public string? Exchange { get; set; }
    public string? Ticker { get; set; }
    [MinLength(12)]
    [MaxLength(12)]
    [RegularExpression(@"^[A-Z]{2}[A-Z0-9]{10}$", ErrorMessage = "ISIN must be 12 characters, start with two uppercase letters, and contain only uppercase letters or numbers.")]
    public string? Isin { get; set; }
    public string? Website { get; set; }
}