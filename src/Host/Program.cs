using Application;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Infrastructure;
using MediatR;
using Serilog;

// Configure Serilog (Console only, no files)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting Clean Architecture API");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        // En desarrollo: habilita OpenAPI/Swagger
        app.UseSwagger();
        app.UseSwaggerUI();
        Log.Information("Swagger UI available at /swagger");
    }
    else
    {
        // En producción: fuerza redirección a HTTPS
        app.UseHttpsRedirection();
    }

    // Add Serilog request logging
    app.UseSerilogRequestLogging();

    // Endpoints
    app.MapPost("/products", async (IMediator mediator, CreateProductCommand command) =>
    {
        Log.Information("Creating product: {Name} - ${Price}", command.Name, command.Price);
        var id = await mediator.Send(command);
        Log.Information("Product created with ID: {ProductId}", id);
        return Results.Created($"/products/{id}", id);
    });

    app.MapGet("/products", async (IMediator mediator) =>
    {
        Log.Information("Retrieving all products");
        var products = await mediator.Send(new GetProductsQuery());
        Log.Information("Retrieved {Count} products", products.Count);
        return Results.Ok(products);
    });

    app.MapGet("/products/{id}", async (IMediator mediator, Guid id) =>
    {
        Log.Information("Retrieving product with ID: {ProductId}", id);
        var product = await mediator.Send(new GetProductByIdQuery(id));
        if (product is null)
        {
            Log.Warning("Product not found: {ProductId}", id);
            return Results.NotFound();
        }
        return Results.Ok(product);
    });

    app.MapPut("/products/{id}", async (IMediator mediator, Guid id, UpdateProductCommand command) =>
    {
        if (id != command.Id)
        {
            Log.Warning("Product ID mismatch: URL={UrlId}, Body={BodyId}", id, command.Id);
            return Results.BadRequest();
        }
        
        Log.Information("Updating product: {ProductId}", id);
        var result = await mediator.Send(command);
        
        if (!result)
        {
            Log.Warning("Product not found for update: {ProductId}", id);
            return Results.NotFound();
        }
        
        Log.Information("Product updated successfully: {ProductId}", id);
        return Results.NoContent();
    });

    app.MapDelete("/products/{id}", async (IMediator mediator, Guid id) =>
    {
        Log.Information("Deleting product: {ProductId}", id);
        var result = await mediator.Send(new DeleteProductCommand(id));
        
        if (!result)
        {
            Log.Warning("Product not found for deletion: {ProductId}", id);
            return Results.NotFound();
        }
        
        Log.Information("Product deleted successfully: {ProductId}", id);
        return Results.NoContent();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}