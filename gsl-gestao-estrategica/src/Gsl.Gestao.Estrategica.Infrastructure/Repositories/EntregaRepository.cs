using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

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

        public async Task Salvar(Entrega entrega, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Entrega
					(id,
					codigo,
					pedidoid,
					latitude,
                    longitude,
                    status,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@PedidoId,
					@Latitude,
                    @Longitude,
                    @Status,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", entrega.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", entrega.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@PedidoId", entrega.PedidoId, System.Data.DbType.Guid);
            parameters.Add("@Latitude", entrega.LatitudeEntrega, System.Data.DbType.Double);
            parameters.Add("@Longitude", entrega.LongitudeEntrega, System.Data.DbType.Double);
            parameters.Add("@Status", entrega.StatusEntrega, System.Data.DbType.Int32);
            parameters.Add("@DataCriacao", entrega.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);            
        }

        public async Task<Entrega> ObterPorCodigo(string codigo, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					pedidoid,
					latitude AS LatitudeEntrega,
                    longitude AS LongitudeEntrega,
                    status AS StatusEntrega,
					datacriacao,
                    dataatualizacao
                FROM Entrega
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Entrega>(sqlInsert, new { codigo });
        }

        public async Task<List<Entrega>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					codigo,
					pedidoid,
					latitude AS LatitudeEntrega,
                    longitude AS LongitudeEntrega,
                    status AS StatusEntrega,
					datacriacao,
                    dataatualizacao
                FROM Entrega
                ORDER BY codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<Entrega>(sqlInsert);
            return lista.ToList();
        }

        public async Task Atualizar(Entrega entrega, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE Entrega SET
					latitude = @Latitude,
                    longitude = @Longitude,
                    status = @Status
                 WHERE codigo = @Codigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", entrega.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@Latitude", entrega.LatitudeEntrega, System.Data.DbType.Double);
            parameters.Add("@Longitude", entrega.LongitudeEntrega, System.Data.DbType.Double);
            parameters.Add("@Status", entrega.StatusEntrega, System.Data.DbType.Int32);

            await connection.ExecuteAsync(sqlInsert, parameters);
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