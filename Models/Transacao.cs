using finchInteligent.Enums;

namespace finchInteligent.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public TipoTransacao Tipo { get; set; }

        public DateTime Data { get; set; }

        public string? Descricao { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ContaId { get; set; }
        public Conta Conta { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
