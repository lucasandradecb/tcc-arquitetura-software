using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Gsl.Gestao.Estrategica.Domain.ValueObjects;

namespace Gsl.Gestao.Estrategica.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de entregas
    /// </summary>
    public class EntregaRepository : IEntregaRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public EntregaRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }       

        public async Task<bool> VerificarSeExiste(Entrega entrega, CancellationToken ctx)
        {
            var entregaExistente = await ObterPorCodigo(entrega.Codigo, ctx);

            return entregaExistente?.Codigo == entrega.Codigo;
        }

        public Task Salvar(Entrega entrega, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<Entrega> ObterPorCodigo(string codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entrega>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Entrega entrega, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task Deletar(string codigo, CancellationToken ctx)
        {
            var sqlInsert =
             $@"DELETE FROM Entrega
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }
    }
}