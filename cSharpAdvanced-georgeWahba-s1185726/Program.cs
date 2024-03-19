using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using cSharpAdvanced_georgeWahba_s1185726.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add DbContext and other services
builder.Services.AddDbContext<cSharpAdvanced_georgeWahba_s1185726Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cSharpAdvanced_georgeWahba_s1185726Context") ?? throw new InvalidOperationException("Connection string 'cSharpAdvanced_georgeWahba_s1185726Context' not found.")));

// Add repositories and services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ILandlordRepository, LandlordRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<SearchService>();

// Add controllers with JSON options for handling circular references
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Define Swagger document for version 1
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Version 1",
        Version = "v1"
    });

    // Define Swagger document for version 2
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Version 2",
        Version = "v2"
    });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://cloudbnb-df3c1.web.app")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure middleware
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Version 1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API Version 2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
