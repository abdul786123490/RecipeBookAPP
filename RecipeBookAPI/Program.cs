

using RecipeBook.Application.DependencyInjection;
using RecipeBook.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ? Register Application Layer
builder.Services.AddApplicationServices();

// ? Register Infrastructure Layer (DbContext + Repositories + JWT)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

