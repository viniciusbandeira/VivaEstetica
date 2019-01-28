using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        [Required]
        public DateTime Fim { get; set; }
        [Required]
        public Cliente Cliente { get; set; }
        [Required]
        public Profissional Profissional { get; set; }
        [Required]
        public TipoServico TipoServico { get; set; }
    }
}