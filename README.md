
- [¿Qué es este proyecto?](#-qué-es-este-proyecto)
- [¿Qué es Clean Architecture?](#-qué-es-clean-architecture)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Cómo Ejecutar el Proyecto](#-cómo-ejecutar-el-proyecto)
- [Cómo Ejecutar los Tests](#-cómo-ejecutar-los-tests)
- [Entendiendo el Código](#-entendiendo-el-código)
- [Endpoints de la API](#-endpoints-de-la-api)
- [Ejemplos de Uso](#-ejemplos-de-uso)
- [Patrones de Diseño Utilizados](#-patrones-de-diseño-utilizados)
- [Cómo Agregar Nuevas Entidades](#-cómo-agregar-nuevas-entidades)
- [Preguntas Frecuentes](#-preguntas-frecuentes)

---

## 🎯 ¿Qué es este proyecto?

Este es un **proyecto base** que puedes usar para crear aplicaciones .NET siguiendo las mejores prácticas de la industria. 

**Piensa en esto como una "plantilla" o "esqueleto"** que ya tiene todo configurado para que puedas empezar a programar sin preocuparte por la arquitectura.

### ¿Qué hace este proyecto?

Gestiona **Productos** (como en una tienda). Puedes:
- ✅ **Crear** un producto nuevo
- ✅ **Ver** todos los productos
- ✅ **Buscar** un producto específico
- ✅ **Actualizar** un producto existente
- ✅ **Eliminar** un producto

---

## 🏛️ ¿Qué es Clean Architecture?

**Clean Architecture** es una forma de organizar tu código para que sea:
- 📦 **Ordenado** - Todo tiene su lugar
- 🔧 **Mantenible** - Fácil de modificar
- 🧪 **Testeable** - Fácil de probar
- 👥 **Entendible** - Otros programadores lo entienden rápido

### Analogía Simple

Imagina que estás construyendo una casa:

```
🏠 Tu Aplicación
├── 🎨 Domain (Planos)         → Define QUÉ es un Producto
├── 🎮 Application (Reglas)    → Define QUÉ HACER con los Productos
├── 💾 Infrastructure (Almacén) → Define DÓNDE guardar los Productos
└── 🌐 Host (Puerta)           → Define CÓMO acceder a los Productos
```

**Regla de Oro:** Las capas internas NO conocen las capas externas.
- Domain no sabe nada de SQL Server
- Application no sabe nada de la API
- Esto hace que sea fácil cambiar tecnologías sin romper todo

---

## 📁 Estructura del Proyecto

```
clean-architecture-base/
│
├── src/                                    # Código fuente
│   ├── Core/                              # Núcleo de la aplicación
│   │   ├── Domain/                        # 🎨 Capa de Dominio
│   │   │   ├── Common/
│   │   │   │   └── BaseEntity.cs         # Clase base para entidades
│   │   │   └── Entities/
│   │   │       └── Product.cs            # Entidad Product
│   │   │
│   │   └── Application/                   # 🎮 Capa de Aplicación
│   │       ├── Interfaces/
│   │       │   └── IRepository.cs        # Contrato del repositorio
│   │       ├── Features/
│   │       │   └── Products/
│   │       │       ├── Commands/         # Acciones que modifican datos
│   │       │       │   ├── CreateProductCommand.cs
│   │       │       │   ├── UpdateProductCommand.cs
│   │       │       │   └── DeleteProductCommand.cs
│   │       │       └── Queries/          # Acciones que leen datos
│   │       │           ├── GetProductsQuery.cs
│   │       │           └── GetProductByIdQuery.cs
│   │       └── DependencyInjection.cs    # Configuración de servicios
│   │
│   ├── Infrastructure/                    # 💾 Capa de Infraestructura
│   │   ├── Persistence/
│   │   │   ├── ApplicationDbContext.cs   # Contexto de EF Core
│   │   │   └── Repository.cs             # Implementación del repositorio
│   │   └── DependencyInjection.cs        # Configuración de BD
│   │
│   ├── Host/                              # 🌐 Capa de Presentación
│   │   ├── Program.cs                    # Punto de entrada + Endpoints
│   │   ├── appsettings.json              # Configuración (Connection String)
│   │   └── Host.csproj
│   │
│   └── Shared/                            # Código compartido
│
├── tests/                                 # 🧪 Tests Unitarios
│   └── Application/
│       └── Features/
│           └── Products/
│               ├── Commands/              # Tests de comandos
│               └── Queries/               # Tests de consultas
│
├── docker-compose.yml                     # Configuración Docker
└── README.md                              # Este archivo
```

---

## 🛠️ Tecnologías Utilizadas

### Backend
- **[.NET 8](https://dotnet.microsoft.com/)** - Framework principal
- **[ASP.NET Core](https://docs.microsoft.com/aspnet/core)** - Para crear la API
- **[Entity Framework Core](https://docs.microsoft.com/ef/core/)** - ORM para acceder a la base de datos
- **[MediatR](https://github.com/jbogard/MediatR)** - Patrón Mediator para CQRS
- **[SQL Server](https://www.microsoft.com/sql-server)** - Base de datos

### Testing
- **[xUnit](https://xunit.net/)** - Framework de testing
- **[Moq](https://github.com/moq/moq4)** - Para crear mocks
- **[FluentAssertions](https://fluentassertions.com/)** - Assertions legibles

### Documentación
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentación automática de la API

---

## ✅ Requisitos Previos

Antes de empezar, necesitas tener instalado:

1. **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
   ```bash
   # Verifica que lo tienes instalado
   dotnet --version
   # Debe mostrar: 8.x.x
   ```

2. **[SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)** o **SQL Server Express**
   - Puedes usar SQL Server local o una instancia remota

3. **[Visual Studio 2022](https://visualstudio.microsoft.com/)** o **[VS Code](https://code.visualstudio.com/)**
   - Recomendado: Visual Studio 2022 Community (gratis)

4. **[Git](https://git-scm.com/)** (opcional, para clonar el repo)

---

## 📥 Instalación

### Paso 1: Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/clean-architecture-base-1.git
cd clean-architecture-base-1
```

### Paso 2: Configurar la Base de Datos

1. Abre `src/Host/appsettings.json`
2. Modifica la cadena de conexión con tus datos:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU-SERVIDOR;Database=TU-BASE-DE-DATOS;User Id=TU-USUARIO;Password=TU-PASSWORD;TrustServerCertificate=True;"
  }
}
```

**Ejemplo:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductsDB;User Id=sa;Password=MiPassword123;TrustServerCertificate=True;"
  }
}
```

### Paso 3: Crear la Tabla en SQL Server

Ejecuta este script en SQL Server Management Studio o Azure Data Studio:

```sql
CREATE DATABASE ProductsDB;
GO

USE ProductsDB;
GO

CREATE TABLE Product (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);
GO
```

### Paso 4: Restaurar Paquetes NuGet

```bash
dotnet restore
```

### Paso 5: Compilar el Proyecto

```bash
dotnet build
```

---

## 🚀 Cómo Ejecutar el Proyecto

### Opción 1: Desde la Terminal

```bash
dotnet run --project src/Host/Host.csproj
```

### Opción 2: Desde Visual Studio

1. Abre `clean-architecture-base.sln`
2. Presiona `F5` o click en "▶ Host"

### ¿Funcionó?

Deberías ver algo como:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5163
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

**¡Listo!** Abre tu navegador en: `http://localhost:5163/swagger`

---

## 🧪 Cómo Ejecutar los Tests

### Ejecutar TODOS los tests

```bash
dotnet test
```

**Resultado esperado:**
```
Test Run Successful.
Total tests: 10
     Passed: 10
 Total time: 0.7 Seconds
```

### Ejecutar tests con más detalle

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Ejecutar un test específico

```bash
dotnet test --filter "FullyQualifiedName~CreateProductCommandTests"
```

---

## 📖 Entendiendo el Código

### 1️⃣ Domain Layer (Capa de Dominio)

**Archivo:** `src/Core/Domain/Entities/Product.cs`

```csharp
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

**¿Qué hace?**
- Define cómo es un "Producto"
- Hereda de `BaseEntity` que tiene el `Id` (tipo `Guid`)
- Es la **representación pura** de tu negocio

**Regla:** Esta capa NO debe tener dependencias externas (ni SQL, ni API, nada).

---

### 2️⃣ Application Layer (Capa de Aplicación)

#### Commands (Comandos) - Modifican datos

**Archivo:** `src/Core/Application/Features/Products/Commands/CreateProductCommand.cs`

```csharp
// El comando (la solicitud)
public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

// El manejador (quien ejecuta la acción)
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IRepository<Product> _repository;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        await _repository.AddAsync(product);
        return product.Id;
    }
}
```

**¿Qué hace?**
1. Recibe: nombre y precio
2. Crea un nuevo `Product`
3. Lo guarda en la base de datos
4. Devuelve el `Id` del producto creado

**Patrón CQRS:** Separamos las operaciones de escritura (Commands) de las de lectura (Queries).

#### Queries (Consultas) - Leen datos

**Archivo:** `src/Core/Application/Features/Products/Queries/GetProductsQuery.cs`

```csharp
// La consulta
public record GetProductsQuery : IRequest<List<Product>>;

// El manejador
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IRepository<Product> _repository;

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
```

**¿Qué hace?**
- Obtiene TODOS los productos de la base de datos
- Devuelve una lista de `Product`

---

### 3️⃣ Infrastructure Layer (Capa de Infraestructura)

**Archivo:** `src/Infrastructure/Persistence/Repository.cs`

```csharp
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    
    // ... más métodos
}
```

**¿Qué hace?**
- Implementa `IRepository<T>` (el contrato definido en Application)
- Usa Entity Framework Core para hablar con SQL Server
- Es **genérico** (`<T>`), funciona para cualquier entidad

**Patrón Repository:** Abstrae el acceso a datos.

---

### 4️⃣ Host Layer (Capa de Presentación)

**Archivo:** `src/Host/Program.cs`

```csharp
// Configuración de servicios
builder.Services.AddApplication();      // MediatR
builder.Services.AddInfrastructure(builder.Configuration); // SQL Server

// Endpoints de la API
app.MapPost("/products", async (IMediator mediator, CreateProductCommand command) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/products/{id}", id);
});

app.MapGet("/products", async (IMediator mediator) =>
{
    var products = await mediator.Send(new GetProductsQuery());
    return Results.Ok(products);
});
```

**¿Qué hace?**
- Define los **endpoints** (URLs) de la API
- Usa **MediatR** para enviar comandos y consultas
- Devuelve respuestas HTTP (200 OK, 201 Created, etc.)

---

## 🌐 Endpoints de la API

### Base URL
```
http://localhost:5163
```

### 1. Crear Producto
```http
POST /products
Content-Type: application/json

{
  "name": "Laptop",
  "price": 999.99
}
```

**Respuesta:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

---

### 2. Obtener Todos los Productos
```http
GET /products
```

**Respuesta:**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Laptop",
    "price": 999.99
  },
  {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "name": "Mouse",
    "price": 25.50
  }
]
```

---

### 3. Obtener Producto por ID
```http
GET /products/{id}
```

**Ejemplo:**
```http
GET /products/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

**Respuesta:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Laptop",
  "price": 999.99
}
```

---

### 4. Actualizar Producto
```http
PUT /products/{id}
Content-Type: application/json

{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Gaming Laptop",
  "price": 1299.99
}
```

**Respuesta:**
```
204 No Content
```

---

### 5. Eliminar Producto
```http
DELETE /products/{id}
```

**Respuesta:**
```
204 No Content
```

---

## 💡 Ejemplos de Uso

### Usando cURL (Terminal)

#### Crear un producto
```bash
curl -X POST http://localhost:5163/products \
  -H "Content-Type: application/json" \
  -d '{"name": "Teclado Mecánico", "price": 89.99}'
```

#### Ver todos los productos
```bash
curl http://localhost:5163/products
```

#### Buscar un producto
```bash
curl http://localhost:5163/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

---

### Usando Swagger UI

1. Abre: `http://localhost:5163/swagger`
2. Verás una interfaz gráfica con todos los endpoints
3. Click en un endpoint → "Try it out" → Completa los datos → "Execute"

**¡Es la forma más fácil de probar la API!**

---

## 🎨 Patrones de Diseño Utilizados

### 1. CQRS (Command Query Responsibility Segregation)

**¿Qué es?**
Separar las operaciones que **modifican** datos (Commands) de las que **leen** datos (Queries).

**Ventajas:**
- Código más organizado
- Fácil de optimizar cada tipo de operación
- Mejor rendimiento

**En este proyecto:**
- **Commands:** Create, Update, Delete
- **Queries:** GetAll, GetById

---

### 2. Repository Pattern

**¿Qué es?**
Una capa de abstracción entre la lógica de negocio y el acceso a datos.

**Ventajas:**
- Puedes cambiar de SQL Server a MongoDB sin tocar Application
- Fácil de testear (usas mocks)

**En este proyecto:**
```csharp
IRepository<Product> // Interfaz (contrato)
Repository<Product>  // Implementación (SQL Server)
```

---

### 3. Mediator Pattern (MediatR)

**¿Qué es?**
Un intermediario que maneja las solicitudes.

**Sin MediatR:**
```csharp
var handler = new CreateProductCommandHandler(repository);
var result = await handler.Handle(command);
```

**Con MediatR:**
```csharp
var result = await mediator.Send(command);
```

**Ventajas:**
- Desacopla el código
- Fácil agregar comportamientos (logging, validación, etc.)

---

### 4. Dependency Injection

**¿Qué es?**
En lugar de crear objetos dentro de una clase, se los "inyectas" desde afuera.

**Ejemplo:**
```csharp
public class CreateProductCommandHandler
{
    private readonly IRepository<Product> _repository;

    // El repository se inyecta aquí ⬇️
    public CreateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }
}
```

**Ventajas:**
- Fácil de testear
- Fácil de cambiar implementaciones

---

## 🆕 Cómo Agregar Nuevas Entidades

Supongamos que quieres agregar una entidad `Category` (Categoría).

### Paso 1: Crear la Entidad (Domain)

**Archivo:** `src/Core/Domain/Entities/Category.cs`

```csharp
namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
```

---

### Paso 2: Agregar al DbContext (Infrastructure)

**Archivo:** `src/Infrastructure/Persistence/ApplicationDbContext.cs`

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; } // ⬅️ Agregar esto

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Category>().ToTable("Category"); // ⬅️ Y esto
        base.OnModelCreating(modelBuilder);
    }
}
```

---

### Paso 3: Crear Commands y Queries (Application)

**Archivo:** `src/Core/Application/Features/Categories/Commands/CreateCategoryCommand.cs`

```csharp
public record CreateCategoryCommand(string Name, string Description) : IRequest<Guid>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IRepository<Category> _repository;

    public CreateCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await _repository.AddAsync(category);
        return category.Id;
    }
}
```

---

### Paso 4: Agregar Endpoints (Host)

**Archivo:** `src/Host/Program.cs`

```csharp
app.MapPost("/categories", async (IMediator mediator, CreateCategoryCommand command) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/categories/{id}", id);
});

app.MapGet("/categories", async (IMediator mediator) =>
{
    var categories = await mediator.Send(new GetCategoriesQuery());
    return Results.Ok(categories);
});
```

---

### Paso 5: Crear la Tabla en SQL Server

```sql
CREATE TABLE Category (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);
```

**¡Listo!** Ya tienes una nueva entidad funcionando.

---

## ❓ Preguntas Frecuentes

### ¿Por qué usar Guid en lugar de int para el Id?

**Respuesta:**
- Los `Guid` son únicos globalmente (no hay colisiones)
- Útil en sistemas distribuidos
- Más seguro (no se pueden adivinar IDs secuenciales)

---

### ¿Puedo usar otra base de datos?

**Sí!** Solo cambia `Infrastructure/DependencyInjection.cs`:

**Para PostgreSQL:**
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
```

**Para MySQL:**
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection")));
```

---

### ¿Cómo agrego validaciones?

Usa **FluentValidation**:

1. Instala el paquete:
```bash
dotnet add package FluentValidation.AspNetCore
```

2. Crea un validador:
```csharp
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
```

---

### ¿Cómo agrego autenticación?

Usa **JWT (JSON Web Tokens)**:

1. Instala el paquete:
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

2. Configura en `Program.cs`:
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* configuración */ });
```

---

### ¿Cómo hago migraciones de base de datos?

Con **Entity Framework Migrations**:

```bash
# Crear una migración
dotnet ef migrations add InitialCreate --project src/Infrastructure

# Aplicar la migración
dotnet ef database update --project src/Infrastructure
```

---

## 📚 Recursos Adicionales

### Documentación Oficial
- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR/wiki)

### Tutoriales Recomendados
- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [Repository Pattern](https://docs.microsoft.com/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

---

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si encuentras un bug o tienes una sugerencia:

1. Haz un Fork del proyecto
2. Crea una rama (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

## 👨‍💻 Autor

**Tu Nombre**
- GitHub: [@ediazgarcia](https://github.com/ediazgarcia)
- LinkedIn: [ediazgarcia](https://linkedin.com/in/ediazgarcia)

---

## 🎉 ¡Felicidades!

Si llegaste hasta aquí, ya entiendes cómo funciona este proyecto. 

**Próximos pasos:**
1. ✅ Ejecuta el proyecto
2. ✅ Prueba los endpoints en Swagger
3. ✅ Ejecuta los tests
4. ✅ Agrega tu propia entidad
5. ✅ Personaliza según tus necesidades

**¡Happy Coding!** 🚀