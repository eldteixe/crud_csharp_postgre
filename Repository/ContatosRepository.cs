using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using NHibernate;

namespace Repository
{
    public class ContatosRepository : RepositoryCrudDao<Contatos>
    {
        public bool RegistroCadastrado(string nome)
        { 
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Contatos>() 
                        where e.nome.Equals(nome) 
                        && e.status.Equals("A") 
                        select e).Count() > 0;
            }
        }
    }
}
