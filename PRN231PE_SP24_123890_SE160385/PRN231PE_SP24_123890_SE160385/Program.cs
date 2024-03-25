using DataAccessLayer.DAOs;
using Microsoft.AspNetCore.OData;
using PRN231PE_SP24_123890_SE160385;
using Repositories.Repositories;
using Repositories.Repositories.Imple;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddODataConfiguraion();
builder.Services.AddControllers();
IConfiguration config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();
builder.Services.AddJWTConfiguration((config["AppSettings:SecretKey"]));
builder.Services.AddScoped<WatercolorsPaitingDAO>();
builder.Services.AddSingleton<WatercolorsPaitingDAO>();
builder.Services.AddScoped<IWatercolorsPaintingRepository, WatercolorsPaintingRepository>();
builder.Services.AddScoped<UserAccountDAO>();
builder.Services.AddSingleton<UserAccountDAO>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
