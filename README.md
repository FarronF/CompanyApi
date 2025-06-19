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

- The API should be available at: [https://localhost:5001/swagger](https://localhost:5001/swagger) 
  (or the port specified in your `docker-compose.yml`)

4. **Shut down**:

Press `Ctrl+C` in the terminal, then run:
`docker-compose down`

## Potential improvements
- UI makes a lot of calls to the API, which can be optimized by caching the data on the client side.
- Error messages on UI are rudimentary, could be improved with more context and user-friendly messages.