using System.Diagnostics;
using System.Drawing;
using APIperson;
using Repository;
using APIperson.Models;
using System.Data.SQLite;
using APIperson._Services.PersonValidations;
using APIperson.Services;
using APIperson._Services;
using Dapper;
using APIperson.Middleware;
using APIperson.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDBaseRepository>(servicer_provider => builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<ValidationPerson>();
builder.Services.AddScoped<PersonServices>();
builder.Services.AddScoped<RepositoryServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ValidationMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();

