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
    /// Repositório de estoques
    /// </summary>
    public class EstoqueRepository : IEstoqueRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public EstoqueRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task<bool> VerificarSeExiste(Estoque estoque, CancellationToken ctx)
        {
            var estoqueExistente = await ObterPorCodigo(estoque.Codigo, ctx);

            return estoqueExistente?.Codigo == estoque.Codigo;
        }

        public async Task Salvar(Estoque estoque, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Estoque
					(id,
					codigo,
					depositocodigo,
					mercadoriacodigo,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@DepositoCodigo,
					@MercadoriaCodigo,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();            
            parameters.Add("@Codigo", estoque.Codigo, System.Data.DbType.Int32);
            parameters.Add("@DepositoCodigo", estoque.DepositoCodigo, System.Data.DbType.Int32);            
            parameters.Add("@DataCriacao", estoque.DataCriacao, System.Data.DbType.DateTime);

            foreach (var item in estoque.ListaMercadorias)
            {
                parameters.Add("@Id", Guid.NewGuid(), System.Data.DbType.Guid);
                parameters.Add("@MercadoriaCodigo", item.Codigo, System.Data.DbType.Int32);
                await connection.ExecuteAsync(sqlInsert, parameters);
            }            
        }

        public async Task<Estoque> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					depositocodigo AS DepositoCodigo,
					mercadoriacodigo AS MercadoriaCodigo,
					datacriacao,
                    dataatualizacao
                FROM Estoque
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert, new { codigo });

            return ConverterSelectToEstoque(listaDinamica).FirstOrDefault();
        }

        public async Task<List<Estoque>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
					depositocodigo AS DepositoCodigo,
					mercadoriacodigo AS MercadoriaCodigo,
					datacriacao,
                    dataatualizacao
                FROM Estoque
                ORDER BY codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert);
                      
            return ConverterSelectToEstoque(listaDinamica);
        }

        public async Task Atualizar(Estoque estoque, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE Estoque SET
                  depositocodigo = @DepositoCodigo,				  
                  dataatualizacao = GETDATE()     
                WHERE codigo = @Codigo AND mercadoriacodigo = @MercadoriaCodigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", estoque.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", estoque.Codigo, System.Data.DbType.Int32);
            parameters.Add("@DepositoCodigo", estoque.DepositoCodigo, System.Data.DbType.Int32);

            foreach (var item in estoque.ListaMercadorias)
            {
                parameters.Add("@MercadoriaCodigo", item.Codigo, System.Data.DbType.Int32);
                await connection.ExecuteAsync(sqlInsert, parameters);
            }
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM Estoque
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        private List<Estoque> ConverterSelectToEstoque(IEnumerable<dynamic> listaDinamica)
        {
            var listaMercadorias = new List<Mercadoria>();
            var lista = new List<Estoque>();

            foreach (var item in listaDinamica.ToList())
            {
                listaMercadorias.Add(new Mercadoria() { Codigo = item.MercadoriaCodigo });
                lista.Add(new Estoque(item.codigo, item.DepositoCodigo, listaMercadorias));
            }

            return lista;
        }
    }
}