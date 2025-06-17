using CompanyApi.Data;
using CompanyApi.DTOs;
using CompanyApi.Models;
using Microsoft.AspNetCore.Mvc;

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
            throw new NotImplementedException();
        }

        [HttpGet("/isin/{isin}")]
        public ActionResult<Company> GetCompanyByIsin(string isin)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CreateCompanyDto dto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("/id/{id}")]
        public IActionResult UpdateCompanyById(int id, [FromBody] UpdateCompanyDto dto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("/isin/{isin}")]
        public IActionResult UpdateCompanyByIsin(string isin, [FromBody] UpdateCompanyDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
