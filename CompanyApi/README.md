# CompanyApi

A .NET 8 Web API for managing company data.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)

## Getting Started

### 1. Clone the repository
`git clone <your-repo-url> cd CompanyApi`

### 2. Configure the database
Update `appsettings.json` or `appsettings.Development.json` with your SQL Server connection string.

### 3. Run database migrations
(optional) this will be run automatically on application start
`dotnet ef database update`

### 4. Run the API
`dotnet run`

The API will be available at `http://localhost:5000` (by default).

### 5. API Documentation

Swagger UI is available at:  
`http://localhost:5000/swagger`

## Running Tests
cd ../CompanyApi.Tests dotnet test

## Docker
To build and run the API with Docker:
docker build -t companyapi . docker run -p 5001:80 companyapi

Or use the solution-level `docker-compose.yml` for multi-service orchestration.

## Project Structure

- `Controllers/` - API endpoints
- `Models/` - Data models
- `DTOs/` - Data transfer objects
- `Data/` - Database context and migrations

## Useful Commands
If changes are made to the database models, you need to create a new migration and update the database:
- Add migration:  
  `dotnet ef migrations add <MigrationName>`

- Update database:  
  `dotnet ef database update`