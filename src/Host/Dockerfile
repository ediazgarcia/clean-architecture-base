# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY *.sln .
COPY src/Core/Domain/Domain.csproj src/Core/Domain/
COPY src/Core/Application/Application.csproj src/Core/Application/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/
COPY src/Shared/Shared.csproj src/Shared/
COPY src/Host/Host.csproj src/Host/

RUN dotnet restore src/Host/Host.csproj

# Copiar todo el código y compilar
COPY . .
WORKDIR /src/Host
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]