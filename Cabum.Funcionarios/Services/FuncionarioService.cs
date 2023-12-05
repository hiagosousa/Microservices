using Cabum.Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Funcionarios.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly ApplicationDBContext _context;

        public FuncionarioService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Funcionario>> GetAll()
        {
            return await _context.Funcionarios.ToListAsync();
        }
        public async Task<Funcionario> GetById(int id)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Funcionario> Create(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> Update(int id, Funcionario funcionario)
        {
            var funcionarioNoDb = await _context.Funcionarios.FirstOrDefaultAsync(c => c.Id == id);
            if (funcionarioNoDb == null)
            {
                throw new Exception("Funcionario não encontrado");
            }
            funcionarioNoDb.Nome = funcionario.Nome;
            funcionarioNoDb.CPF = funcionario.CPF;
            await _context.SaveChangesAsync();
            return funcionarioNoDb;
        }

        public async Task<Funcionario> Delete(int id)
        {
            var funcionarioNoDb = await _context.Funcionarios.FirstOrDefaultAsync(c => c.Id == id);
            if (funcionarioNoDb == null)
            {
                throw new Exception("Funcionario não encontrado");
            }
            _context.Remove(funcionarioNoDb);
            await _context.SaveChangesAsync();
            return funcionarioNoDb;
        }

    }
}