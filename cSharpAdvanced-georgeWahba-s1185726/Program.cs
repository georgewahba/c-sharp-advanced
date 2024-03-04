using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;
using cSharpAdvanced_georgeWahba_s1185726.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<cSharpAdvanced_georgeWahba_s1185726Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cSharpAdvanced_georgeWahba_s1185726Context") ?? throw new InvalidOperationException("Connection string 'cSharpAdvanced_georgeWahba_s1185726Context' not found.")));

// Add services to the container.
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ILandlordRepository, LandlordRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();


builder.Services.AddScoped<SearchService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
var app = builder.Build();


app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
