using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Repository.Entities;

namespace Repository.Mapping
{
    public class UsuariosMap : ClassMap<Usuarios>
    {
        public UsuariosMap()
        {
            Id(c => c.id);
            Map(c => c.login);
            Map(c => c.nome);
            Map(c => c.senha);
            Map(c => c.status);
            Table("usuarios");
        }
    }
}
