using CompanyApi.Data;
using CompanyApi.DTOs;
using CompanyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyContext _context;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(CompanyContext context, ILogger<CompanyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return Ok(companies);
        }

        [HttpGet("id/{id}")]
        public ActionResult<Company> GetCompanyById(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet("/isin/{isin}")]
        public ActionResult<Company> GetCompanyByIsin(string isin)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Isin == isin);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = new Company
            {
                Name = dto.Name,
                Exchange = dto.Exchange,
                Ticker = dto.Ticker,
                Isin = dto.Isin,
                Website = dto.Website
            };

            _context.Companies.Add(company);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { error = "A database error occurred." });
            }

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpPut("/id/{id}")]
        public IActionResult UpdateCompanyById(int id, [FromBody] UpdateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var company = _context.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            company.Name = dto.Name ?? company.Name;
            company.Exchange = dto.Exchange ?? company.Exchange;
            company.Ticker = dto.Ticker ?? company.Ticker;
            company.Isin = dto.Isin ?? company.Isin;
            company.Website = dto.Website ?? company.Website;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { error = "A database error occurred." });
            }

            return NoContent();
        }

        [HttpPut("/isin/{isin}")]
        public IActionResult UpdateCompanyByIsin(string isin, [FromBody] UpdateCompanyDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
