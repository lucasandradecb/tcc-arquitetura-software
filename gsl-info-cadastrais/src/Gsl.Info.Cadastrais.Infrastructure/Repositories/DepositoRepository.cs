using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using Gsl.Info.Cadastrais.Domain.ValueObjects;

namespace Gsl.Info.Cadastrais.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório de depositos
    /// </summary>
    public class DepositoRepository : IDepositoRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public DepositoRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public async Task<List<Deposito>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                  $@"SELECT 
                	id,
					codigo,
					nome,
					latitude,
					longitude,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado
                FROM Deposito
                ORDER BY codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert);
            var listaDepositos = new List<Deposito>();

            foreach (var item in listaDinamica.ToList())            
                listaDepositos.Add(ConverterSelectToDeposito(item));            

            return listaDepositos;
        }

        public async Task<Deposito> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                	id,
					codigo,
					nome,
					latitude,
					longitude,
                    cep,
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    estado
                FROM Deposito
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            var depositoDinamico = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlInsert, new { codigo });
            return depositoDinamico != null ? ConverterSelectToDeposito(depositoDinamico) : depositoDinamico;
        }

        public async Task Salvar(Deposito deposito, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Deposito
					(id,
					codigo,
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
					@Codigo,
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
            parameters.Add("@Id", deposito.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", deposito.Codigo, System.Data.DbType.Int64);
            parameters.Add("@Nome", deposito.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Latitude", deposito.Latitude, System.Data.DbType.Decimal);
            parameters.Add("@Longitude", deposito.Longitude, System.Data.DbType.Decimal);
            parameters.Add("@Cep", deposito.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", deposito.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(deposito.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", deposito.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", deposito.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", deposito.Endereco.Estado, System.Data.DbType.AnsiString);
            parameters.Add("@DataCriacao", deposito.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<bool> VerificarSeExiste(Deposito deposito, CancellationToken ctx)
        {
            var depositoExistente = await ObterPorCodigo(deposito.Codigo, ctx);

            return depositoExistente?.Codigo == deposito.Codigo;
        }

        public async Task Atualizar(Deposito deposito, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"UPDATE Deposito SET
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
                WHERE codigo = @Codigo";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", deposito.Codigo, System.Data.DbType.AnsiString);
            parameters.Add("@Nome", deposito.Nome, System.Data.DbType.AnsiString);
            parameters.Add("@Latitude", deposito.Latitude, System.Data.DbType.Decimal);
            parameters.Add("@Longitude", deposito.Longitude, System.Data.DbType.Decimal);
            parameters.Add("@Cep", deposito.Endereco.Cep, System.Data.DbType.AnsiString);
            parameters.Add("@Logradouro", deposito.Endereco.Logradouro, System.Data.DbType.AnsiString);
            parameters.Add("@Numero", Int64.Parse(deposito.Endereco.Numero), System.Data.DbType.Int64);
            parameters.Add("@Complemento", deposito.Endereco.Complemento, System.Data.DbType.AnsiString);
            parameters.Add("@Cidade", deposito.Endereco.Cidade, System.Data.DbType.AnsiString);
            parameters.Add("@Estado", deposito.Endereco.Estado, System.Data.DbType.AnsiString);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
              $@"DELETE FROM Deposito
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        #region Métodos privados

        private Deposito ConverterSelectToDeposito (dynamic select)
        {
            var endereco = new EnderecoCompleto(select.cep, select.logradouro, select.numero.ToString(), select?.complemento, select.cidade, select.estado);
            int codigo = select.codigo;
            double latitude = Convert.ToDouble(select.latitude);
            double longitude = Convert.ToDouble(select.longitude);
            string nome = select.nome;

            return new Deposito(
                        nome,
                        codigo,
                        endereco,
                        latitude,
                        longitude
                    );
        }

        #endregion
    }
}