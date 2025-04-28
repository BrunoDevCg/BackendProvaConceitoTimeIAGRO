using BuscaLivro.Domain.Interfaces;
using BuscaLivro.Infrastructure.Repository;
using BuscaLivro.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços (injeção de dependência)
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

// Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
