using ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;
using ESTADOTC.API.Application.Mapping;
using ESTADOTC.API.Application.Validators;
using ESTADOTC.API.Infrastructure.Data;
using ESTADOTC.API.Infrastructure.HealthChecks;
using ESTADOTC.API.Infrastructure.Middleware;
using ESTADOTC.API.Infrastructure.Repositories;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;
using ESTADOTC.API.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Servicios
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetCardStatementQueryHandler>());
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IDatabaseHealthCheck, DatabaseHealthCheck>();

builder.Services.AddValidatorsFromAssemblyContaining<AddPurchaseDtoValidator>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ESTADOTC API",
        Version = "v1",
        Description = "API para consulta y gestión del estado de cuenta de tarjetas."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
