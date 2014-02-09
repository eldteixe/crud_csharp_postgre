using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Repository
{
    public class FluentySessionFactory
    {
        private static string ConnectionString = "Server=localhost; Port=5432; User Id=postgres; Password=190588; Database=pessoas";

        private static ISessionFactory session;

        public static ISessionFactory CriarSession()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);            
            var configMap                   = Fluently.Configure().Database(configDB).Mappings(m => m.FluentMappings.AddFromAssemblyOf<Mapping.ContatosMap>());
            session                         = configMap.BuildSessionFactory();
            return session;
        }

        public static ISession AbrirSession()
        {
            return CriarSession().OpenSession();
        }
    }
}
