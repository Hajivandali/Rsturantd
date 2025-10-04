## Running the Project with Docker

This project provides Docker support for the ASP.NET API (`RestaurantSystem.API`) using .NET 9.0. The setup is managed via the included `Dockerfile` and `docker-compose.yml` files.

### Requirements
- **.NET Version:** 9.0 (as specified in the Dockerfile)
- **Docker Compose:** Uses the `docker-compose.yml` in the project root

### Build and Run Instructions
1. Ensure Docker and Docker Compose are installed on your system.
2. From the project root, run:
   ```sh
   docker compose up --build
   ```
   This will build and start the API service.

### Service Details
- **Service Name:** `csharp-restaurant-api`
- **Build Context:** `./RestaurantSystem.API`
- **Dockerfile Location:** `./RestaurantSystem.API/Dockerfile`
- **Exposed Port:** `80` (mapped to host port 80)
- **Network:** `restaurant-net` (custom bridge network)

### Environment Variables
- No required environment variables are specified in the Dockerfile or compose file by default.
- If you need to add environment variables, you can create a `.env` file in `./RestaurantSystem.API` and uncomment the `env_file` line in `docker-compose.yml`.

### Healthcheck
- The API container includes a healthcheck on `http://localhost:80/health`.

### Special Configuration
- The Dockerfile uses multi-stage builds for efficient image creation and runs the API as a non-root user for security.
- If you need to add a database (e.g., PostgreSQL), uncomment and configure the relevant sections in `docker-compose.yml`.

---
*For further customization, review the `docker-compose.yml` and `Dockerfile` in `./RestaurantSystem.API`.*