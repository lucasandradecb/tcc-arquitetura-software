using Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces;
using Gsl.Gestao.Estrategica.Infrastructure.Helpers;
using Gsl.Gestao.Estrategica.Infrastructure.Models;
using Gsl.Gestao.Estrategica.Infrastructure.Models.RestRequest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public Task AtualizarMercadoria(MercadoriaGatewayModel mercadoriaGateway, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtem os dados de um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<ClienteGatewayModel> ObterCliente(string cpf, CancellationToken ctx)
        {
            var url = _httpClient.BaseAddress.AbsoluteUri + $"v1/contratos/{cpf}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(request, ctx);

            return await RestDeserializeBase.Deserialize<ClienteGatewayModel, GatewayErroModel>(result);
        }

        public Task<DepositoGatewayModel> ObterDeposito(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<FornecedorGatewayModel> ObterFornecedor(string cnpj, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<MercadoriaGatewayModel> ObterMercadoria(int codigo, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }
}
