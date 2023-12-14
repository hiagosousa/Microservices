using Cabum.Vendas.Mensageria;
using Cabum.Vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Vendas.Services
{
    public class VendaService : IVendaService
    {
        private readonly ApplicationDBContext _context;
        private readonly RabbitMQPublisherService<Venda> _rabbitMQPublisherService;

        public VendaService(ApplicationDBContext context, RabbitMQPublisherService<Venda> rabbitMQPublisherService)
        {
            _context = context;
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        public async Task<List<Venda>> GetAll()
        {
            return await _context.Vendas.ToListAsync();
        }
        public async Task<Venda> GetById(int id)
        {
            return await _context.Vendas.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Venda> Create(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();

            // _rabbitMQPublisherService.PublicarMensagem(venda, "vendas");

            return venda;
        }

        public async Task<Venda> Update(int id, Venda venda)
        {
            var vendaNoDb = await _context.Vendas.FirstOrDefaultAsync(c => c.Id == id);
            if (vendaNoDb == null)
            {
                throw new Exception("Venda não encontrado");
            }
            vendaNoDb.Quantidade = venda.Quantidade;
            vendaNoDb.IdCliente = venda.IdCliente;
            vendaNoDb.IdFuncionario = venda.IdFuncionario;
            await _context.SaveChangesAsync();
            return vendaNoDb;
        }

        public async Task<Venda> Delete(int id)
        {
            var vendaNoDb = await _context.Vendas.FirstOrDefaultAsync(c => c.Id == id);
            if (vendaNoDb == null)
            {
                throw new Exception("Venda não encontrado");
            }
            _context.Remove(vendaNoDb);
            await _context.SaveChangesAsync();
            return vendaNoDb;
        }

    }
}