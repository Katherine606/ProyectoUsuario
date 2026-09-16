using Microsoft.Data.SqlClient;
using System.Data;
using UsuarioAPI.Middlewares;
using UsuarioAPI.Repositories;
using UsuarioAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// 1. dapper conexion
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("connectionDB")));

builder.Services.AddScoped<UsuarioRepo>();
builder.Services.AddScoped<UsuarioServ>();
builder.Services.AddScoped<ValidacionesServ>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseMiddleware<ExcepcionesGlobales>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuarioApi v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
