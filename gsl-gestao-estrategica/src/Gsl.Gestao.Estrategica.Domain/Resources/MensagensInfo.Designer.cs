﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gsl.Gestao.Estrategica.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MensagensInfo {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MensagensInfo() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gsl.Gestao.Estrategica.Domain.Resources.MensagensInfo", typeof(MensagensInfo).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OPS!!! Já existe um cliente cadastrado com o CPF informado..
        /// </summary>
        public static string Cliente_CpfExistente {
            get {
                return ResourceManager.GetString("Cliente_CpfExistente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao deletar cliente com o CPF informado!.
        /// </summary>
        public static string Cliente_ErroDeletar {
            get {
                return ResourceManager.GetString("Cliente_ErroDeletar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi encontrado cliente com o CPF informado!.
        /// </summary>
        public static string Cliente_NaoEncontrado {
            get {
                return ResourceManager.GetString("Cliente_NaoEncontrado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Já existe uma entrega cadastrada com o código informado..
        /// </summary>
        public static string Entrega_CodigoExistente {
            get {
                return ResourceManager.GetString("Entrega_CodigoExistente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao deletar entrega com o código informado!.
        /// </summary>
        public static string Entrega_ErroDeletar {
            get {
                return ResourceManager.GetString("Entrega_ErroDeletar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi encontrada uma entrega com o código informado!.
        /// </summary>
        public static string Entrega_NaoEncontrada {
            get {
                return ResourceManager.GetString("Entrega_NaoEncontrada", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Já existe um estoque cadastrado com o código informado..
        /// </summary>
        public static string Estoque_CodigoExiste {
            get {
                return ResourceManager.GetString("Estoque_CodigoExiste", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao deletar estoque com o código informado!.
        /// </summary>
        public static string Estoque_ErroDeletar {
            get {
                return ResourceManager.GetString("Estoque_ErroDeletar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi encontrado um estoque com o código informado!.
        /// </summary>
        public static string Estoque_NaoEncontrado {
            get {
                return ResourceManager.GetString("Estoque_NaoEncontrado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao atulizar a mercadoria {0} pois sua quantidade máxima no estoque é de {1}.
        /// </summary>
        public static string Mercadoria_MaxQuantidade {
            get {
                return ResourceManager.GetString("Mercadoria_MaxQuantidade", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi encontrada uma mercadoria com o código informado!.
        /// </summary>
        public static string Mercadoria_NaoEncontrada {
            get {
                return ResourceManager.GetString("Mercadoria_NaoEncontrada", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao deletar pedido com o código informado!.
        /// </summary>
        public static string Pedido_ErroDeletar {
            get {
                return ResourceManager.GetString("Pedido_ErroDeletar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao deletar item do pedido com o código informado!.
        /// </summary>
        public static string Pedido_ErroDeletarItem {
            get {
                return ResourceManager.GetString("Pedido_ErroDeletarItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não foi encontrado um pedido com o código informado!.
        /// </summary>
        public static string Pedido_NaoEncontrado {
            get {
                return ResourceManager.GetString("Pedido_NaoEncontrado", resourceCulture);
            }
        }
    }
}
