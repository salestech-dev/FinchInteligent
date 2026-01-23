using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Models;


namespace finchInteligent.Repository.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta> CreateAsync(Conta conta);

        Task<Conta?> GetByIdAsync(int id, int usuarioId);

        Task<IEnumerable<Conta>> GetAllByUsuarioAsync(int usuarioId);

        Task UpdateAsync(Conta conta);

        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}