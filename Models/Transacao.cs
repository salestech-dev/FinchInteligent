using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Enums;

namespace finchInteligent.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Conta Conta { get; set; }
        public Usuario Usuario { get; set; }
        public Categoria Categoria { get; set; }
        public string Descricao { get; set; }

    }
}