using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public class TipoServico
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public float valor { get; set; }
    }
}