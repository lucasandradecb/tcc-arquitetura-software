using Gsl.Gestao.Estrategica.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces
{
    /// <summary>
    /// Interface do gateway de informações cadastrais
    /// </summary>
    public interface IGslInfoCadastraisGateway
    {
        /// <summary>
        /// Obtém a mercadoria por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<MercadoriaGatewayModel> ObterMercadoria(int codigo, CancellationToken ctx);
        /// <summary>
        /// Atualiza uma mercadoria
        /// </summary>
        /// <returns></returns>
        /// <param name="mercadoriaGateway"></param>
        /// <param name="ctx"></param>
        Task AtualizarMercadoria(MercadoriaGatewayModel mercadoriaGateway, CancellationToken ctx);
        /// <summary>
        /// Obtém o deposito por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<DepositoGatewayModel> ObterDeposito(int codigo, CancellationToken ctx);
        /// <summary>
        /// Obtém o cliente pelo cpf
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<ClienteGatewayModel> ObterCliente(string cpf, CancellationToken ctx);
        /// <summary>
        /// Obtém o forncedor pelo cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<FornecedorGatewayModel> ObterFornecedor(string cnpj, CancellationToken ctx);
    }
}
