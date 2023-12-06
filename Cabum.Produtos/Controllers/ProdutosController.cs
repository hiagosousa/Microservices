using Cabum.Produtos.Models;
using Cabum.Produtos.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQMensageiro;

namespace Cabum.Produtos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{   
    private readonly IProdutoService _produtoService;
    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mensagemService = new MensagemService();

        var produtos = await _produtoService.GetAll();
        if(produtos.Count == 0)
            return NotFound();

        return Ok("oi");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produtoNoDb = await _produtoService.GetById(id);
        return Ok(produtoNoDb);     
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Produto produto)
    {
        var produtoNovo = await _produtoService.Create(produto);
        return Ok(produtoNovo);    
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
    {
        var produtoAtualizado = await _produtoService.Update(id, produto);
        return Ok(produtoAtualizado);    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produtoExcluido = await _produtoService.Delete(id);
        return Ok(produtoExcluido);    
    }

}



