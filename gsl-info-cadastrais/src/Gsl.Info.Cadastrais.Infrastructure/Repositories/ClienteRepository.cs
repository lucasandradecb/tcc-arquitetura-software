using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using Localiza.BuildingBlocks.Redis;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de clientes
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ClienteRepository : RedisRepository<Cliente>, IClienteRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public ClienteRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Cliente model) => ObterChave(model.Cpf.Numero);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static string ObterChave(string cpf) => $"cliente:{cpf}";

        /// <summary>
        /// Armazena o cliente no banco de dados
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Cliente cliente, CancellationToken ctx)
        {
            await Add(cliente, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o cliente pelo cpf
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Cliente> ObterPorCpf(string cpf, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(cpf), ctx);
        }

        /// <summary>
        /// Verifica se o cliente já existe no banco
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Cliente cliente, CancellationToken ctx)
        {
            var clienteExistente = await ObterPorCpf(cliente.Cpf.Numero, ctx);

            return clienteExistente?.Cpf?.Numero == cliente.Cpf.Numero;
        }
        
        /// <summary>
        /// Obtém lista de clientes
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cliente>> ListarTodos(CancellationToken ctx)
        {
            var listaChaves = GetServer().Keys(pattern: "cliente:*").ToList();
            var listaClientes = new List<Cliente>();

            foreach (var chave in listaChaves)
            {
                var cliente = await GetByKey(chave, ctx);
                if (cliente != default)
                    listaClientes.Add(cliente);
            }

            return listaClientes;
        }
    }    
}