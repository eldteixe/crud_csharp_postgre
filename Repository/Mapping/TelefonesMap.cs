using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Repository.Entities;

namespace Repository.Mapping
{
    public class TelefonesMap : ClassMap<Telefones>
    {
        public TelefonesMap()
        {
            Id(c => c.id);
            Map(c => c.id_contatos);
            Map(c => c.site);
            Map(c => c.telefone);
            Map(c => c.email);
            Table("telefones");
        }
    }
}
