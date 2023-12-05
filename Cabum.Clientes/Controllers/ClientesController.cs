using Cabum.Clientes.Models;
using Cabum.Clientes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cabum.Clientes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{   
    private readonly IClienteService _clienteService;
    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteService.GetAll();
        if(clientes.Count == 0){
            return NotFound();
        }
    return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var clienteNoDb = await _clienteService.GetById(id);
        return Ok(clienteNoDb);     
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Cliente cliente)
    {
        var clienteNovo = await _clienteService.Create(cliente);
        return Ok(clienteNovo);    
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
    {
        var clienteAtualizado = await _clienteService.Update(id, cliente);
        return Ok(clienteAtualizado);    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteExcluido = await _clienteService.Delete(id);
        return Ok(clienteExcluido);    
    }

}



