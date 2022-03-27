using Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces;
using Gsl.Gestao.Estrategica.Infrastructure.Helpers;
using Gsl.Gestao.Estrategica.Infrastructure.Models;
using Gsl.Gestao.Estrategica.Infrastructure.Models.RestRequest;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Gsl.Gestao.Estrategica.Infrastructure.Gateways
{
    public class GslInfoCadastraisGateway : IGslInfoCadastraisGateway
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="httpClient"></param>
        public GslInfoCadastraisGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Atualiza as informações de uma mercadoria
        /// </summary>
        /// <param name="mercadoriaGateway"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task AtualizarMercadoria(MercadoriaGatewayModel mercadoriaGateway, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/mercadorias";
            
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = new StringContent(JsonSerializer.Serialize(mercadoriaGateway), Encoding.UTF8, "application/json");

            await _httpClient.SendAsync(request, ctx);
        }

        /// <summary>
        /// Obtem os dados de um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<ClienteGatewayModel> ObterCliente(string cpf, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/clientes/{cpf}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<ClienteGatewayModel, GatewayErroModel>(result);
        }

        /// <summary>
        /// Obtém dados de um deposito
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<DepositoGatewayModel> ObterDeposito(int codigo, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/depositos/{codigo}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<DepositoGatewayModel, GatewayErroModel>(result);
        }

        /// <summary>
        /// Obtém dados de um fornecedor
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<FornecedorGatewayModel> ObterFornecedor(string cnpj, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/fornecedores/{cnpj}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<FornecedorGatewayModel, GatewayErroModel>(result);
        }

        /// <summary>
        /// Obtém dados de uma mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<MercadoriaGatewayModel> ObterMercadoria(int codigo, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/mercadorias/{codigo}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<MercadoriaGatewayModel, GatewayErroModel>(result);
        }
    }
}
