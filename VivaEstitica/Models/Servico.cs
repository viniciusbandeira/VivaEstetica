using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public enum Estados
    {
        [Description("Aguardando")]
        aguardando,
        [Description("Confirmado")]
        confirmado,
        [Description("Recusado")]
        recusado,
        [Description("Cancelado Pelo Cliente")]
        canceladoCliente,
        [Description("Cancelado Pelo Profissional")]
        canceladoProfissional,
        [Description("Concluido")]
        concluido
    }
    public class Servico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        [Required]
        public Cliente Cliente { get; set; }
        [Required]
        public Profissional Profissional { get; set; }
        [Required]
        public TipoServico TipoServico { get; set; }
        [Required]
        public Estados? Estado { get; set; }
    }
}