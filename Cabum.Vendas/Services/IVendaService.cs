using Cabum.Vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Vendas.Services
{
    public interface IVendaService
    {
        Task<List<Venda>> GetAll();
        Task<Venda> GetById(int id);        
        Task<Venda> Create(Venda venda);        
        Task<Venda> Update(int id, Venda venda);         
        Task<Venda> Delete(int id);       
    }
}