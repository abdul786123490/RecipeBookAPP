//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using RecipeBook.Application.Abstractions;
//using RecipeBook.Application.Interfaces.Repositories;
//using RecipeBook.Application.Interfaces.Services;
//using RecipeBook.Application.Services.Implementations;
//using RecipeBook.Infrastructure;
//using RecipeBook.Infrastructure.Repositories;

//using System.Text;
//using MediatR;

//var builder = WebApplication.CreateBuilder(args);

//// -----------------------------------------------------------------------------
//// 1. Add Controllers
//// -----------------------------------------------------------------------------
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

//// -----------------------------------------------------------------------------
//// 2. Configure Database Context (EF Core)
//// -----------------------------------------------------------------------------
//builder.Services.AddDbContext<RecipeBookDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

//// ✅ Register IApplicationDbContext for MediatR Handlers & Repositories
//builder.Services.AddScoped<IApplicationDbContext, RecipeBookDbContext>();

//// -----------------------------------------------------------------------------
//// 3. Register Repositories (Dependency Injection)
//// -----------------------------------------------------------------------------
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
////builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

//// -----------------------------------------------------------------------------
//// 4. Configure MediatR
//// -----------------------------------------------------------------------------
//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssemblies(
//        typeof(RecipeBook.Application.Features.Users.Commands.RegisterUserCommand).Assembly));

//// -----------------------------------------------------------------------------
//// 5. Configure JWT Authentication
//// -----------------------------------------------------------------------------
//var jwtKey = builder.Configuration["Jwt:Key"] ?? "ThisIsYourSecretKeyForJWT";
//var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "RecipeBookAPI";

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtIssuer,
//        ValidAudience = "RecipeBookUsers",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//    };
//});

//// -----------------------------------------------------------------------------
//// 6. JWT Token Service
//// -----------------------------------------------------------------------------
//builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

//// -----------------------------------------------------------------------------
//// 7. Configure Swagger with JWT Authentication
//// -----------------------------------------------------------------------------
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "RecipeBook API",
//        Version = "v1",
//        Description = "API documentation for RecipeBook with JWT authentication"
//    });

//    // ✅ Enable JWT Auth in Swagger UI
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Enter JWT token below. **Do not include 'Bearer' keyword**.\n\nExample: `eyJhbGciOi...`",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

//// -----------------------------------------------------------------------------
//// 8. Build the App
//// -----------------------------------------------------------------------------
//var app = builder.Build();

//// -----------------------------------------------------------------------------
//// 9. Middleware Pipeline
//// -----------------------------------------------------------------------------
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook API v1");
//        c.RoutePrefix = "swagger";
//    });
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeBook.Application.Abstractions;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Application.Interfaces.Services;
using RecipeBook.Application.Services.Implementations;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Repositories;
using System.Text;
using MediatR;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------------------------
// 1. Add Controllers
// -----------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// -----------------------------------------------------------------------------
// 2. Configure Database Context (EF Core)
// -----------------------------------------------------------------------------
builder.Services.AddDbContext<RecipeBookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

// ✅ Register IApplicationDbContext for MediatR Handlers & Repositories
builder.Services.AddScoped<IApplicationDbContext, RecipeBookDbContext>();

// -----------------------------------------------------------------------------
// 3. Register Repositories
// -----------------------------------------------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

// -----------------------------------------------------------------------------
// 4. Configure MediatR
// -----------------------------------------------------------------------------
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(RecipeBook.Application.Features.Users.Commands.RegisterUserCommand).Assembly));

// -----------------------------------------------------------------------------
// 5. Configure JWT Authentication
// -----------------------------------------------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ThisIsYourSecretKeyForJWT";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "RecipeBookAPI";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = "RecipeBookUsers",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        // ✅ CRITICAL FIX FOR ROLES & USERNAME CLAIMS
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});

// -----------------------------------------------------------------------------
// 6. JWT Token Service
// -----------------------------------------------------------------------------
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// -----------------------------------------------------------------------------
// 7. Configure Swagger with JWT Authentication
// -----------------------------------------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RecipeBook API",
        Version = "v1",
        Description = "API documentation for RecipeBook with JWT authentication"
    });

    // ✅ Enable JWT Auth in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token ONLY, Swagger will add 'Bearer' automatically.\n\nExample: `eyJhbGciOi...`",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
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

// -----------------------------------------------------------------------------
// 8. Build the App
// -----------------------------------------------------------------------------
var app = builder.Build();

// -----------------------------------------------------------------------------
// 9. Middleware Pipeline
// -----------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// ✅ Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
