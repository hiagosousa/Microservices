using Cabum.Funcionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabum.Funcionarios.Services
{
    public interface IFuncionarioService
    {
        Task<List<Funcionario>> GetAll();
        Task<Funcionario> GetById(int id);        
        Task<Funcionario> Create(Funcionario funcionario);        
        Task<Funcionario> Update(int id, Funcionario funcionario);         
        Task<Funcionario> Delete(int id);       
    }
}