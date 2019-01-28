using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VivaEstitica.Models
{
    public class VivaEsteticaContext : DbContext
    {
        public VivaEsteticaContext() : base("VicaEsteticaContext")
        {

        }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Gerenciador> Gerenciadors { get; set; }
        public virtual DbSet<Profissional> Profissionais { get; set; }
        public virtual DbSet<Servico> Servicos { get; set; }
        public virtual DbSet<TipoServico> TipoServicos { get; set; }
    }
}