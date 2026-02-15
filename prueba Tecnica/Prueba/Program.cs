using Prueba.BusinessLogic;
using Prueba.BusinessLogic.Services;
using Prueba.DataAccess;
using Prueba.DataAccess.Contexrt;
using Prueba.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("prueba_con");

// Database Context
builder.Services.AddDbContext<OrderManagementDBContext>(option => option.UseSqlServer(conectionString));
builder.Services.AddHttpContextAccessor();

// Register Repositories and Services
ServiceConfiguration.DataAccess(builder.Services, conectionString);
ServiceConfiguration.businessLogic(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfileExtensions>());

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
