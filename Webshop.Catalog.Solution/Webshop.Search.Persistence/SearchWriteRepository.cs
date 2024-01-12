using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using Webshop.Domain.Common;
using Webshop.Search.Persistence.Contracts;

namespace Webshop.Search.Persistence
{
    public class SearchWriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : ElasticEntityBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<SearchWriteRepository<TEntity>> _logger;

        public SearchWriteRepository(IElasticClient elasticClient, ILogger<SearchWriteRepository<TEntity>> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task SaveAsync(TEntity document)
        {
            var response = await _elasticClient.IndexAsync(
                new IndexRequest<TEntity>(document, document.Index));
            
            if (!response.IsValid || response.OriginalException is not null)
            {
                _logger.LogError(response.OriginalException, $"The query for indexing the document {nameof(document)} was invalid");
                throw response.OriginalException ?? throw new OperationCanceledException($"The query for indexing the document was invalid: {response.ServerError}");
            }

            if (response.ServerError is not null)
            {
                var errorMessage =
                    $"Failed to create a document to the index {document.Index}: {JsonConvert.SerializeObject(response.ServerError)}";
                _logger.LogError(errorMessage);
                throw new OperationCanceledException(errorMessage);
            }

        }

        public async Task UpdateAsync(TEntity document, Func<TEntity, string> getId)
        {
            var id = getId(document);
            var newDoc = DocumentPath<TEntity>.Id(id);
            var response = await _elasticClient.UpdateAsync(newDoc, u => u.Index(document.Index).DocAsUpsert().Doc(document));

            _logger.LogDebug(response.DebugInformation);
            if (!response.IsValid || response.OriginalException is not null)
            {
                _logger.LogError(response.OriginalException, $"The query for updating the document {nameof(document)} was invalid");
            }

            if (response.ServerError is not null)
            {
                var errorMessage =
                    $"Failed to update the document {nameof(document)}: {JsonConvert.SerializeObject(response.ServerError)}";
                _logger.LogError(errorMessage);
                throw new OperationCanceledException(errorMessage);
            }
        }
    }
}
