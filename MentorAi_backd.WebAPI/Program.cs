using AutoMapper;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Data;
using MentorAi_backd.Infrastructure.Persistance.Repositories;
using MentorAi_backd.Middleware;
using MentorAi_backd.Repositories.Implementations;
using MentorAi_backd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ClockSkew = TimeSpan.Zero,
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            try
            {
                // Support both NameIdentifier and Sub
                var userIdValue = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                 ?? context.Principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (string.IsNullOrWhiteSpace(userIdValue))
                {
                    Console.WriteLine("Token validation failed: User ID claim missing.");
                    context.Fail("User ID not found in token.");
                    return;
                }

                // Assuming User.Id is an integer
                if (!int.TryParse(userIdValue, out var userId))
                {
                    Console.WriteLine($"Token validation failed: Unable to parse user ID '{userIdValue}' as int.");
                    context.Fail("Invalid user ID in token.");
                    return;
                }

                var db = context.HttpContext.RequestServices.GetRequiredService<MentorAiDbContext>();
                var user = await db.Users.FindAsync(userId);
                if (user == null)
                {
                    Console.WriteLine($"Token validation failed: No user found with ID {userId}.");
                    context.Fail("User does not exist.");
                    return;
                }

                // Optional: enforce post-logout token invalidation
                //if (user.LastLogout.HasValue)
                //{
                //    var tokenIssuedAt = context.SecurityToken.ValidFrom.ToUniversalTime();
                //    var lastLogout = user.LastLogout.Value.ToUniversalTime();

                //    if (tokenIssuedAt < lastLogout)
                //    {
                //        Console.WriteLine($"Token validation failed: Token issued at {tokenIssuedAt} before last logout {lastLogout}.");
                //        context.Fail("Token was issued before the user logged out.");
                //        return;
                //    }
                //}


                Console.WriteLine($"Token validated successfully for user ID {userId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnTokenValidated exception: {ex}");
                context.Fail("Internal token validation error");
            }
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        }
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
builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();
builder.Services.AddScoped<IReviwerService, ReviewerService>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));


// ---------------------------
// 5. AutoMapper
// ---------------------------
builder.Services.AddAutoMapper(typeof(Program));
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
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
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
        c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        {
            ["activated"] = false
        };
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // CORS policy

app.UseAuthentication(); // JWT authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
