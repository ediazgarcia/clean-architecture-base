<!--
Guidance for AI coding agents working on this repository.
Write concise, actionable edits only — reference files shown below.
-->
# Copilot / AI Agent Instructions for clean-architecture-base

This repository implements a small Clean Architecture example (Products). The goal
is to keep edits minimal, respect project conventions, and prefer the code as
source-of-truth over outdated docs.

High-level architecture
- **Layers**: `src/Core/Domain` (entities, `BaseEntity`), `src/Core/Application`
  (CQRS commands/queries, `IRepository<T>`), `src/Infrastructure` (Dapper-based
  `Repository<T>`, `IDbConnection` DI), `src/Host` (Minimal API, Serilog),
  `src/Shared` (DTOs).
- **Data flow**: HTTP endpoints in `src/Host/Program.cs` -> MediatR ->
  Application handlers -> `IRepository<T>` -> `Infrastructure.Repository<T>` -> SQL
  (via `IDbConnection` + Dapper). DI hooks are `AddApplication()` and
  `AddInfrastructure(configuration)`.

Important, discoverable patterns and conventions
- The generic repository (`Repository<T>`) expects a table named exactly like
  the entity type (e.g. `Product`). It builds SQL dynamically using property
  names (excluding `Id`). Be careful when adding entities with computed or
  non-column properties.
- Repositories use `Dapper` and `IDbConnection` (SqlConnection). Connection is
  registered as transient in `src/Infrastructure/DependencyInjection.cs`.
- Application layer uses MediatR. `src/Core/Application/DependencyInjection.cs`
  calls `AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));`.
  Note: when investigating handler registration prefer to reference handler
  types explicitly (e.g. `typeof(SomeHandler).Assembly`) if registration
  mismatches are observed.
- DTOs live in `src/Shared/DTOs`. Watch for ID type mismatches — the domain
  `BaseEntity.Id` is a `Guid` (see `src/Core/Domain/Common/BaseEntity.cs`) but
  `ProductDto` currently uses `int Id` — reconcile when adding endpoints or
  mappings.

Build, run, and test workflows (concrete commands)
- Build everything: `dotnet build` (run from repository root).
- Run the API: `dotnet run --project src/Host/Host.csproj` (or open the solution
  in an IDE and run the `Host` project). Add `-f net8.0` if you need a specific TFM.
- Run tests: `dotnet test` (root will run `tests/` project).
- Docker: `docker-compose.yml` and `src/Host/Dockerfile` exist. Inspect
  `appsettings.json` for connection strings before running containers.

What to change and where (common tasks)
- Add a new entity/feature:
  - Add entity model in `src/Core/Domain/Entities` (inherit `BaseEntity`).
  - Add Commands/Queries and handlers in `src/Core/Application/Features/<Entity>`.
  - If you need special SQL or behavior, implement a specific repository
    interface in `src/Core/Application/Interfaces` and provide the
    implementation in `src/Infrastructure/Persistence` and register it via
    `src/Infrastructure/DependencyInjection.cs`.
  - Add DTOs to `src/Shared/DTOs` and map between domain and DTO in handlers
    or a small mapping helper.
- Fixing bugs in data access:
  - Inspect `Repository<T>` — it assumes property names map 1:1 to DB columns.
  - Queries are built via string interpolation; prefer parameterized Dapper
    usage as already practiced, and avoid inline string concatenation for
    identifiers without validation.

Notable inconsistencies for human review
- `README.md` lists Entity Framework Core as a technology, but the codebase
  uses `Dapper` + `IDbConnection`. Trust the code over the README when in
  doubt.
- `ProductDto.Id` is an `int` while `BaseEntity.Id` is a `Guid` — mapping
  decisions should be explicit (do not assume implicit coercion).

Editing rules for AI agents
- Make the smallest change that achieves the goal. Prefer edits that are
  contained to a single project/assembly when possible.
- When adding DI registrations, follow existing patterns: use `AddScoped` for
  repository types and register `IDbConnection` as transient using the
  connection string `DefaultConnection`.
- Do not change database migration or schema without tests or an explicit
  migration plan; update SQL scripts or README instructions instead.
- If you modify public APIs (endpoints, DTOs, handler contracts), update the
  tests under `tests/` and run `dotnet test`.

References (examples in repo)
- DI (application): `src/Core/Application/DependencyInjection.cs`
- DI (infrastructure): `src/Infrastructure/DependencyInjection.cs`
- Generic repository: `src/Infrastructure/Persistence/Repository.cs`
- Minimal API endpoints: `src/Host/Program.cs`
- Domain base: `src/Core/Domain/Common/BaseEntity.cs`
- DTO example: `src/Shared/DTOs/ProductDto.cs`

If anything here is unclear or you want the agent to follow stricter rules
(naming, mapping libraries, or to prefer EF over Dapper), tell me and I will
adjust these instructions.
