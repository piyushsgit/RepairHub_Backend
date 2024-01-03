
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepairHub;
 
using RepairHub.Areas.Realtime.ManageRequest;
using Services;
 
using SGBSystem.Middleware;
 
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

builder.Services.DataRegisters();
builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllRequests", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});


// Read JWT configuration from appsettings.json
var jwtIssuer = builder.Configuration["JwtToken:Issuer"];
var jwtAudience = builder.Configuration["JwtToken:Audience"];
var jwtSigningKey = builder.Configuration["JwtToken:SecretKey"];
builder.Services.AddAuthentication(auth =>
{
    //auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey))
    };



    //options.SaveToken = false;
    //options.RequireHttpsMetadata = false;
    //options.TokenValidationParameters = new TokenValidationParameters()
    //{
    //    ValidateIssuer = false,
    //    ValidateAudience = false,
    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey)),
    //    ClockSkew = TimeSpan.Zero
    //};
});

builder.Services.AddSwaggerGen(option =>
{
    //var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    //option.IncludeXmlComments(filePath);
    //builder.Services.AddSwaggerGen(x =>
    //{
    //    x.EnableAnnotations();
    //});

    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Repair Hub",
        Version = "v1",
        Description = "Repair Hub - online produtcs repairing website",
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

    // Set the comments path for the Swagger JSON and UI.
    
});

builder.Services.DataRegisters();


builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


 
 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(DocExpansion.None);
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
 

app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials());
app.UseMiddleware<CustomMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapHub<ManageRequest>("/Request");
app.MapControllers(); 
app.Run();
