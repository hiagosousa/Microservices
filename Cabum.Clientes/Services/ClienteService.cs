using Cabum.Clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Clientes.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDBContext _context;

        public ClienteService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }
        public async Task<Cliente> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Update(int id, Cliente cliente)
        {
            var clienteNoDb = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (clienteNoDb == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            clienteNoDb.Nome = cliente.Nome;
            clienteNoDb.CPF = cliente.CPF;
            await _context.SaveChangesAsync();
            return clienteNoDb;
        }

        public async Task<Cliente> Delete(int id)
        {
            var clienteNoDb = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (clienteNoDb == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            _context.Remove(clienteNoDb);
            await _context.SaveChangesAsync();
            return clienteNoDb;
        }

    }
}