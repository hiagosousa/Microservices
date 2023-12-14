
using Cabum.Vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Vendas;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

}