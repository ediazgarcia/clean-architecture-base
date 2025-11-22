using Application;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

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
}
else
{
    // En producción: fuerza redirección a HTTPS
    app.UseHttpsRedirection();
}

// Endpoints
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

app.MapGet("/products/{id}", async (IMediator mediator, Guid id) =>
{
    var product = await mediator.Send(new GetProductByIdQuery(id));
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPut("/products/{id}", async (IMediator mediator, Guid id, UpdateProductCommand command) =>
{
    if (id != command.Id) return Results.BadRequest();
    var result = await mediator.Send(command);
    return result ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/products/{id}", async (IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new DeleteProductCommand(id));
    return result ? Results.NoContent() : Results.NotFound();
});

app.Run();