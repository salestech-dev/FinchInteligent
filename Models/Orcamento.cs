namespace finchInteligent.Models
{
    public class Orcamento
    {
        public int Id { get; set; }

        public decimal ValorLimite { get; set; }

        public int Mes { get; set; } // 1 a 12
        public int Ano { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
