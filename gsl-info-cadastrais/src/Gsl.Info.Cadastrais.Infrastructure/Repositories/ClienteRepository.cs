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
    /// Repositório de clientes
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public ClienteRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public Task Atualizar(Cliente cliente, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(string cpf, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> ObterPorCpf(string cpf, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task Salvar(Cliente cliente, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Cliente
					(id,
					cpf,
					nome,
					aniversario,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado,
					datacriacao)
				VALUES 
					(@Id,
					@Cpf,
					@Nome,
					@Aniversario,
                    @Cep,
                    @Logradouro,
                    @Numero,
                    @Complemento,
                    @Cidade,
                    @Estado,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", cliente.Id, System.Data.DbType.Guid);
            parameters.Add("@Cpf", cliente.Cpf, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", cliente.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Aniversario", cliente.Aniversario, System.Data.DbType.DateTime);
            parameters.Add("@Cep", cliente.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", cliente.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(cliente.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", cliente.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", cliente.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", cliente.Endereco.Estado, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", cliente.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public Task<bool> VerificarSeExiste(Cliente cliente, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }
}