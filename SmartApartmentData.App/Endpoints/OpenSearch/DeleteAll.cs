using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SmartApartmentData.Web.Endpoints.OpenSearch
{
    public class DeleteAll : BaseAsyncEndpoint<bool>
    {
        private readonly IAwsService _awsService;

        public DeleteAll(IAwsService awsService)
        {
            _awsService = awsService;
        }

        [FromRoute]
        public string documentName { get; set; }


        [HttpDelete("/DeleteAll/{documentName}")]
        [SwaggerOperation(
            Summary = "Delete's an OpenSearch document",
            Description = "Delete's an OpenSearch document",
            OperationId = "SmartApartmentData.DeleteAll",
            Tags = new[] { "Open Search Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CancellationToken cancellationToken)
        {
            var response = await _awsService.DeleteAllAsync(documentName);
            return NoContent();
        }
    }
}
