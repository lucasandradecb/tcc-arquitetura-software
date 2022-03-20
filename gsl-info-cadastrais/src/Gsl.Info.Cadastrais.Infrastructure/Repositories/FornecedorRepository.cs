using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de fornecedores
    /// </summary>
    public class FornecedorRepository : IFornecedorRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public FornecedorRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task Atualizar(Fornecedor fornecedor, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"UPDATE Fornecedor SET
					nome = @Nome,
					latitude = @Latitude,
					longitude = @Longitude,
                    cep = @Cep,
                    logradouro = @Logradouro,
                    numero = @Numero,
                    complemento = @Complemento,
                    cidade = @Cidade,
                    estado = @Estado,
                  dataatualizacao = GETDATE()     
                WHERE cnpj = @Cnpj";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Cnpj", fornecedor.Cnpj, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", fornecedor.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Latitude", fornecedor.Latitude, System.Data.DbType.Decimal);
            parameters.Add("@Longitude", fornecedor.Longitude, System.Data.DbType.Decimal);
            parameters.Add("@Cep", fornecedor.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", fornecedor.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(fornecedor.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", fornecedor.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", fornecedor.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", fornecedor.Endereco.Estado, System.Data.DbType.AnsiString);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(string cnpj, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM Fornecedor
				 WHERE cnpj = @{nameof(cnpj)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { cnpj });
        }

        public async Task<List<Fornecedor>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					cnpj,
					nome,
					latitude,
					longitude,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao,
                    dataatualizacao
                FROM Fornecedor
                ORDER BY nome";

            using var connection = SqlServerDbContext.GetConnection();

            var lista = await connection.QueryAsync<Fornecedor>(sqlInsert);
            return lista.ToList();
        }

        public async Task<Fornecedor> ObterPorCnpj(string cnpj, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					cnpj,
					nome,
					latitude,
					longitude,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao,
                    dataatualizacao
                FROM Fornecedor
                WHERE cnpj = @{nameof(cnpj)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<Fornecedor>(sqlInsert, new { cnpj });
        }

        public async Task Salvar(Fornecedor fornecedor, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Fornecedor
					(id,
					cnpj,
					nome,
					latitude,
					longitude,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao)
				VALUES 
					(@Id,
					@Cnpj,
					@Nome,
					@Latitude,
					@Longitude,
                    @Cep,
                    @Logradouro,
                    @Numero,
                    @Complemento,
                    @Cidade,
                    @Estado,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", fornecedor.Id, System.Data.DbType.Guid);
            parameters.Add("@Cnpj", fornecedor.Cnpj, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", fornecedor.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Latitude", fornecedor.Latitude, System.Data.DbType.Decimal);
            parameters.Add("@Longitude", fornecedor.Longitude, System.Data.DbType.Decimal);
            parameters.Add("@Cep", fornecedor.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", fornecedor.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(fornecedor.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", fornecedor.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", fornecedor.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", fornecedor.Endereco.Estado, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", fornecedor.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(Fornecedor fornecedor, CancellationToken ctx)
        {
            var fornecedorExistente = await ObterPorCnpj(fornecedor.Cnpj, ctx);

            return fornecedorExistente?.Cnpj == fornecedor.Cnpj;
        }
    }
}