using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using Localiza.BuildingBlocks.Redis;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de usuarios
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UsuarioRepository : RedisRepository<Usuario>, IUsuarioRepository
    {
        private TimeSpan REDIS_TEMPO_EXPIRACAO = new TimeSpan(10, 0, 0, 0);

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="config"></param>
        public UsuarioRepository(RedisConfiguration config) : base(config)
        {
            
        }

        /// <summary>
        /// Cria uma chave de acesso ao redis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override string CreateRedisKey(Usuario model) => ObterChave(model.Login);

        /// <summary>
        /// Obtém chave de acesso do redis
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static string ObterChave(string login) => $"usuario:{login}";

        /// <summary>
        /// Armazena o usuario no banco de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task Salvar(Usuario usuario, CancellationToken ctx)
        {
            await Add(usuario, ctx, REDIS_TEMPO_EXPIRACAO);
        }

        /// <summary>
        /// Obtém o usuario pelo login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Usuario> ObterPorLogin(string login, CancellationToken ctx)
        {
            return await GetByKey(ObterChave(login), ctx);
        }

        /// <summary>
        /// Verifica se o usuario já existe no banco
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<bool> VerificarSeExiste(Usuario usuario, CancellationToken ctx)
        {
            var usuarioExistente = await ObterPorLogin(usuario.Login, ctx);

            return usuarioExistente?.Login == usuario.Login;
        }
    }    
}