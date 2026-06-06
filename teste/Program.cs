using System.Diagnostics;
using System.Drawing;
using APIperson;
using Repository;
using APIperson.Models;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = "Data Source=C:\\Projetos\\Bancos\\DbPerson.db";
builder.Services.AddScoped(provider => new SQLiteConnection(connection));
builder.Services.AddScoped<DBase>();
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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();

