using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Repository.Entities;

namespace Repository.Mapping
{
    public class ContatosMap : ClassMap<Contatos>
    {
        public ContatosMap()
        {
            Id(c => c.id);
            Map(c => c.nome);
            Map(c => c.numero);
            Map(c => c.status);
            Map(c => c.estado);
            Map(c => c.endereco);
            Map(c => c.cidade);
            Map(c => c.cep);
            Map(c => c.bairro);
            Table("contatos");
        }
    }
}
