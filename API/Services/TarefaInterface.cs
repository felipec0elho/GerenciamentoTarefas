using API.Models;

namespace API;
public interface TarefaInterface
{
    Task<ServicesResponse<List<Tarefa>>> GetTarefas();
        Task<ServicesResponse<List<Tarefa>>> CreateTarefas(Tarefa tarefaCadastrada);
        Task<ServicesResponse<Tarefa>> GetUsuarioById(int id);
        Task<ServicesResponse<List<Tarefa>>> UpdateUsuario(Tarefa tarefaeditada);
}

public class ServicesResponse<T>
{
}