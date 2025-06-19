using CompanyApi.Data;
using CompanyApi.DTOs;
using CompanyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyContext _context;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(CompanyContext context, ILogger<CompanyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>
        /// A list of all <see cref="Company"/> entities.
        /// </returns>
        /// <response code="200">Returns the list of companies.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return Ok(companies);
        }

        /// <summary>
        /// Retrieves a company by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the company.</param>
        /// <returns>
        /// The <see cref="Company"/> with the specified ID, or 404 if not found.
        /// </returns>
        /// <response code="200">Returns the company with the specified ID.</response>
        /// <response code="404">If the company is not found.</response>
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

        /// <summary>
        /// Retrieves a company by its ISIN code.
        /// </summary>
        /// <param name="isin">The ISIN code of the company.</param>
        /// <returns>
        /// The <see cref="Company"/> with the specified ISIN, or 404 if not found.
        /// </returns>
        /// <response code="200">Returns the company with the specified ISIN.</response>
        /// <response code="404">If the company is not found.</response>
        [HttpGet("isin/{isin}")]
        public ActionResult<Company> GetCompanyByIsin(string isin)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Isin == isin);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        /// <summary>
        /// Creates a new company.
        /// </summary>
        /// <param name="dto">The company data to create.</param>
        /// <returns>
        /// Returns <see cref="Company"/> with HTTP 201 status code if creation is successful.
        /// Returns HTTP 400 with validation errors if the input is invalid.
        /// </returns>
        /// <response code="201">The company was created successfully.</response>
        /// <response code="400">The request was invalid or a database error occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Company), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<Company> CreateCompany([FromBody] CreateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_context.Companies.Any(c => c.Name == dto.Name))
            {
                return BadRequest(new { Name = "A company with this Name already exists." });
            }
            if(_context.Companies.Any(c => c.Isin == dto.Isin))
            {
                return BadRequest(new { Isin = "A company with this ISIN already exists." });
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
            catch (DbUpdateException)
            {
                return BadRequest(new { error = "A database error occurred." });
            }

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        /// <summary>
        /// Updates an existing company by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the company to update.</param>
        /// <param name="dto">The updated company data.</param>
        /// <returns>
        /// Returns HTTP 204 if the update is successful.
        /// Returns HTTP 400 if the input is invalid.
        /// Returns HTTP 404 if the company is not found.
        /// </returns>
        /// <response code="204">The company was updated successfully.</response>
        /// <response code="400">The request was invalid or a database error occurred.</response>
        /// <response code="404">If the company is not found.</response>
        [HttpPut("id/{id}")]
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

            if (!string.IsNullOrEmpty(dto.Name) && _context.Companies.Any(c => c.Id != id && c.Name == dto.Name))
            {
                return BadRequest(new { Name = "A company with this Name already exists." });                
            }
            if (!string.IsNullOrEmpty(dto.Isin) && _context.Companies.Any(c => c.Id != id && c.Isin == dto.Isin))
            {
                return BadRequest(new { Isin = "A company with this ISIN already exists." });              
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
    }
}
