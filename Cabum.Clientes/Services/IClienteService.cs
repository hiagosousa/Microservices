using Cabum.Clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Clientes.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();
        Task<Cliente> GetById(int id);        
        Task<Cliente> Create(Cliente cliente);        
        Task<Cliente> Update(int id, Cliente cliente);         
        Task<Cliente> Delete(int id);       
    }
}