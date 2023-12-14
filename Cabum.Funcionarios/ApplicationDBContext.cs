
using Cabum.Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Funcionarios;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }


}