using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
