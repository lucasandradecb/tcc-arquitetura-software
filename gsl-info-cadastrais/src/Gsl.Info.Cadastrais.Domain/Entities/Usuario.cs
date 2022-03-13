using Flunt.Validations;
using System;
using System.Security.Cryptography;
using System.Text;
using Gsl.Info.Cadastrais.Domain.Entities.Core;

namespace Gsl.Info.Cadastrais.Domain.Entities
{
    /// <summary>
    /// Classe da entidade de Usuario
    /// </summary>
    public class Usuario : Entity
    {
        /// <summary>
        /// Construtor padrão de operador
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Construtor de operador
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        public Usuario(string login, string senha)
        {
            Login = login;            
            DataCriacao = DateTime.UtcNow;            

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Login, nameof(Login), "Login não pode ser nulo ou vazio")
                .IsNotNullOrEmpty(senha, nameof(senha), "Senha não pode ser nula ou vazia"));

            Senha = GerarHashSenha(senha);
        }

        /// <summary>
        /// Login do usuario
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Senha { get; set; }        
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Criar um hash único de acordo com os parâmetros recebidos
        /// </summary>
        public static string GerarHashSenha(params object[] args)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.Unicode.GetBytes(string.Concat(args)));
            var s = new StringBuilder();
            foreach (byte b in hash)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
    }
}
