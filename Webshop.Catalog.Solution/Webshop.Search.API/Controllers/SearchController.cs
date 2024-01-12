using Microsoft.AspNetCore.Mvc;
using Nest;
using Webshop.Search.Domain.AggregateRoots;
using Webshop.Search.Persistence;

namespace Webshop.Search.API.Controllers
{
    [Route("/search")]
    [ApiController]
    public class SearchController : BaseController
    {
        private readonly SearchReadRepository<ProductEntity> _searchReadRepository;
        private readonly ILogger<SearchController> _logger;

        public SearchController( SearchReadRepository<ProductEntity> searchReadRepository, ILogger<SearchController> logger)
        {
            _searchReadRepository = searchReadRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> SearchByTitleAsync([FromQuery] string query, [FromQuery]int page, [FromQuery]int perPage)
        {
            _logger.LogInformation("Querying for {query}", query);
            var entries = await _searchReadRepository.SearchByIndexAsync(query, Indices.All, page, perPage);
            _logger.LogInformation("Found {count} entries.", entries.Hits.Count);
            return Ok(entries.Hits);
        }
    }
}
