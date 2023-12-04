using Cabum.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Produtos.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetAll();
        Task<Produto> GetById(int id);        
        Task<Produto> Create( Produto produto);        
        Task<Produto> Update(int id, Produto produto);         
        Task<Produto> Delete(int id);       
    }
}