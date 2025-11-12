var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // En desarrollo: habilita OpenAPI/Swagger
    app.MapOpenApi();
}
else
{
    // En producción: fuerza redirección a HTTPS
    app.UseHttpsRedirection();
}

// Endpoints y middlewares adicionales aquí
// app.UseAuthorization();
// app.MapControllers();

app.Run();