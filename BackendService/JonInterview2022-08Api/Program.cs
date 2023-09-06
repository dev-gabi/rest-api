using BL;
using Dal;
using Entities.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configure the HTTP request pipeline.

builder.Services.AddDbContext<NamesContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("Names")));
builder.Services.Configure<ActionTypes>(builder.Configuration.GetSection("ActionTypes"));

builder.Services.AddScoped<INameService, NamesService>();
builder.Services.AddScoped<NamesDal>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();

