using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gsl.Info.Cadastrais.Application.Interfaces;
using Gsl.Info.Cadastrais.Application.Models;
using Gsl.Info.Cadastrais.Domain.Entities;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Domain.Resources;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de mercadoria
    /// </summary>
    public class MercadoriaApplication : IMercadoriaApplication
    {
        private readonly IMapper _mapper;
        private readonly IMercadoriaRepository _mercadoriaRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="mercadoriaRepository"></param>
        public MercadoriaApplication(IMapper mapper,
                                     IMercadoriaRepository mercadoriaRepository)
        {
            _mapper = mapper;
            _mercadoriaRepository = mercadoriaRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de mercadorias 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<MercadoriaModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaMercadorias = await _mercadoriaRepository.ListarTodos(ctx);

            return Result<List<MercadoriaModel>>.Ok(_mapper.Map<List<MercadoriaModel>>(listaMercadorias));
        }

        /// <summary>
        /// Obtem dados de uma mercadoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<MercadoriaModel>> ObterMercadoria(int codigo, CancellationToken ctx)
        {
            var output = new MercadoriaModel();

            var mercadoria = await _mercadoriaRepository.ObterPorCodigo(codigo, ctx);
            if (mercadoria == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Mercadoria.Codigo), MensagensInfo.Mercadoria_NaoEncontrada) };
                return Result<MercadoriaModel>.Error(notification);
            }

            output = _mapper.Map<Mercadoria, MercadoriaModel>(mercadoria);            

            return Result<MercadoriaModel>.Ok(output);
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de uma mercadoria
        /// </summary>
        /// <param name="mercadoriaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Mercadoria>> CadastrarMercadoria(MercadoriaModel mercadoriaModel, CancellationToken ctx)
        {
            var mercadoria = _mapper.Map<MercadoriaModel, Mercadoria>(mercadoriaModel);

            if (mercadoria.Valid)
            {
                if (!await _mercadoriaRepository.VerificarSeExiste(mercadoria, ctx))
                {
                    await _mercadoriaRepository.Salvar(mercadoria, ctx);
                    return Result<Mercadoria>.Ok(mercadoria);
                }

                mercadoria.AddNotification(nameof(Mercadoria.Codigo), MensagensInfo.Mercadoria_CodigoExiste);
            }

            return Result<Mercadoria>.Error(mercadoria.Notifications);
        }

        #endregion

    }
}
