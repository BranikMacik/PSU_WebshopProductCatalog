using Microsoft.Extensions.Logging;
using Nest;
using Webshop.Domain.Common;
using Webshop.Search.Persistence.Contracts;

namespace Webshop.Search.Persistence
{
    public class SearchReadRepository<TEntity> where TEntity : ElasticEntityBase
    {
        private readonly IElasticClient elasticClient;
        private readonly ILogger<SearchReadRepository<TEntity>> logger;

        public SearchReadRepository(
            IElasticClient elasticClient,
            ILogger<SearchReadRepository<TEntity>> logger)
        {
            this.elasticClient = elasticClient;
            this.logger = logger;
        }

        public async Task<ISearchResponse<TEntity>> SearchByIndexAsync(
            string query,
            Indices indices,
            int from,
            int size)
        {
            var terms = new List<Func<QueryContainerDescriptor<TEntity>, QueryContainer>>
            {
                x => x.QueryString(queryDescriptor => queryDescriptor.Query(query))
            };

            var result = await elasticClient.SearchAsync<TEntity>
            (s => s.Index(indices)
                .From(from)
                .Size(size)
                .Query(b => b.Bool(
                    bq => bq.Must(terms))
                ));

            logger.LogDebug(result.DebugInformation);
            if (!result.IsValid)
            {
                logger.LogError(result.OriginalException, $"The query was invalid. {result.ServerError}");
            }

            return result;
        }
    }
}
