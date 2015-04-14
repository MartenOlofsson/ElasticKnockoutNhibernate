using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using PetInfo.Web.Hibernate;
using PetInfo.Web.Search;

namespace PetInfo.Web.Boostrappers
{
    public class CustomBootStrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<ISearchClient, SearchClient>().AsMultiInstance();
            container.Register<OwnerRepository>(new OwnerRepository(NhibernateHelper.CreateSessionFactory()));
        }
    }
}