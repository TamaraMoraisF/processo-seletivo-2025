using Microsoft.EntityFrameworkCore;
using Seals.Duv.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados PostgreSQL
builder.Services.AddDbContext<DuvDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o dos controllers e op��es JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configura��o do CORS para permitir requisi��es do front-end React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5174") // ? sem barra no final
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowFrontLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();