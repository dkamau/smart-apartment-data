using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace SmartApartmentData.Web.Endpoints.OpenSearch
{
    public class Delete : BaseAsyncEndpoint<bool>
    {
        private readonly IAwsService _awsService;

        public Delete(IAwsService awsService)
        {
            _awsService = awsService;
        }

        [FromRoute]
        public string documentName { get; set; }

        [FromRoute]
        public int id { get; set; }

        [HttpDelete("/Delete/{documentName}/{id}")]
        [SwaggerOperation(
            Summary = "Deletes a Single item from an OpenSearch document",
            Description = "Deletes a Single item from an OpenSearch document",
            OperationId = "SmartApartmentData.Delete",
            Tags = new[] { "Open Search Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CancellationToken cancellationToken)
        {
            var response = await _awsService.DeleteAsync(documentName, id);
            return NoContent();
        }
    }
}
