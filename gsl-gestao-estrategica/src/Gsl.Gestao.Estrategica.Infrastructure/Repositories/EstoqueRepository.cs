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

        public Task Salvar(Estoque estoque, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
        public async Task<Estoque> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Estoque>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(Estoque estoque, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task Deletar(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarSeExiste(Estoque estoque, CancellationToken ctx)
        {
            var estoqueExistente = await ObterPorCodigo(estoque.Codigo, ctx);

            return estoqueExistente?.Codigo == estoque.Codigo;
        }
    }
}