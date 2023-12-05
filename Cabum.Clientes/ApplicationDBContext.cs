
using Cabum.Clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Clientes;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Cliente> Clientes { get; set; }

}