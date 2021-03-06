using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SmartApartmentData.Web.Endpoints.OpenSearch
{
    public class SearchAll : BaseAsyncEndpoint<bool>
    {
        private readonly IAwsService _awsService;

        public SearchAll(IAwsService awsService)
        {
            _awsService = awsService;
        }

        [FromRoute]
        public string searchTerm { get; set; }

        [FromRoute]
        public string? searchMarkets { get; set; }

        [FromRoute]
        public int? limit { get; set; } = 25;


        [HttpGet("/Search/{searchTerm}/{searchMarkets}/{limit}")]
        [SwaggerOperation(
            Summary = "Search's an OpenSearch document",
            Description = "Search's an OpenSearch document",
            OperationId = "SmartApartmentData.SearchAll",
            Tags = new[] { "Open Search Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CancellationToken cancellationToken)
        {
            if (limit == null)
                limit = 25;

            var response = await _awsService.SearchAsync(searchTerm, searchMarkets, (int)limit);

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }
    }
}
