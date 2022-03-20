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
using System;

namespace Gsl.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de fornecedor
    /// </summary>
    public class FornecedorApplication : IFornecedorApplication
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository _fornecedorRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="fornecedorRepository"></param>
        public FornecedorApplication(IMapper mapper, 
                                     IFornecedorRepository fornecedorRepository)
        {
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de fornecedores 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<FornecedorModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaFornecedores = await _fornecedorRepository.ListarTodos(ctx);

            return Result<List<FornecedorModel>>.Ok(_mapper.Map<List<FornecedorModel>>(listaFornecedores));
        }

        /// <summary>
        /// Obtem dados de um fornecedor
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<FornecedorModel>> ObterFornecedor(string cnpj, CancellationToken ctx)
        {
            var output = new FornecedorModel();

            if (!string.IsNullOrEmpty(cnpj))
            {
                var fornecedor = await _fornecedorRepository.ObterPorCnpj(cnpj, ctx);
                if (fornecedor == null)
                {
                    var notification = new List<Notification> { new Notification(nameof(Fornecedor.Cnpj), MensagensInfo.Fornecedor_NaoEncontrado) };
                    return Result<FornecedorModel>.Error(notification);
                }

                output = _mapper.Map<Fornecedor, FornecedorModel>(fornecedor);
            }

            return Result<FornecedorModel>.Ok(output);
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um fornecedor
        /// </summary>
        /// <param name="fornecedorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Fornecedor>> CadastrarFornecedor(FornecedorModel fornecedorModel, CancellationToken ctx)
        {
            var fornecedor = _mapper.Map<FornecedorModel, Fornecedor>(fornecedorModel);

            if (fornecedor.Valid)
            {
                if (!await _fornecedorRepository.VerificarSeExiste(fornecedor, ctx))
                {
                    await _fornecedorRepository.Salvar(fornecedor, ctx);
                    return Result<Fornecedor>.Ok(fornecedor);
                }

                fornecedor.AddNotification(nameof(Fornecedor.Cnpj), MensagensInfo.Fornecedor_CpnjExiste);
            }

            return Result<Fornecedor>.Error(fornecedor.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados do fornecedor
        /// </summary>
        /// <param name="fornecedorModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Fornecedor>> AtualizarFornecedor(FornecedorModel fornecedorModel, CancellationToken ctx)
        {
            var fornecedor = _mapper.Map<FornecedorModel, Fornecedor>(fornecedorModel);

            if (fornecedor.Valid)
            {
                if (await _fornecedorRepository.VerificarSeExiste(fornecedor, ctx))
                {
                    await _fornecedorRepository.Atualizar(fornecedor, ctx);
                    return Result<Fornecedor>.Ok(fornecedor);
                }

                fornecedor.AddNotification(nameof(Fornecedor.Cnpj), MensagensInfo.Fornecedor_NaoEncontrado);
            }

            return Result<Fornecedor>.Error(fornecedor.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta um fornecedor pelo CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Fornecedor>> DeletarFornecedor(string cnpj, CancellationToken ctx)
        {
            try
            {
                await _fornecedorRepository.Deletar(cnpj, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Fornecedor.Cnpj), MensagensInfo.Fornecedor_ErroDeletar) };
                return Result<Fornecedor>.Error(notification);
            }

            return Result<Fornecedor>.Ok(new Fornecedor());
        }

        #endregion

    }
}
