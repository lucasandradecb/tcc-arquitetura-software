using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de mercadorias
    /// </summary>
    public class MercadoriaRepository : IMercadoriaRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public MercadoriaRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }
        public async Task Salvar(Mercadoria mercadoria, CancellationToken ctx)
        {
            var sqlInsert =
                @"INSERT INTO Mercadoria
					(id,
					codigo,
					nome,
					quantidade,
					valor,
                    fornecedorId,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@Nome,
					@Quantidade,
					@Valor,
                    @FornecedorId,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, mercadoria);
        }

        public async Task<Mercadoria> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					nome,
					quantidade,
					valor,
                    fornecedorId,
					datacriacao,
                    dataatualizacao
                FROM Mercadoria
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Mercadoria>(sqlInsert, new { codigo });
        }

        public async Task<bool> VerificarSeExiste(Mercadoria mercadoria, CancellationToken ctx)
        {
            var mercadoriaExistente = await ObterPorCodigo(mercadoria.Codigo, ctx);

            return mercadoriaExistente?.Codigo == mercadoria.Codigo;
        }

        public async Task<List<Mercadoria>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					nome,
					quantidade,
					valor,
                    fornecedorId,
					datacriacao,
                    dataatualizacao
                FROM Mercadoria
                ORDER BY codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<Mercadoria>(sqlInsert);
            return lista.ToList();
        }

        public async Task Atualizar(Mercadoria mercadoria, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE Mercadoria SET
                  nome = @{ nameof(mercadoria.Nome)},
				  quantidade = @{ nameof(mercadoria.Quantidade)},
                  valor = @{ nameof(mercadoria.Valor)},
                  dataatualizacao = GETDATE()     
                WHERE codigo = @{nameof(mercadoria.Codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, mercadoria);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
               $@"DELETE FROM Mercadoria
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }
    }    
}