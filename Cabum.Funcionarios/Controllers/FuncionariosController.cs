using Cabum.Funcionarios.Models;
using Cabum.Funcionarios.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cabum.Funcionarios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuncionariosController : ControllerBase
{   
    private readonly IFuncionarioService _funcionarioService;
    public FuncionariosController(IFuncionarioService funcionarioService)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var funcionarios = await _funcionarioService.GetAll();
        if(funcionarios.Count == 0){
            return NotFound();
        }
    return Ok(funcionarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var funcionarioNoDb = await _funcionarioService.GetById(id);
        return Ok(funcionarioNoDb);     
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Funcionario funcionario)
    {
        var funcionarioNovo = await _funcionarioService.Create(funcionario);
        return Ok(funcionarioNovo);    
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Funcionario funcionario)
    {
        var funcionarioAtualizado = await _funcionarioService.Update(id, funcionario);
        return Ok(funcionarioAtualizado);    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var funcionarioExcluido = await _funcionarioService.Delete(id);
        return Ok(funcionarioExcluido);    
    }

}



