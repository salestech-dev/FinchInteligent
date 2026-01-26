using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Enums;
using finchInteligent.Models;
using finchInteligent.Repository.Interfaces;

namespace finchInteligent.Service
{
    public class TransacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;


        public TransacaoService(IUsuarioRepository usuarioRepository, IOrcamentoRepository orcamentoRepository, ICategoriaRepository categoriaRepository, ITransacaoRepository transacaoRepository, IContaRepository contaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
            _categoriaRepository = categoriaRepository;
            _orcamentoRepository = orcamentoRepository;
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao, string usuarioId)
        {
            var usuarioExists = await _usuarioRepository.ExistsAsync(usuarioId);
            if (!usuarioExists)
                throw new Exception("Usuário não encontrado.");

            var conta = await _contaRepository.GetByIdAndUsuarioAsync(transacao.ContaId, usuarioId);
            if (conta == null)
                throw new Exception("Conta não encontrada para o usuário.");

            var categoria = await _categoriaRepository.GetByIdAndUsuarioAsync(transacao.CategoriaId, usuarioId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada para o usuário.");

            if (transacao.Valor == 0)
                throw new Exception("O valor da transação não pode ser zero.");

            // 🔹 CARTÃO DE CRÉDITO
            if (conta.Tipo == TipoDeConta.CartaoCredito)
            {
                if (transacao.Tipo != TipoTransacao.Saida)
                    throw new Exception("Cartão de crédito só permite transações do tipo Saída.");

                if (transacao.Valor > 0)
                    throw new Exception("Transações em cartão de crédito devem ser negativas.");
            }
            else
            {
                // 🔹 CONTAS NORMAIS
                if (transacao.Tipo == TipoTransacao.Entrada && transacao.Valor < 0)
                    throw new Exception("Entrada não pode ter valor negativo.");

                if (transacao.Tipo == TipoTransacao.Saida && transacao.Valor > 0)
                    throw new Exception("Saída não pode ter valor positivo.");

                // 🔥 ESTOURO DE ORÇAMENTO
                if (transacao.Tipo == TipoTransacao.Saida)
                {
                    var mes = transacao.Data.Month;
                    var ano = transacao.Data.Year;

                    var orcamento = await _orcamentoRepository
                        .GetByCategoriaMesAnoAsync(
                            transacao.CategoriaId,
                            mes,
                            ano,
                            usuarioId
                        );

                    if (orcamento != null)
                    {
                        var totalGasto = await _transacaoRepository
                            .GetTotalSaidasByCategoriaMesAsync(
                                transacao.CategoriaId,
                                mes,
                                ano,
                                usuarioId
                            );

                        var novoTotal = totalGasto + Math.Abs(transacao.Valor);

                        if (novoTotal > orcamento.ValorLimite)
                            throw new Exception("Orçamento estourado.");
                    }
                }

                // Atualiza saldo
                conta.Saldo += transacao.Valor;
                await _contaRepository.UpdateAsync(conta);
            }

            // 🔹 Vinculações finais
            transacao.UsuarioId = usuarioId;
            transacao.Data = DateTime.Now;

            return await _transacaoRepository.CreateAsync(transacao);
        }

    }
}
