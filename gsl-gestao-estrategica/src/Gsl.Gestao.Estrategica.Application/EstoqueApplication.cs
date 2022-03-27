using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Gestao.Estrategica.Application.Interfaces;
using Gsl.Gestao.Estrategica.Application.Models;
using Gsl.Gestao.Estrategica.Domain.Entities;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Domain.Resources;
using Gsl.Gestao.Estrategica.Domain.ValueObjects;
using Flunt.Notifications;
using System.Collections.Generic;
using System;
using Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces;

namespace Gsl.Gestao.Estrategica.Application
{
    /// <summary>
    /// Classe de application de estoque
    /// </summary>
    public class EstoqueApplication : IEstoqueApplication
    {
        private readonly IMapper _mapper;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IGslInfoCadastraisGateway _gslInfoCadastraisGateway;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="estoqueRepository"></param>
        /// <param name="gslInfoCadastraisGateway"></param>
        public EstoqueApplication(IMapper mapper,
                                  IEstoqueRepository estoqueRepository,
                                  IGslInfoCadastraisGateway gslInfoCadastraisGateway)
        {
            _mapper = mapper;
            _estoqueRepository = estoqueRepository;
            _gslInfoCadastraisGateway = gslInfoCadastraisGateway;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de estoques 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<EstoqueModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaEstoques = await _estoqueRepository.ListarTodos(ctx);

            return Result<List<EstoqueModel>>.Ok(_mapper.Map<List<EstoqueModel>>(listaEstoques));
        }

        /// <summary>
        /// Obtem dados de um estoque
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<EstoqueModel>> ObterEstoque(int codigo, CancellationToken ctx)
        {
            var output = new EstoqueModel();

            var estoque = await _estoqueRepository.ObterPorCodigo(codigo, ctx);
            if (estoque == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Estoque.Codigo), MensagensInfo.Estoque_NaoEncontrado) };
                return Result<EstoqueModel>.Error(notification);
            }

            output = _mapper.Map<Estoque, EstoqueModel>(estoque);

            return Result<EstoqueModel>.Ok(output);

        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um estoque
        /// </summary>
        /// <param name="estoqueModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Estoque>> CadastrarEstoque(EstoqueModel estoqueModel, CancellationToken ctx)
        {
            var estoque = _mapper.Map<EstoqueModel, Estoque>(estoqueModel);

            if (estoque.Valid)
            {
                if (!await _estoqueRepository.VerificarSeExiste(estoque, ctx))
                {
                    await _estoqueRepository.Salvar(estoque, ctx);
                    return Result<Estoque>.Ok(estoque);
                }

                estoque.AddNotification(nameof(Estoque.Codigo), MensagensInfo.Estoque_CodigoExiste);
            }

            return Result<Estoque>.Error(estoque.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados do estoque
        /// </summary>
        /// <param name="estoqueModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Estoque>> AtualizarEstoque(EstoqueModel estoqueModel, CancellationToken ctx)
        {
            var estoque = _mapper.Map<EstoqueModel, Estoque>(estoqueModel);

            if (estoque.Valid)
            {
                if (await _estoqueRepository.VerificarSeExiste(estoque, ctx))
                {
                    await _estoqueRepository.Atualizar(estoque, ctx);
                    return Result<Estoque>.Ok(estoque);
                }

                estoque.AddNotification(nameof(Estoque.Codigo), MensagensInfo.Estoque_NaoEncontrado);
            }

            return Result<Estoque>.Error(estoque.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta um estoque pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Estoque>> DeletarEstoque(int codigo, CancellationToken ctx)
        {
            try
            {
                await _estoqueRepository.Deletar(codigo, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Estoque.Codigo), MensagensInfo.Estoque_ErroDeletar) };
                return Result<Estoque>.Error(notification);
            }

            return Result<Estoque>.Ok(new Estoque());
        }

        #endregion

    }
}
