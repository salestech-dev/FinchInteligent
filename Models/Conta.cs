using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Enums;

namespace finchInteligent.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoDeConta Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}