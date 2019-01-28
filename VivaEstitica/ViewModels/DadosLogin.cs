using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivaEstitica.Models;

namespace VivaEstitica.ViewModels
{
    public enum TipoUsuario { gerenciador, cliente, profissional};
    public class DadosLogin
    {
        [Key]
        public string documento { get; set; }
        [Required]
        public string senha { get; set; }
        public TipoUsuario? tipoUsuario;


        public bool Autentica(VivaEsteticaContext context)
        {
            List<AbstractUsuario> listaUsuarios = new List<AbstractUsuario>();
            listaUsuarios.AddRange(context.Gerenciadors);
            listaUsuarios.AddRange(context.Profissionais);
            listaUsuarios.AddRange(context.Clientes);
            AbstractUsuario usuario = null;
            foreach (var u in listaUsuarios)
            {
                if (u.Documento.Equals(this.documento))
                {
                    usuario = u;
                    break;
                }

            }

            

            if(usuario != null && usuario.ValidaSenha(senha))
            {
                if (usuario.GetType() is Gerenciador)
                {
                    this.tipoUsuario = TipoUsuario.gerenciador;
                }
                else if (usuario.GetType() is Profissional)
                {
                    this.tipoUsuario = TipoUsuario.profissional;
                }
                else if (usuario.GetType() is Cliente)
                {
                    this.tipoUsuario = TipoUsuario.cliente;
                }

                    return true;
            }
            else
            {
                return false;
            }
        }
    }

}