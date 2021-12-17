using Microsoft.AspNetCore.Mvc;
using PowerUp.Mongo.Entidades;
using PowerUp.Mongo.Repositorios;

namespace PowerUp.Mongo.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepositorio _repositorio;

    public PessoaController(IPessoaRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _repositorio.PrimeiroAsync(x => x.Id == id));
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repositorio.FiltrarAsync(x=> true));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pessoa pessoa)
    {
        await _repositorio.AdicionarAsync(pessoa);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(Pessoa pessoa)
    {
        await _repositorio.AtualizarAsync(pessoa);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Pessoa pessoa)
    {
        await _repositorio.DeletarAsync(pessoa);

        return Ok();
    }

    [HttpPatch("{id:guid}/nome/{nome}")]
    public async Task<IActionResult> PatchName([FromRoute] Guid id, [FromRoute] string nome)
    {
        var pessoa = await _repositorio.PrimeiroAsync(x => x.Id == id);
        pessoa.Nome = nome;
        await _repositorio.AtualizarAsync(pessoa);

        return Ok();
    }

    [HttpPatch("{id:guid}/idade/{idade:int}")]
    public async Task<IActionResult> PatchIdade([FromRoute] Guid id, [FromRoute] byte idade)
    {
        var pessoa = await _repositorio.PrimeiroAsync(x => x.Id == id);
        pessoa.Idade = idade;
        await _repositorio.AtualizarAsync(pessoa);

        return Ok();
    }
}