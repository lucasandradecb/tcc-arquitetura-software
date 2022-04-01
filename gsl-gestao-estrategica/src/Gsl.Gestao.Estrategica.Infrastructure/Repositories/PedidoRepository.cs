using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Dapper;

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

        public async Task Salvar(Pedido pedido, CancellationToken ctx)
        {
            var sqlInsert =
                $@"INSERT INTO Pedido
					(id,
					codigo,
					clientecpf,
					valortotal,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@ClienteCpf,
					@ValorTotal,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", pedido.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", pedido.Codigo, System.Data.DbType.Int32);
            parameters.Add("@ClienteCpf", pedido.ClienteCpf, System.Data.DbType.AnsiString);
            parameters.Add("@ValorTotal", pedido.ValorTotal, System.Data.DbType.Double);
            parameters.Add("@DataCriacao", pedido.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);

            foreach (var item in pedido.ItensPedido)
            {
                item.PedidoId = pedido.Id;
                await SalvarItem(item, ctx);
            }
        }

        public async Task<Pedido> ObterPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                $@"SELECT 
                      Pedido.Id AS Id,
                      Pedido.codigo AS PedidoCodigo,
                      Pedido.clientecpf,
                      Pedido.valortotal,
                      ItemPedido.mercadoriacodigo,
                      ItemPedido.mercadoriaquantidade,
	                  ItemPedido.valor,
                      ItemPedido.codigo AS ItemCodigo
                  FROM [db_gestao_estrategica].[dbo].[Pedido]
                  inner join ItemPedido on pedidoid = Pedido.id 
                  where Pedido.codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert, new { codigo });

            return ConverterSelectToPedido(listaDinamica).FirstOrDefault();
        }

        public async Task<List<Pedido>> ListarTodos(CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                      Pedido.Id AS Id,
                	  Pedido.codigo AS PedidoCodigo,
                      Pedido.clientecpf,
                      Pedido.valortotal,
                      ItemPedido.mercadoriacodigo,
                      ItemPedido.mercadoriaquantidade,
	                  ItemPedido.valor,
                      ItemPedido.codigo AS ItemCodigo
                  FROM [db_gestao_estrategica].[dbo].[Pedido]
                  inner join ItemPedido on pedidoid = Pedido.id 
                  ORDER BY Pedido.codigo DESC";

            using var connection = SqlServerDbContext.GetConnection();

            var listaDinamica = await connection.QueryAsync<dynamic>(sqlInsert);

            return ConverterSelectToPedido(listaDinamica);
        }

        public async Task Atualizar(Pedido pedido, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE Pedido SET
                  valortotal = @ValorTotal,
                  dataatualizacao = GETDATE()     
                WHERE codigo = @Codigo";            

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", pedido.Codigo, System.Data.DbType.Int32);
            parameters.Add("@ValorTotal", pedido.ValorTotal, System.Data.DbType.Double);

            await connection.ExecuteAsync(sqlInsert, parameters);

            foreach (var item in pedido.ItensPedido)
            {
                item.PedidoId = pedido.Id;

                if (await ObterItemPorCodigo(item.Codigo, ctx) == null)
                    await SalvarItem(item, ctx);
                else
                    await AtualizarItem(item, ctx);
            }
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

        public async Task SalvarItem(ItemPedido itemPedido, CancellationToken ctx)
        {
            var sqlInsert =
               $@"INSERT INTO ItemPedido
					(id,
					codigo,
					mercadoriacodigo,
					mercadoriaquantidade,
                    valor,
                    pedidoid,
					datacriacao)
				VALUES 
					(@Id,
					@Codigo,
					@MercadoriaCodigo,
                    @MercadoriaQuantidade,
					@Valor,
                    @PedidoId,
					@DataCriacao)";

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", itemPedido.Id, System.Data.DbType.Guid);
            parameters.Add("@Codigo", itemPedido.Codigo, System.Data.DbType.Int32);
            parameters.Add("@MercadoriaCodigo", itemPedido.MercadoriaCodigo, System.Data.DbType.Int32);
            parameters.Add("@MercadoriaQuantidade", itemPedido.MercadoriaQuantidade, System.Data.DbType.Int32);
            parameters.Add("@Valor", itemPedido.Valor, System.Data.DbType.Double);
            parameters.Add("@PedidoId", itemPedido.PedidoId, System.Data.DbType.Guid);
            parameters.Add("@DataCriacao", itemPedido.DataCriacao, System.Data.DbType.DateTime);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task AtualizarItem(ItemPedido itemPedido, CancellationToken ctx)
        {
            var sqlInsert =
                $@"UPDATE ItemPedido SET
                  mercadoriaquantidade = @MercadoriaQuantidade,
                  valor = @Valor,
                  dataatualizacao = GETDATE()     
                WHERE codigo = @Codigo AND mercadoriacodigo = @MercadoriaCodigo";
           

            using var connection = SqlServerDbContext.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Codigo", itemPedido.Codigo, System.Data.DbType.Int32);
            parameters.Add("@MercadoriaCodigo", itemPedido.MercadoriaCodigo, System.Data.DbType.Int32);
            parameters.Add("@MercadoriaQuantidade", itemPedido.MercadoriaQuantidade, System.Data.DbType.Int32);
            parameters.Add("@Valor", itemPedido.Valor, System.Data.DbType.Double);

            await connection.ExecuteAsync(sqlInsert, parameters);
        }

        public async Task<ItemPedido> ObterItemPorCodigo(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
                 $@"SELECT 
                	id,
					codigo,
                    pedidoid
                FROM ItemPedido
                WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<ItemPedido>(sqlInsert, new { codigo });             
        }

        public async Task DeletarItem(int codigo, CancellationToken ctx)
        {
            var sqlInsert =
             $@"DELETE FROM ItemPedido
				 WHERE codigo = @{nameof(codigo)}";

            using var connection = SqlServerDbContext.GetConnection();

            await connection.ExecuteAsync(sqlInsert, new { codigo });
        }

        private List<Pedido> ConverterSelectToPedido(IEnumerable<dynamic> listaDinamica)
        {            
            var lista = new List<Pedido>();

             var results = listaDinamica
            .GroupBy(p => p.Id)
            .Where(x => x.Count() > 1)
            .Select(x => new List<dynamic>(x)).ToList();

            foreach (var itemGroup in results)
            {
                var pedido = new Pedido();
                var listaItems = new List<ItemPedido>();
                foreach (var item in itemGroup.ToList())
                {
                    listaItems.Add(new ItemPedido(item.ItemCodigo, item.mercadoriacodigo, item.mercadoriaquantidade, Convert.ToDouble(item.valor)));
                                       
                    pedido.Id = item.Id;
                    pedido.Codigo = item.ItemCodigo;
                    pedido.ClienteCpf = item.clientecpf;
                    pedido.ValorTotal = Convert.ToDouble(item.valortotal);
                    pedido.ItensPedido = listaItems;
                }

                lista.Add(pedido);
            }

            

            return lista;
        }
    }
}