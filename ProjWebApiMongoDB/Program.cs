using Microsoft.Extensions.Options;
using ProjWebApiMongoDB.Config;
using ProjWebApiMongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration Singleton
builder.Services.Configure<ProjWebApiMongoDBSettings>(builder.Configuration.GetSection("ProjWebApiMongoDBSettings"));
builder.Services.AddSingleton<IProjWebApiMongoDBSettings>(s => s.GetRequiredService<IOptions<ProjWebApiMongoDBSettings>>().Value);
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<AddressService>();
builder.Services.AddSingleton<CityService>();

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
