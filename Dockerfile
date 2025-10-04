# syntax=docker/dockerfile:1

ARG DOTNET_VERSION=9.0

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder
WORKDIR /src

# Copy solution and project files first for restore (use --link for faster copy)
COPY --link ../RestaurantSystem.sln ./
COPY --link ../RestaurantSystem.Core/RestaurantSystem.Core.csproj ./RestaurantSystem.Core/
COPY --link ../RestaurantSystem.Application/RestaurantSystem.Application.csproj ./RestaurantSystem.Application/
COPY --link ../RestaurantSystem.Infrastructure/RestaurantSystem.Infrastructure.csproj ./RestaurantSystem.Infrastructure/
COPY --link RestaurantSystem.API.csproj ./RestaurantSystem.API/

# Restore dependencies with cache mounts
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore ./RestaurantSystem.sln

# Copy all source files (use --link for faster copy)
COPY --link ../RestaurantSystem.Core ./RestaurantSystem.Core
COPY --link ../RestaurantSystem.Application ./RestaurantSystem.Application
COPY --link ../RestaurantSystem.Infrastructure ./RestaurantSystem.Infrastructure
COPY --link . ./RestaurantSystem.API

# Publish the API project
RUN dotnet publish ./RestaurantSystem.API/RestaurantSystem.API.csproj -c Release -o /app/publish --no-restore

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create non-root user
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

# Copy published output from builder
COPY --from=builder /app/publish .

# Expose default ASP.NET port
EXPOSE 80

# Healthcheck (optional, can be customized)
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 CMD wget --spider -q http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "RestaurantSystem.API.dll"]
