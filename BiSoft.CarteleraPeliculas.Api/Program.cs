using BiSoft.CarteleraPeliculas.Api.Extensions;
using BiSoft.CarteleraPeliculas.Application.Services;
using BiSoft.CarteleraPeliculas.Domain.Repositories;
using BiSoft.CarteleraPeliculas.Domain.Services;
using CarteleraPeliculas.Infrastructure.Contexts;
using CarteleraPeliculas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Base de datos SQLite
builder.Services.AddDbContext<CarteleraPeliculasContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Capa Application
builder.Services.AddScoped<PeliculaService>();

// Capa Domain
builder.Services.AddScoped<PeliculaDomainService>();

// Capa Infrastructure
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarteleraPeliculasContext>();

    Console.WriteLine("BD usada: " + db.Database.GetDbConnection().DataSource);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();