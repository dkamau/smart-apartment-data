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
        public string documentName { get; set; }


        [HttpGet("/Search/{documentName}/{searchTerm}")]
        [SwaggerOperation(
            Summary = "Search's an OpenSearch document",
            Description = "Search's an OpenSearch document",
            OperationId = "SmartApartmentData.SearchAll",
            Tags = new[] { "Open Search Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CancellationToken cancellationToken)
        {
            var response = await _awsService.SearchAsync(searchTerm);

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }
    }
}
