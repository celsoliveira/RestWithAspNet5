using Microsoft.EntityFrameworkCore;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Business;
using RestWithAspNet5.Business.Implementations;
using MySqlConnector;
using EvolveDb;
using Serilog;
using RestWithAspNet5.Repository.Generic;

var builder = WebApplication.CreateBuilder(args);
var appName = "REST API's RESTful from 0 to Azure with ASP.NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";


// Add services to the container.

builder.Services.AddControllers();

//Conexão com banco de dados
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0, 29)))
);

//Migration
if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);      
}

//Versioning API
builder.Services.AddApiVersioning();


//Injeção de Dependencia
builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IBookBusiness, BookBusiness>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}
