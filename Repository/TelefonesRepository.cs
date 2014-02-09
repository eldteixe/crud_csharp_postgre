using NHibernate;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Repository
{
    public class TelefonesRepository : RepositoryCrudDao<Telefones>
    {
        public List<Telefones> RetornarPorContato(int contato)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Telefones>() where e.id_contatos == contato select e).ToList();
            }
        }
    }
}
