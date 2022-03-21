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

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert);
            var listaFornecedores = new List<Fornecedor>();

            foreach (var item in listaDinamica.ToList())
                listaFornecedores.Add(ConverterSelectToFornecedor(item));

            return listaFornecedores;
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

            var fornecedorDinamico = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlInsert, new { cnpj });
            return fornecedorDinamico != null ? ConverterSelectToFornecedor(fornecedorDinamico) : fornecedorDinamico;
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

        #region Métodos privados

        private Fornecedor ConverterSelectToFornecedor(dynamic select)
        {
            var endereco = new EnderecoCompleto(select.cep, select.logradouro, select.numero.ToString(), select?.complemento, select.cidade, select.estado);
            string cnpj = select.cnpj;
            double latitude = Convert.ToDouble(select.latitude);
            double longitude = Convert.ToDouble(select.longitude);
            string nome = select.nome;

            return new Fornecedor(
                        nome,
                        cnpj,
                        endereco,
                        latitude,
                        longitude
                    );
        }

        #endregion
    }
}