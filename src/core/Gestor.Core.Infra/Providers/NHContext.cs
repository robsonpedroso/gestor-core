using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Microsoft.Extensions.Configuration;


namespace Gestor.Core.Infra.Providers
{
    public class NHContext
    {
        public ISessionFactory SessionFactory { get; private set; }

        public NHContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GestorConn");

            SessionFactory = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHContext>())
                .BuildSessionFactory();
        }
    }
}
