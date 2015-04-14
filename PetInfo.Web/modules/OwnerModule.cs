using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Serialization.JsonNet;
using PetInfo.Web.Hibernate;
using PetInfo.Web.models;
using PetInfo.Web.Search;

namespace PetInfo.Web.modules
{
    public class OwnerModule : NancyModule
    {
        private OwnerRepository _repo;
        private ISearchClient _client;
        private readonly OwnerRepository _repository;

        public OwnerModule(ISearchClient client,OwnerRepository repository)
        {
            _client = client;
            _repository = repository;
            Get["/owners", true] = async (x, ct) =>
            {
                var owners = _repository.GetAll<Owner>();
                return new JsonResponse(owners, new JsonNetSerializer());
            };
            Post["/owners/save", true] = async (x, ct) =>
            {
                var owner = this.Bind<OwnerModel>();

                _repository.Save<Owner>(new Owner
                {
                    Age = owner.Age,
                    Name = owner.Name,
                    
                });
                return HttpStatusCode.OK;
            };
            Get["/search", true] = async (x, ct) =>
            {
                var all = await _client.GetAll();

                var docs = all.Documents;
                return new JsonResponse(docs, new DefaultJsonSerializer());
            };
            Get["/search/{query}", true] = async (x, ct) =>
            {
                var all = await _client.Search(x.query);
                return new JsonResponse(all.Hits, new DefaultJsonSerializer());
            };
            Get["/index", true] = async (x, ct) =>
            {
                var owners = _repository.GetAll<Owner>();
                foreach (var owner in owners)
                {
                    _client.Index(owner);
                }
                return HttpStatusCode.OK;
            };
        }
    }
}