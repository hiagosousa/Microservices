using Cabum.Funcionarios;
using Cabum.Funcionarios.Models;
using Cabum.Funcionarios.Services;
using Microsoft.EntityFrameworkCore;
using Cabum.Funcionarios.Mensageria;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

builder.Services.AddScoped<RabbitMQPublisherService<Funcionario>>();
builder.Services.AddHostedService<RabbitMQListenerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar CORS para permitir todas as origens
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configurar roteamento, se necessário

app.MapSwagger();

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
