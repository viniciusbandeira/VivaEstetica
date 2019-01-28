using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public class Cliente : AbstractUsuario
    {
        //CPF
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$", ErrorMessage ="Formato esperado 999.999.999-99")]
        public override string Documento { get; set; }
        [Required, RegularExpression("[a-zA-Z ]*", ErrorMessage="Não pode conter caracteres especias e numeros")]
        public override string Nome { get; set; }
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "Não pode conter caracteres especias e numeros")]
        public string Sobrenome { get; set; }
        //Formato esperado: "(99)9-9999-9999"
        [Required, RegularExpression("^\\(\\d{2}\\)\\d-\\d{4}-\\d{4}$", ErrorMessage = "Não pode conter caracteres especias e numeros")]
        public string Celular { get; set; }
        public string Endereco { get; set; }

        public virtual List<Servico> Servicos { get; set; }
    }
}