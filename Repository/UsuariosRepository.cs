using NHibernate;
using NHibernate.Linq;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UsuariosRepository : RepositoryCrudDao<Usuarios>
    {
        public bool ValidaLogin(string login)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<Usuarios>() where e.login.Equals(login) select e).Count() > 0;
            }
        }
    }
}
