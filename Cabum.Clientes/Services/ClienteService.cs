using Cabum.Clientes.Models;
using Cabum.Vendas.Mensageria;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Clientes.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDBContext _context;

        private readonly RabbitMQPublisherService<Cliente> _rabbitMQPublisherService;

        public ClienteService(ApplicationDBContext context, RabbitMQPublisherService<Cliente> rabbitMQPublisherService)
        {
            _context = context;
            _rabbitMQPublisherService = rabbitMQPublisherService;
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

            _rabbitMQPublisherService.PublicarMensagem(cliente, "criacaoClientes");

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

            _rabbitMQPublisherService.PublicarMensagem(cliente, "atualizacaoClientes");

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

            _rabbitMQPublisherService.PublicarMensagem(clienteNoDb, "exclusaoClientes");

            return clienteNoDb;
        }

    }
}