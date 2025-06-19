using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CompanyApi.Controllers;
using CompanyApi.Data;
using CompanyApi.DTOs;
using CompanyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CompanyApi.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private readonly CompanyController _controller;
        private readonly CompanyContext _context;
        private readonly Mock<ILogger<CompanyController>> _mockLogger;

        // Define test companies at class scope
        private Company _companyA;
        private Company _companyB;

        public CompanyControllerTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new CompanyContext(options);
            _mockLogger = new Mock<ILogger<CompanyController>>();
            _controller = new CompanyController(_context, _mockLogger.Object);

            SeedTestCompanies();
        }

        private void SeedTestCompanies()
        {
            _companyA = new Company
            {
                Name = "Test Company A",
                Exchange = "Exchange 1",
                Ticker = "TEST1",
                Isin = "EG0000000001",
                Website = "https://a.example.com"
            };
            _companyB = new Company
            {
                Name = "Test Company B",
                Exchange = "Exchange 2",
                Ticker = "TEST2",
                Isin = "EG0000000002"
            };

            _context.Companies.AddRange(_companyA, _companyB);
            _context.SaveChanges();
        }


        [Fact]
        public void GetAllCompanies_ReturnsAllCompanies()
        {
            // Act
            var result = _controller.GetAllCompanies();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var companies = Assert.IsAssignableFrom<IEnumerable<Company>>(okResult.Value);

            // Assert
            Assert.Equal(2, companies.Count());
            Assert.Contains(companies, c =>
                c.Name == _companyA.Name &&
                c.Exchange == _companyA.Exchange &&
                c.Ticker == _companyA.Ticker &&
                c.Isin == _companyA.Isin &&
                c.Website == _companyA.Website
            );
            Assert.Contains(companies, c =>
                c.Name == _companyB.Name &&
                c.Exchange == _companyB.Exchange &&
                c.Ticker == _companyB.Ticker &&
                c.Isin == _companyB.Isin &&
                c.Website == _companyB.Website
            );
        }

        [Fact]
        public void GetCompanyById_WhenIsPresent_ReturnsCompany()
        {
            // Arrange
            var companyToGet = _companyA;

            // Act
            var result = _controller.GetCompanyById(companyToGet.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var company = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(companyToGet.Name, company.Name);
            Assert.Equal(companyToGet.Exchange, company.Exchange);
            Assert.Equal(companyToGet.Ticker, company.Ticker);
            Assert.Equal(companyToGet.Isin, company.Isin);
            Assert.Equal(companyToGet.Website, company.Website);
        }

        [Fact]
        public void GetCompanyById_WhenIsNotPresent_ReturnsNotFound()
        {
            // Arrange
            int nonExistentId = -1;

            // Act
            var result = _controller.GetCompanyById(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCompanyByIsin_WhenIsPresent_ReturnsCompany()
        {
            // Arrange
            var companyToGet = _companyB;

            // Act
            var result = _controller.GetCompanyByIsin(companyToGet.Isin);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var company = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(companyToGet.Name, company.Name);
            Assert.Equal(companyToGet.Exchange, company.Exchange);
            Assert.Equal(companyToGet.Ticker, company.Ticker);
            Assert.Equal(companyToGet.Isin, company.Isin);
            Assert.Equal(companyToGet.Website, company.Website);
        }

        [Fact]
        public void GetCompanyByIsin_WhenIdIsNotPresent_ReturnsNotFound()
        {
            // Arrange
            string nonExistentIsin = "NonExistentIsin";

            // Act
            var result = _controller.GetCompanyByIsin(nonExistentIsin);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateCompany_WhenIsValid_CreatesCompanyInDbAndReturnsSuccess()
        {
            // Arrange
            var createCompanyDto = new CreateCompanyDto
            {
                Name = "New Company",
                Exchange = "New Exchange",
                Ticker = "NEW1",
                Isin = "EG0000000003",
                Website = "https://new.example.com"
            };

            // Act
            var result = _controller.CreateCompany(createCompanyDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdCompany = Assert.IsType<Company>(createdAtActionResult.Value);
            Assert.Equal(createCompanyDto.Name, createdCompany.Name);
            Assert.Equal(createCompanyDto.Exchange, createdCompany.Exchange);
            Assert.Equal(createCompanyDto.Ticker, createdCompany.Ticker);
            Assert.Equal(createCompanyDto.Isin, createdCompany.Isin);
            Assert.Equal(createCompanyDto.Website, createdCompany.Website);

            var companyInDb = _context.Companies.Find(createdCompany.Id);
            Assert.NotNull(companyInDb);
            Assert.Equal(createCompanyDto.Name, companyInDb.Name);
            Assert.Equal(createCompanyDto.Exchange, companyInDb.Exchange);
            Assert.Equal(createCompanyDto.Ticker, companyInDb.Ticker);
            Assert.Equal(createCompanyDto.Isin, companyInDb.Isin);
            Assert.Equal(createCompanyDto.Website, companyInDb.Website);
        }

        [Fact]
        public void CreateCompany_WhenIsNotValid_ReturnsBadRequest()
        {
            // Arrange
            var createCompanyDto = new CreateCompanyDto
            {
                Name = "New Company",
                Exchange = "New Exchange",
                Ticker = "NEW1",
                Isin = "EG0000000003",
                Website = "https://new.example.com"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.CreateCompany(createCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void CreateCompany_WhenIsDuplicateName_ReturnsBadRequest()
        {
            // Arrange
            var createCompanyDto = new CreateCompanyDto
            {
                Name = _companyA.Name, // Duplicate name
                Exchange = "Any Exchange",
                Ticker = "ANY1",
                Isin = "EG0000000099", // Unique ISIN
                Website = "https://any.example.com"
            };

            // Act
            var result = _controller.CreateCompany(createCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
            var type = badRequestResult.Value.GetType();
            var nameProperty = type.GetProperty("Name");
            Assert.NotNull(nameProperty);
        }

        [Fact]
        public void CreateCompany_WhenIsDuplicateIsin_ReturnsBadRequest()
        {
            // Arrange
            var createCompanyDto = new CreateCompanyDto
            {
                Name = "Any Name", // Unique name
                Exchange = "Any Exchange",
                Ticker = "ANY2",
                Isin = _companyB.Isin, // Duplicate ISIN
                Website = "https://any.example.com"
            };

            // Act
            var result = _controller.CreateCompany(createCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
            var type = badRequestResult.Value.GetType();
            var isinProperty = type.GetProperty("Isin");
            Assert.NotNull(isinProperty);
        }

        [Fact]
        public void UpdateCompanyById_WhenIsValid_ReturnsSuccess()
        {
            // Arrange
            var updateCompanyDto = new UpdateCompanyDto
            {
                Name = "Updated Company A",
                Exchange = "Updated Exchange",
                Ticker = "UPD1",
                Isin = "EG0000000099",
                Website = "https://updated.example.com"
            };

            // Act
            var result = _controller.UpdateCompanyById(_companyA.Id, updateCompanyDto);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);

            var companyInDb = _context.Companies.Find(_companyA.Id);
            Assert.NotNull(companyInDb);
            Assert.Equal(updateCompanyDto.Name, companyInDb.Name);
            Assert.Equal(updateCompanyDto.Exchange, companyInDb.Exchange);
            Assert.Equal(updateCompanyDto.Ticker, companyInDb.Ticker);
            Assert.Equal(updateCompanyDto.Isin, companyInDb.Isin);
            Assert.Equal(updateCompanyDto.Website, companyInDb.Website);
        }

        [Fact]
        public void UpdateCompanyById_WhenIsNotValid_ReturnsBadRequest()
        {
            // Arrange
            var updateCompanyDto = new UpdateCompanyDto
            {
                Name = "Updated Name",
                Exchange = "Updated Exchange",
                Ticker = "UPD1",
                Isin = "Invalid Isin",
                Website = "https://updated.example.com"
            };

            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.UpdateCompanyById(_companyA.Id, updateCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void UpdateCompanyById_WhenIsDuplicateName_ReturnsBadRequest()
        {
            // Arrange
            var updateCompanyDto = new UpdateCompanyDto
            {
                Name = _companyB.Name,
                Exchange = "Updated Exchange",
                Ticker = "UPDATE",
                Isin = "EG0000000098",
                Website = "https://any.example.com"
            };

            // Act
            var result = _controller.UpdateCompanyById(_companyA.Id, updateCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            var type = badRequestResult.Value.GetType();
            var nameProperty = type.GetProperty("Name");
            Assert.NotNull(nameProperty);
        }

        [Fact]
        public void UpdateCompanyById_WhenIsDuplicateIsin_ReturnsBadRequest()
        {
            // Arrange
            var updateCompanyDto = new UpdateCompanyDto
            {
                Name = "Updated Name",
                Exchange = "Updated Exchange",
                Ticker = "UPDATE",
                Isin = _companyB.Isin,
                Website = "https://any.example.com"
            };

            // Act
            var result = _controller.UpdateCompanyById(_companyA.Id, updateCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            var type = badRequestResult.Value.GetType();
            var nameProperty = type.GetProperty("Isin");
            Assert.NotNull(nameProperty);
        }
    }

    public class CompanyControllerExceptionTests
    {
        private readonly CompanyController _controller;
        private readonly FailingCompanyContext _context;
        private readonly Mock<ILogger<CompanyController>> _mockLogger;

        public CompanyControllerExceptionTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new FailingCompanyContext(options);
            _mockLogger = new Mock<ILogger<CompanyController>>();
            _controller = new CompanyController(_context, _mockLogger.Object);
        }

        [Fact]
        public void CreateCompany_WhenSaveChangesThrows_ReturnsBadRequest()
        {
            // Arrange
            var createCompanyDto = new CreateCompanyDto
            {
                Name = "Test",
                Exchange = "TestEx",
                Ticker = "TST",
                Isin = "EG0000000004",
                Website = "https://test.com"
            };

            _context.ShouldFail = true;

            // Act
            var result = _controller.CreateCompany(createCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void UpdateCompanyById_WhenSaveChangesThrows_ReturnsBadRequest()
        {
            // Arrange
            var company = new Company
            {
                Name = "Company Name",
                Exchange = "Exchange",
                Ticker = "Ticker",
                Isin = "EG0000000005",
                Website = ""
            };
            _context.Companies.Add(company);
            _context.SaveChanges();

            var updateCompanyDto = new UpdateCompanyDto
            {
                Name = "Updated Name",
                Exchange = "Updated Exchange",
                Ticker = "TST",
                Isin = "EG0000000004",
                Website = "https://test.com"
            };

            _context.ShouldFail = true;

            // Act
            var result = _controller.UpdateCompanyById(company.Id, updateCompanyDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        private class FailingCompanyContext : CompanyContext
        {
            public bool ShouldFail { get; set; }
            public FailingCompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }
            public override int SaveChanges()
            {
                if (ShouldFail)
                    throw new DbUpdateException("Simulated failure");
                return base.SaveChanges();
            }
        }
    }
}
