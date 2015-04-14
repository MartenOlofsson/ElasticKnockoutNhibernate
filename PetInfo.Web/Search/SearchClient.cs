using System;
using System.Threading.Tasks;
using Nest;
using PetInfo.Web.models;

namespace PetInfo.Web.Search
{
    public interface ISearchClient
    {
        void Index(Owner owner);
        Task<ISearchResponse<Owner>> GetAll();
        Task<ISearchResponse<Owner>> Search(string query);
    }

    public class SearchClient : ISearchClient
    {
        private static ElasticClient Client
        {
            get
            {
                var node = new Uri("http://localhost:9200");
                var settings = new ConnectionSettings(node);
                var client = new ElasticClient(settings);
                return client;
            }
        }

        public void Index(Owner owner)
        {
            Client.Index(owner, x => x.Index("owners").Id(owner.Id));
        }

        public async Task<ISearchResponse<Owner>> GetAll()
        {
            var results = Client.SearchAsync<Owner>(s => s
                .From(0)
                .Size(10));

            return await results;
        }

        public async Task<ISearchResponse<Owner>> Search(string query)
        {
            var results = Client.SearchAsync<Owner>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Fuzzy(f => f
                        .OnField("_all")
                        .Value(query))
                )
                .FacetTerm(x => x.OnField(f => f.Age)));

            return await results;
        }

        public void Delete()
        {
            Client.DeleteIndex("owners");
        }
    }
}