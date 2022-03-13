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
    /// Repositório de fornecedores
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FornecedorRepository : RedisRepository<Fornecedor>, IFornecedorRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public FornecedorRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Fornecedor model) => ObterChave(model.Cnpj);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static string ObterChave(string cpf) => $"fornecedor:{cpf}";

        /// <summary>
        /// Armazena o cliente no banco de dados
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Fornecedor fornecedor, CancellationToken ctx)
        {
            await Add(fornecedor, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o fornecedor pelo cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Fornecedor> ObterPorCnpj(string cnpj, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(cnpj), ctx);
        }

        /// <summary>
        /// Verifica se o fornecedor já existe no banco
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Fornecedor fornecedor, CancellationToken ctx)
        {
            var fornecedorExistente = await ObterPorCnpj(fornecedor.Cnpj, ctx);

            return fornecedorExistente?.Cnpj == fornecedor.Cnpj;
        }
        
        /// <summary>
        /// Obtém lista de veiculos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fornecedor>> ListarTodos(CancellationToken ctx)
        {
            var listaChaves = GetServer().Keys(pattern: "fornecedor:*").ToList();
            var listaFornecedore = new List<Fornecedor>();

            foreach (var chave in listaChaves)
            {
                var fornecedor = await GetByKey(chave, ctx);
                if (fornecedor != default)
                    listaFornecedore.Add(fornecedor);
            }

            return listaFornecedore;
        }
    }    
}