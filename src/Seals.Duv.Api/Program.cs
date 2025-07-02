using Microsoft.EntityFrameworkCore;
using Seals.Duv.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with InMemory
builder.Services.AddDbContext<SealsDuvDbContext>(options =>
    options.UseInMemoryDatabase("SealsDuvDb"));

// Add controllers and Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();