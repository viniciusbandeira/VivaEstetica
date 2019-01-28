using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public class Profissional : AbstractUsuario
    {
        public string Formacao { get; set; }

        public virtual List<Servico> Servicos { get; set; }
    }
}