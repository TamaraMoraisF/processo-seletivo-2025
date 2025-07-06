using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Seals.Duv.Api.Configurations;
using Seals.Duv.Application.Mappings;
using Seals.Duv.Infrastructure.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProjectDependencies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5175")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<DuvDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFront", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is Seals.Duv.Application.Exceptions.ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Validation Error",
                validationEx.Errors
            });
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "An unexpected error occurred.",
                Detail = exceptionHandlerPathFeature?.Error?.Message
            });
        }
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowViteFront");
app.UseAuthorization();
app.MapControllers();
app.Run();
