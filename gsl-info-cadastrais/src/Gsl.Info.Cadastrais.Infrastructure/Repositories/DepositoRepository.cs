﻿using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Dapper;

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

        public Task<List<Deposito>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<Deposito> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
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

        public Task<bool> VerificarSeExiste(Deposito deposito, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Deposito deposito, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }       
    }    
}