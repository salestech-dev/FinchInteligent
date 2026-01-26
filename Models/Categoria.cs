using finchInteligent.Enums;

namespace finchInteligent.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public TipoDeCategoria TipoDeCategoria { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
