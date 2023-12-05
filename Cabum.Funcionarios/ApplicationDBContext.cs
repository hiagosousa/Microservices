
using Cabum.Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Funcionarios;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }

    public DbSet<Funcionario> Funcionarios { get; set; }

}