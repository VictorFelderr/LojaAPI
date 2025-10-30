using LojaAPI.Loja.Domain.Interfaces;
using LojaAPI.Loja.Infra.Data.Context;
using LojaAPI.Loja.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext
builder.Services.AddDbContext<LojaDbContext>(options =>
   options.UseSqlServer("Server=LAPTOP-3BMIET23\\SQLEXPRESS;Database=LojaDB;Trusted_Connection=true;TrustServerCertificate=true;"));

// Adiciona o repositório
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();