# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY efscaffold/efscaffold.csproj ./efscaffold/
COPY api/api.csproj ./api/

# Restore dependencies
RUN dotnet restore ./api/api.csproj

# Copy full source
COPY . .

# Publish the Web API
RUN dotnet publish ./api/api.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose port and run
EXPOSE 8080
ENTRYPOINT ["dotnet", "api.dll"]
