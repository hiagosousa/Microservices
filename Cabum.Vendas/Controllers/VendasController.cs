using Cabum.Vendas.Models;
using Cabum.Vendas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cabum.Vendas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{   
    private readonly IVendaService _vendaService;
    public VendasController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vendas = await _vendaService.GetAll();
        if(vendas.Count == 0){
            return NotFound();
        }
    return Ok(vendas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vendaNoDb = await _vendaService.GetById(id);
        return Ok(vendaNoDb);     
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Venda venda)
    {
        var vendaNova = await _vendaService.Create(venda);
        return Ok(vendaNova);    
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Venda venda)
    {
        var vendaAtualizada = await _vendaService.Update(id, venda);
        return Ok(vendaAtualizada);    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vendaExcluida = await _vendaService.Delete(id);
        return Ok(vendaExcluida);    
    }

}



