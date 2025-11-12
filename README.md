# Clean Architecture Base (.NET 8)

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

Implementación base de Clean Architecture en .NET 8 con capas bien definidas, CQRS y principios SOLID.

---

## 📂 Estructura del Proyecto

```
clean-architecture-base/
├── src/
│   ├── Core/
│   │   ├── Domain/                    # 🎯 Entidades y reglas de negocio
│   │   │   └── Domain.csproj
│   │   └── Application/               # 💼 Casos de uso, CQRS, interfaces
│   │       └── Application.csproj
│   │
│   ├── Infrastructure/                # 🔧 Persistencia, servicios externos
│   │   └── Infrastructure.csproj
│   │
│   ├── Shared/                        # 📦 DTOs, utilidades comunes
│   │   └── Shared.csproj
│   │
│   └── Host/                          # 🌐 API ASP.NET Core
│       └── Host.csproj
│
└── tests/                             # ✅ Pruebas unitarias
    └── Tests.csproj
```

### Responsabilidades por Capa

| Capa | Descripción | Contenido Principal |
|------|-------------|---------------------|
| **Domain** | Núcleo del negocio | Entidades, Value Objects, Interfaces de repositorio, Excepciones de dominio |
| **Application** | Lógica de aplicación | Commands, Queries, Handlers (CQRS), Validadores, DTOs de aplicación |
| **Infrastructure** | Implementaciones técnicas | Repositorios, DbContext, Servicios externos, Configuraciones de BD |
| **Shared** | Código reutilizable | DTOs compartidos, Extensiones, Constantes, Helpers |
| **Host** | Punto de entrada | Controllers, Middleware, Configuración DI, Swagger |

---

## 🔗 Dependencias entre Capas

El flujo de dependencias sigue la regla de Clean Architecture: **las dependencias apuntan hacia adentro**.

```
┌─────────────────────────────────────┐
│             Host                    │  ◄─── Entry Point
│         (Web API)                   │
└──────────────┬──────────────────────┘
               │
               ├──────────► Shared ◄──────────┐
               │                              │
               ▼                              │
    ┌──────────────────┐                     │
    │  Infrastructure  │─────────────────────┤
    └──────────┬───────┘                     │
               │                              │
               ▼                              │
      ┌────────────────┐                     │
      │  Application   │─────────────────────┘
      └────────┬───────┘
               │
               ▼
        ┌──────────┐
        │  Domain  │  ◄─── Core del negocio (sin dependencias)
        └──────────┘
```

### Reglas de Dependencia

✅ **Permitido:**
- `Application` → `Domain`, `Shared`
- `Infrastructure` → `Application`, `Domain`, `Shared`
- `Host` → `Application`, `Infrastructure`, `Shared`
- `Tests` → `Domain`, `Application`, `Infrastructure`

❌ **Prohibido:**
- `Domain` → Ninguna otra capa
- `Application` → `Infrastructure`, `Host`
- Dependencias circulares

---

## 🚀 Cómo Ejecutar

### Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE: Visual Studio 2022, VS Code o Rider (opcional)
- Docker (opcional)

### 1️⃣ Restaurar Paquetes

```bash
dotnet restore
```

### 2️⃣ Compilar el Proyecto

```bash
dotnet build
```

Compilar en modo Release:
```bash
dotnet build -c Release
```

### 3️⃣ Ejecutar la API

```bash
dotnet run --project src/Host/Host.csproj
```

La API estará disponible en:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`
- **Swagger**: `https://localhost:5001/swagger`

### Ejecutar con Hot Reload

```bash
dotnet watch run --project src/Host/Host.csproj
```

---

## ✅ Testing

### Ejecutar Todas las Pruebas

```bash
dotnet test
```

### Ejecutar con Verbosidad Detallada

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Ejecutar con Cobertura de Código

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Ejecutar Solo Tests Unitarios

```bash
dotnet test --filter Category=Unit
```

### Generar Reporte de Cobertura (HTML)

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
reportgenerator -reports:coverage.cobertura.xml -targetdir:coverage-report
```

---

## 🐳 Docker

### Construir la Imagen

```bash
docker build -t clean-architecture-base ./src/Host
```

Con tag específico:
```bash
docker build -t clean-architecture-base:1.0.0 ./src/Host
```

### Ejecutar Contenedor

```bash
docker run -p 5000:80 clean-architecture-base
```

Ejecutar en modo detached con nombre:
```bash
docker run -d -p 5000:80 --name my-api clean-architecture-base
```

### Ejecutar con Variables de Entorno

```bash
docker run -p 5000:80 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__DefaultConnection="tu-connection-string" \
  clean-architecture-base
```

### Docker Compose

`docker-compose.yml`:

```bash
# Iniciar servicios
docker-compose up -d

# Ver logs
docker-compose logs -f

# Detener servicios
docker-compose down
```

## 🛠️ Stack Tecnológico

- **.NET 8** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM (recomendado)
- **MediatR** - Patrón Mediator para CQRS
- **FluentValidation** - Validaciones
- **AutoMapper** - Mapeo objeto-objeto
- **Swagger/OpenAPI** - Documentación de API
- **xUnit / NUnit** - Testing framework

---

## 📚 Patrones Implementados

- ✅ **Clean Architecture** - Separación de capas
- ✅ **CQRS** - Command Query Responsibility Segregation
- ✅ **Repository Pattern** - Abstracción de acceso a datos
- ✅ **Unit of Work** - Transacciones atómicas
- ✅ **Dependency Injection** - Inversión de control
- ✅ **Mediator Pattern** - Desacoplamiento de componentes

---

## 🔧 Configuración

Edita `src/Host/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=ServerName;Database=DbName;User Id=sa;Password=YourPassword;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🤝 Contribuir

¡Las contribuciones son bienvenidas!

1. Fork el repositorio
2. Crea una rama para tu feature: `git checkout -b feature/nueva-funcionalidad`
3. Commit tus cambios: `git commit -m 'Add: nueva funcionalidad'`
4. Push a la rama: `git push origin feature/nueva-funcionalidad`
5. Abre un Pull Request

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver [LICENSE](LICENSE) para más detalles.

---

## 📧 Soporte

- 📖 [Documentación](docs/)
- 🐛 [Reportar un Bug](issues)
- 💡 [Solicitar Feature](issues)

---

<div align="center">

**[⬆ Volver arriba](#clean-architecture-base-net-8)**

Hecho con ❤️ usando Clean Architecture

</div>