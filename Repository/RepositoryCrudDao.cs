using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryCrudDao<T> : ICrudDao<T> where T : class
    {
        public void Inserir(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    { 
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao inserir entidade: " + ex.Message);
                    }
                }
            }
        }

        public void Alterar(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    { 
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao atualizar entidade: " + ex.Message);
                    }
                }
            }
        }

        public void Excluir(T entidade)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao excluir entidade: " + ex.Message);
                    }
                }
            }
        }

        public T RetornarPorId(int id)
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return session.Get<T>(id);
            }
        }

        public IList<T> Consultar()
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return (from e in session.Query<T>() select e).ToList();               
            }
        }

        public IList<T> Consultar(Expression<Func<T, bool>> where )
        {
            using (ISession session = FluentySessionFactory.AbrirSession())
            {
                return session.Query<T>().Where<T>(where).ToList();
            }
        }
    }
}
