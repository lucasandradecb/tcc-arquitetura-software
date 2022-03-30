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
    /// Repositório de pedidos
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private ISqlServerDbContext SqlServerDbContext { get; }

        public PedidoRepository(ISqlServerDbContext sqlServerDbContext)
        {
            SqlServerDbContext = sqlServerDbContext;
        }

        public Task Salvar(Pedido pedido, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pedido>> ListarTodos(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Pedido pedido, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task Deletar(Pedido pedido, CancellationToken ctx)
        {
            foreach (var item in pedido.ItensPedido)
                await DeletarItem(item.Codigo, ctx);

            var sqlInsert =
             $@"DELETE FROM Pedido
				 WHERE codigo = @{nameof(pedido.Codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { pedido.Codigo });
        }
        public async Task<bool> VerificarSeExiste(Pedido pedido, CancellationToken ctx)
        {
            var pedidoExistente = await ObterPorCodigo(pedido.Codigo, ctx);

            return pedidoExistente?.Codigo == pedido.Codigo;
        }

        public Task SalvarItem(ItemPedido itemPedido, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public async Task DeletarItem(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
             $@"DELETE FROM ItemPedido
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }
    }
}