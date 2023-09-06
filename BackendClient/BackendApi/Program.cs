using BL;
using Entities.Configuration;
using SA;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("Api"));

builder.Services.AddScoped<INamesSa, NamesSA>();
builder.Services.AddScoped<NamesBl>();

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
