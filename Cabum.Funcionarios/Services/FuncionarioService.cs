using Cabum.Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Funcionarios.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly ApplicationDBContext _context;

        private readonly RabbitMQPublisherService<Funcionario> _rabbitMQPublisherService;

        public FuncionarioService(ApplicationDBContext context, RabbitMQPublisherService<Funcionario> rabbitMQPublisherService)
        {
            _context = context;
            _rabbitMQPublisherService = rabbitMQPublisherService;
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

            _rabbitMQPublisherService.PublicarMensagem(funcionario, "criacaoFuncionarios");

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

            _rabbitMQPublisherService.PublicarMensagem(funcionario, "atualizacaoFuncionarios");

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

            _rabbitMQPublisherService.PublicarMensagem(funcionario, "exclusaoFuncionarios");

            return funcionarioNoDb;
        }

    }
}