using finchInteligent.Enums;

namespace finchInteligent.Models
{
    public class Conta
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public TipoDeConta Tipo { get; set; }

        public decimal SaldoInicial { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
        public decimal Saldo { get; set; }
    }
}
