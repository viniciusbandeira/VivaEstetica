﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivaEstitica.Utils;

namespace VivaEstitica.Models
{
    public abstract class AbstractUsuario
    {
        
        [Key]
        public string Documento { get; set; }
        [Required]
        public string Nome { get; set; }
        protected internal string Senha { get; set; }

        public bool ValidaSenha(string senha)
        {
            senha = Hasher.CriptografaMD5(senha);
            if (this.Senha.Equals(senha))
                return true;
            else
                return false;
        }

        public bool ModificaSenha(string senhaAntiga, string senhaNova)
        {
            senhaAntiga = Hasher.CriptografaMD5(senhaAntiga);
            if (ValidaSenha(senhaAntiga))
            {
                senhaNova = Hasher.CriptografaMD5(senhaNova);
                this.Senha = senhaNova;
                return true;
            }
            else
            {
                return false;
            } 
        }
    }

}