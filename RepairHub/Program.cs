 
using Microsoft.Data.SqlClient;
using RepairHub;
using Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
builder.Services.DataRegisters(); 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(DocExpansion.None);
 
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
