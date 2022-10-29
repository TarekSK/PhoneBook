using Application.Core;
using Application.Query.Company;
using Application.Command.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Interface;
using Persistence.Repository;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Main Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Context
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// MediatR - Handlers
builder.Services.AddMediatR(typeof(GetAllCompanyQueryHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(AddCompanyCommandHandler).GetTypeInfo().Assembly);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
