using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PetInfo.Web.models;

namespace PetInfo.Web.Hibernate
{
    public static class NhibernateHelper
    {
        public static ISessionFactory CreateSessionFactory2()
        {
            var sessionFactory = Fluently.Configure()

                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0; Integrated Security=True; MultipleActiveResultSets=True;AttachDBFilename=|DataDirectory|\Test.mdf")
                        )//End Database
                .ExposeConfiguration(config => new SchemaExport(config).Create(false, true))
                            .Mappings(m =>
                m.FluentMappings
                  .AddFromAssemblyOf<Owner>())
                .BuildSessionFactory();
            return sessionFactory;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            var sessionFactory = Fluently.Configure()

                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0; Integrated Security=True; MultipleActiveResultSets=True;AttachDBFilename=|DataDirectory|\Test.mdf")
                        )//End Database
                            .Mappings(m =>
                m.FluentMappings
                  .AddFromAssemblyOf<Owner>())
                .BuildSessionFactory();
            return sessionFactory;
        }
    }
}