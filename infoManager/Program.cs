
using infoManager.Data;
using infoManagerAPI.Data.Repositories;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.Mapper;
using infoManagerAPI.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InfoManagerDbContext>();

builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IPhoneNumbersService, PhoneNumbersService>();

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IPhoneNumbersRepository, PhoneNumbersRepository>();


builder.Services.AutoMapperConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
