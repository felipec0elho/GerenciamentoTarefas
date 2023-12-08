using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly AppDataContext _context;

    public TarefaController(AppDataContext context) =>
        _context = context;

    // GET: api/tarefa/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/tarefa/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Tarefa tarefa)
    {
        try
        {
            Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
            if (categoria == null)
            {
                return NotFound();
            }
            tarefa.Categoria = categoria;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PATCH: api/tarefa/alterar
    [HttpPut]
    [Route("alterar/{status}")]
    public IActionResult Alterar([FromRoute] string status,
    [FromBody] Tarefa tarefa)
    {
        try
        {
            Tarefa? tarefaCadastrada =
                _context.Tarefas.FirstOrDefault(x => x.Status == status);

            if (tarefaCadastrada != null)
            {
                if(tarefaCadastrada.Status == "Não iniciada")
                {
                    tarefaCadastrada.Status = "Em andamento";
                }else if(tarefaCadastrada.Status == "Em andamento")
                {
                    tarefaCadastrada.Status = "Concluida";
                }
                _context.Tarefas.Update(tarefaCadastrada);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
    }
    

     // GET: api/tarefa/concluidas
[HttpGet]
    [Route("concluidas")]
    public IActionResult Concluidas([FromBody] Tarefa tarefa)
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).Where(x => x.Status == "Concluida").ToList();
           
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //GET	api/tarefa/naoconcluidas
 [HttpGet]
    [Route("naoconcluidas")]
    public IActionResult NaoConcluidas([FromBody] Tarefa tarefa)
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).Where(x => x.Status != "Concluida").ToList();
           
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
