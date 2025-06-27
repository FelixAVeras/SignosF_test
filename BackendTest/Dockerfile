# Use a .NET 8 image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY ProductsAPI/*.csproj ./ProductsAPI/
RUN dotnet restore

# Copy the rest of the code
COPY ProductsAPI/. ./ProductsAPI/
WORKDIR /app/ProductsAPI
RUN dotnet publish -c Release -o out

# Final image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ProductsAPI/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "ProductsAPI.dll"]
