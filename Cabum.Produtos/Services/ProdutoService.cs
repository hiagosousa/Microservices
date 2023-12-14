using Cabum.Produtos.Mensageria;
using Cabum.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Produtos.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly ApplicationDBContext _context;
        private readonly RabbitMQPublisherService<Produto> _rabbitMQPublisherService;

        public ProdutoService(ApplicationDBContext context, RabbitMQPublisherService<Produto> rabbitMQPublisherService)
        {
            _context = context;
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        public async Task<List<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        } 

        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        } 

        public async Task<Produto> Create( Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            _rabbitMQPublisherService.PublicarMensagem(produto, "criacaoProdutos");

            return produto;
        } 

        public async Task<Produto> Update(int id, Produto produto)
        {
            var produtoNoDb = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if(produtoNoDb == null){
                throw new Exception("Produto não encontrado");
            }

            produtoNoDb.Preco = produto.Preco;
            produtoNoDb.Quantidade = produto.Quantidade;
            produtoNoDb.Nome = produto.Nome;
            await _context.SaveChangesAsync();

            _rabbitMQPublisherService.PublicarMensagem(produto, "atualizacaoProdutos");

            return produtoNoDb;
        } 

        public async Task<Produto> Delete(int id)
        {
            var produtoNoDb = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if(produtoNoDb == null){
                throw new Exception("Produto não encontrado");
            }

            _context.Remove(produtoNoDb);
            await _context.SaveChangesAsync();

            _rabbitMQPublisherService.PublicarMensagem(produtoNoDb, "exclusaoProdutos");

            return produtoNoDb;
        }
        

        

    }
}