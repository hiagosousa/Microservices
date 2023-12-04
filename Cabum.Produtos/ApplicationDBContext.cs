
using Cabum.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Produtos;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Produto> Produtos { get; set; }

}