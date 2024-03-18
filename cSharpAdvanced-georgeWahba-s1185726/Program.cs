using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using cSharpAdvanced_georgeWahba_s1185726.Services;

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

// Add controllers and other necessary services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
