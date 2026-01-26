using Microsoft.AspNetCore.Identity;

namespace finchInteligent.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
