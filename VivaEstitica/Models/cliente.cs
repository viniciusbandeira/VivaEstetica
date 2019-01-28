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
        [Key, RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$")]
        public string Documento { get; set; }
        [Required, RegularExpression("[a-zA-Z ]*")]
        public string Nome { get; set; }
        [RegularExpression("[a-zA-Z ]*")]
        public string Sobrenome { get; set; }
        //Formato esperado: "(99)9-9999-9999"
        [Required, RegularExpression("^\\(\\d{2}\\)\\d-\\d{4}-\\d{4}$")]
        public string Celular { get; set; }
        public string Endereco { get; set; }
    }
}