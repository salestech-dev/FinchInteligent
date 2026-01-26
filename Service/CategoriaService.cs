using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;

namespace finchInteligent.Service
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
        {
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Categoria> CreateCategoriaAsync(Categoria categoria, string usuarioId)
        {
            var usuarioExists = await _usuarioRepository.ExistsAsync(usuarioId);
            var categoriaExists = await _categoriaRepository.ExistsAsync(categoria.Nome, categoria.TipoDeCategoria, usuarioId);
            categoria.UsuarioId = usuarioId;
            if (usuarioExists == false)
            {
                throw new Exception("Usuário não encontrado.");
            }
            if(categoria.Nome == null|| categoria.Nome.Trim() == "")
            {
                throw new Exception("O nome da categoria não pode ser nulo.");
            }
            if(categoriaExists)
            {
                throw new Exception("Categoria já existe.");
            }
            return await _categoriaRepository.CreateAsync(categoria);

        }
    }
}
