using WordFinderResolver.Service;
using WordFinderResolver.Service.Validations.Chains;
using WordFinderResolver.Service.Validations.Rules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<WordFinderFactory>();
builder.Services.AddSingleton<IWordFinderFactory, WordFinderFactory>();
builder.Services.AddScoped<WordFinderService>();
builder.Services.AddScoped<MatrixLengthValidation>();
builder.Services.AddScoped<MatrixSquareValidation>();
builder.Services.AddScoped<MatrixValidationsChains>();
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
