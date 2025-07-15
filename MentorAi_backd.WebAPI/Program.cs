using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Data;
using MentorAi_backd.Middleware;
using AutoMapper;
using MentorAi_backd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MentorAi_backd.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1. Configuration
// ---------------------------

// Add DbContext with SQL Server connection string from appsettings.json
builder.Services.AddDbContext<MentorAiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ---------------------------
// 2. Authentication (JWT)
// ---------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

// ---------------------------
// 3. CORS (Optional)
// ---------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ---------------------------
// 4. Dependency Injection
// ---------------------------
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));


// ---------------------------
// 5. AutoMapper
// ---------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ---------------------------
// 6. Add Controllers & Swagger
// ---------------------------
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MentorAI API",
        Version = "v1",
        Description = "API for MentorAI Application",
        Contact = new OpenApiContact
        {
            Name = "Support",
            Email = "support@mentorai.com"
        }
    });

    // Add JWT Auth support to Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// ---------------------------
// 7. Build App
// ---------------------------
var app = builder.Build();

// ---------------------------
// 8. Middleware Pipeline
// ---------------------------

// Use custom global exception handling middleware (make sure this is implemented in your Middleware folder)
app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MentorAI API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // CORS policy

app.UseAuthentication(); // JWT authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
