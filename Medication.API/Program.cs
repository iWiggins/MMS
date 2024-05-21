using Medication.API.Model;
using Medication.Core;
using Microsoft.EntityFrameworkCore;

const string POLICY_ALLOW_ALL = "POLICY_ALLOW_ALL";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options => options.AddPolicy(
		POLICY_ALLOW_ALL,
        policy =>
            policy.AllowAnyOrigin()
        ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MedicationContext>();
builder.Services.AddScoped<MedicationMapper>();
builder.Services.AddScoped<ManufacturerMapper>();
builder.Services.AddScoped<IMedicationDatabase, MedicationDatabase>();

var app = builder.Build();

// Create database if it does not exist.
app.Services.GetService<MedicationContext>()!.Database.Migrate();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(POLICY_ALLOW_ALL);

app.UseAuthorization();

app.MapControllers();

app.Run();
