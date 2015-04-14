using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PetInfo.Web.models;

namespace PetInfo.Web.Hibernate
{
    public static class NhibernateHelper
    {
        public static ISessionFactory CreateSessionFactory(bool createSchema = false)
        {
            var builder = Fluently.Configure()

                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0; Integrated Security=True; MultipleActiveResultSets=True;AttachDBFilename=|DataDirectory|\Test.mdf")
                        )//End Database
                            .Mappings(m =>
                m.FluentMappings
                  .AddFromAssemblyOf<Owner>());

            if (createSchema)
            {
                builder.ExposeConfiguration(config => new SchemaExport(config).Create(false, true));
            }
            

            var sessionFactory = builder.BuildSessionFactory();

            return sessionFactory;
        }
    }
}