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

        public Task Atualizar(Fornecedor fornecedor, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(string cnpj, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fornecedor>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> ObterPorCnpj(string cnpj, CancellationToken ctx)
        {
            throw new NotImplementedException();
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

        public Task<bool> VerificarSeExiste(Fornecedor fornecedor, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }
}