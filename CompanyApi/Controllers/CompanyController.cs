using CompanyApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("id/{id}")]
        public IEnumerable<Company> GetCompanyById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("/isin/{isin}")]
        public IEnumerable<Company> GetCompanyByIsin()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IEnumerable<Company> CreateCompany()
        {
            throw new NotImplementedException();
        }

        [HttpPut("/id/{id}")]
        public IEnumerable<Company> UpdateCompanyById()
        {
            throw new NotImplementedException();
        }

        [HttpPut("/isin/{isin}")]
        public IEnumerable<Company> UpdateCompanyByIsin()
        {
            throw new NotImplementedException();
        }
    }
}
