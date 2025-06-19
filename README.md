# CompanyApi
## Quick Start with Docker

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed

### Quick Start

1. **Clone the repository** (if you haven't already):


2. **Build and run all services**:
`docker-compose up --build`

This will build and start all containers defined in `docker-compose.yml`.

3. **Access the API**:

- The API should be available at: [http://localhost:8080/swagger](https://localhost:8080/swagger) 
  (or the port specified in your `docker-compose.yml`)
- The frontend should be available at: [http://localhost:4300/](http://localhost:4300/) 
  (or the port specified in your `docker-compose.yml`)

4. **Shut down**:

Press `Ctrl+C` in the terminal, then run:
`docker-compose down`

## Database Migrations
Note that database migrations are automatically applied on application startup. However if there are any changes to the code the migrations will need to be generated, see CompanyApi/README.md for details.

## Potential improvements
- The frontend currently runs in development mode within Docker; consider configuring it to use nginx for production readiness.
- UI makes a lot of calls to the API, which can be optimized by caching the data on the client side.
- Error messages on UI are rudimentary, could be improved with more context and user-friendly messages. i.e. if has duplicate name or isin it only gives generic error, it would be better if this was clear as to why.
- Database migrations run on every startup; ideally , they should be run manually or as part of a CI/CD pipeline.
- console errors in the browser when selecting FormField, 'Error: Permission denied to access property "ownerDocument"' possibly an angular material how these components are used here, but needs investigation. Doesn't seem to affect functionality.