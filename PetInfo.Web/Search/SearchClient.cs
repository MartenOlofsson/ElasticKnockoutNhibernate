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

        public async void Index(Owner owner)
        {
            await Client.IndexAsync(owner, x => x.Index("owners"));
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
                    .Term(p => p.Name, query)
                    ));

            return await results;
        }
    }
}