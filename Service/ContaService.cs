using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Data;
using finchInteligent.Models;
using finchInteligent.Repository.Implementations;
using finchInteligent.Repository.Interfaces;

namespace finchInteligent.Service
{
    public class ContaService
    {

        private readonly IUsuarioRepository _usuariorepo;
        private readonly IContaRepository contaRepository;
        public ContaService(IUsuarioRepository usuarioRepository, IContaRepository contaRepository)
        {

            this._usuariorepo = usuarioRepository;
            this.contaRepository = contaRepository;
            
        }

        public async Task<Conta> CreateAccount(Conta conta)
        {
           var userExists = await _usuariorepo.ExistsAsync(conta.UsuarioId);
            if (userExists == false)
            {
                throw new Exception("Usuário não encontrado.");
            }
            if (conta.SaldoInicial < 0)
            {
                throw new Exception("O saldo inicial não pode ser negativo.");
            }
            if(conta.Nome == null || conta.Nome.Trim() == "")
            {
                throw new Exception("O nome da conta não pode ser vazio.");
            }
            await contaRepository.CreateAsync(conta);
            return conta;
        }

        
    }
}