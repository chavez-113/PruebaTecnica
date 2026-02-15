using Prueba.DataAccess;
using Microsoft.EntityFrameworkCore;
using Prueba.BusinessLogic;
using Prueba.Extensions;
using Prueba.DataAccess.Context;
using Prueba.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("prueba_con");

// Database Context
builder.Services.AddDbContext<PruebaContext>(option => option.UseSqlServer(conectionString));
builder.Services.AddHttpContextAccessor();

// Register Repositories and Services
ServiceConfiguration.DataAccess(builder.Services, conectionString);
ServiceConfiguration.businessLogic(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfileExtensions));

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
