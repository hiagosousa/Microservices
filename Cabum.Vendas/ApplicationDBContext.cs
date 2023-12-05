
using Cabum.Vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Vendas;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Venda> Vendas { get; set; }

}