using LibGit2Sharp;
using MentorAi_backd.Data;
using MentorAi_backd.Middleware;
using MentorAi_backd.Repositories.Implementations;
using MentorAi_backd.Repositories.Interfaces;
using MentorAi_backd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nest;
using Octokit;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1. Add DbContext
// ---------------------------
builder.Services.AddDbContext<MentorAiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ---------------------------
// 2. Dependency Injection
// ---------------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ITokenRepo, TokenRepo>();

// ---------------------------
// 3. Add Controllers and Swagger
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

    // JWT Authorization for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer your-token')",
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
            Array.Empty<string>()
        }
    });
});

// ---------------------------
// 4. Build and Configure Pipeline
// ---------------------------
var app = builder.Build();

// Global exception handling middleware
app.UseGlobalExceptionHandling();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MentorAI API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

// Default Middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Needed if using JWT Auth
app.UseAuthorization();
app.MapControllers();

// Run the app
app.Run();
