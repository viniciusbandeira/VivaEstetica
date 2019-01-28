using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VivaEstitica.Utils;

namespace VivaEstitica.Models
{
    public class VivaEsteticaContextSeedInitializer : DropCreateDatabaseAlways<VivaEsteticaContext>
    {
        protected override void Seed(VivaEsteticaContext context)
        {
            var senha = "teste";
            senha = Hasher.CriptografaMD5(senha);
            var clientes = new List<Cliente>
            {
                new Cliente{Celular="(12)1-2345-6789", Documento="111.111.111-11", Endereco="Rua rua Bairro bairro Casa casa", Nome="Ciclano", Sobrenome="da Silva", Senha=senha},
                new Cliente{Celular="(12)1-2345-6789", Documento="111.111.111-12", Endereco="Rua rua Bairro bairro Casa casa", Nome="Belcrano", Sobrenome="da Silva", Senha=senha},
                new Cliente{Celular="(12)1-2345-6789", Documento="111.111.111-13", Endereco="Rua rua Bairro bairro Casa casa", Nome="Teltrano", Sobrenome="da Silva", Senha=senha}

            };

            clientes.ForEach(c => context.Clientes.Add(c));
            context.SaveChanges();

            var Gerenciador = new Gerenciador { Nome = "Admin", Documento = "111.111.111-10", Senha = senha};
            context.Gerenciadors.Add(Gerenciador);

            var TiposServico = new List<TipoServico>
            {
                new TipoServico{Descricao="Micro agulhamento", valor=(float)550.00, DuracaoMinutos=100},
                new TipoServico{Descricao="Lipoaspiração", valor=(float)2000.00, DuracaoMinutos=400},
                new TipoServico{Descricao="Pilling", valor=(float)100.00, DuracaoMinutos=50}
            };

            TiposServico.ForEach(t => context.TipoServicos.Add(t));
            context.SaveChanges();

            var Profissionais = new List<Profissional>
            {
                new Profissional{Documento="111.111.111-14", Formacao="Esteticista", Nome="Dr mão de ouro", Senha=senha},
                new Profissional{Documento="111.111.111-15", Formacao="Eletricista", Nome="Dr Energia", Senha=senha},
                new Profissional{Documento="111.111.111-16", Formacao="Pedreiro", Nome="Dr Embeleza", Senha=senha}
            };

            Profissionais.ForEach(p => context.Profissionais.Add(p));
            context.SaveChanges();

            var Servicos = new List<Servico>
            {
                new Servico
                {
                    Cliente =context.Clientes.FirstOrDefault(c => c.Documento.Equals("111.111.111-11")),
                    Profissional=context.Profissionais.FirstOrDefault(c => c.Documento.Equals("111.111.111-14")),
                    Inicio=new DateTime(2018, 01, 30, 14, 00, 00),
                    TipoServico=context.TipoServicos.Find(1),
                    Estado= Estados.aguardando
                },
                new Servico
                {
                    Cliente =context.Clientes.FirstOrDefault(c => c.Documento.Equals("111.111.111-12")),
                    Profissional=context.Profissionais.FirstOrDefault(c => c.Documento.Equals("111.111.111-15")),
                    Inicio=new DateTime(2018, 01, 30, 14, 00, 00),
                    TipoServico=context.TipoServicos.Find(2),
                    Estado= Estados.aguardando
                },
                new Servico
                {
                    Cliente =context.Clientes.FirstOrDefault(c => c.Documento.Equals("111.111.111-13")),
                    Profissional=context.Profissionais.FirstOrDefault(c => c.Documento.Equals("111.111.111-15")),
                    Inicio=new DateTime(2018, 01, 31, 14, 00, 00),
                    TipoServico=context.TipoServicos.Find(3),
                    Estado= Estados.aguardando
                }
            };

            
        }
    }
}