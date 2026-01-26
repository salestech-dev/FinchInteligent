using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Enums;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;

namespace finchInteligent.Service
{
    public class OrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public OrcamentoService(IUsuarioRepository usuarioRepository, ICategoriaRepository categoriaRepository, IOrcamentoRepository orcamentoRepository)
        {
            _categoriaRepository = categoriaRepository;
            _orcamentoRepository = orcamentoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Orcamento> CreateOrcamentoAsync(Orcamento orcamento, string usuarioId)
        {
            var usuarioExists = await _usuarioRepository.ExistsAsync(usuarioId);
            var orcamentoDuplicado = await _orcamentoRepository.ExistsAsync(
                orcamento.CategoriaId,
                orcamento.Mes,
                orcamento.Ano,
                usuarioId);


            if (usuarioExists ==  false)
                throw new Exception("Usuário não encontrado.");

            var categoria = await _categoriaRepository
                .GetByIdAndUsuarioAsync(orcamento.CategoriaId, usuarioId);

            if (categoria == null)
                throw new Exception("Categoria não encontrada para este usuário.");

            if (orcamento.Mes < 1 || orcamento.Mes > 12)
                throw new Exception("Mês inválido.");

            if (orcamento.Ano < 2000 || orcamento.Ano > DateTime.Now.Year + 6)
                throw new Exception("Ano inválido.");

            if (orcamento.ValorLimite <= 0)
                throw new Exception("O valor limite deve ser maior que zero.");
            if (orcamentoDuplicado == true)
                throw new Exception("Já existe um orçamento para esta categoria no mês e ano informados.");

            orcamento.UsuarioId = usuarioId;

            return await _orcamentoRepository.CreateAsync(orcamento);
        }

    }
}