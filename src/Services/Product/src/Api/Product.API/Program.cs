using Product.Application;
using Product.Persistence;
using Product.Infrastructure;
using Product.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// --- Add services to the DI container. ---
// 1. Application lay?n?n servisl?rini ?lav? et (MediatR, AutoMapper, FluentValidation, Behaviors)
builder.Services.AddApplicationServices();

// 2. Persistence lay?n?n servisl?rini ?lav? et (DbContext, Repositories, UnitOfWork)
builder.Services.AddPersistenceServices(builder.Configuration);

// 3. Infrastructure lay?n?n servisl?rini ?lav? et (g?l?c?kd? Email, Cache v? s.)
builder.Services.AddInfrastructureServices(builder.Configuration);

// ASP.NET Core-un öz servisl?ri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (Cross-Origin Resource Sharing) siyas?tini ?lav? etm?k (Frontend il? ?laq? üçün vacibdir)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

// --- Configure the HTTP request pipeline. ---

// Development mühitind? Swagger UI-? aktivl??diririk.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

    
    app.UseMiddleware<ErrorHandlerMiddleware>();

// CORS siyas?tini t?tbiq edirik.
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();