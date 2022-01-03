using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartApartmentData.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartApartmentData.Web.Endpoints.OpenSearch
{
    public class Upload : BaseAsyncEndpoint<IFormFile, bool>
    {
        private readonly IAwsService _awsService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Upload(IAwsService awsService, IWebHostEnvironment hostEnvironment)
        {
            _awsService = awsService;
            _hostingEnvironment = hostEnvironment;
        }

        [HttpPost("/Upload")]
        [SwaggerOperation(
            Summary = "Uploads a JSON file to OpenSearch",
            Description = "Uploads a JSON file that contains multiple documents to an OpenSearch Service domain.\n" +
                          "The JSON file name will be used as the document name.",
            OperationId = "SmartApartmentData.Upload",
            Tags = new[] { "Open Search Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(IFormFile jsonFile, CancellationToken cancellationToken)
        {
            // Validate that the uploaded file is a valid Json file.
            string fileExtension = Path.GetExtension(jsonFile.FileName).ToLower();
            if (!fileExtension.Equals(".json"))
                return BadRequest(new
                {
                    Error = "Ivalid File! Please upload a valid json file."
                });

            // Create a directory where uploaded files will be saved
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            // Save the uploaded file to the directory
            string fileName = jsonFile.FileName;
            string filePath = Path.Combine(uploads, jsonFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await jsonFile.CopyToAsync(fileStream);
            }

            // Read all file contents and convert them to Json objects
            var fileData = System.IO.File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<List<JObject>>(fileData);

            // Upload the data to Amazon open search service using multiple tasks running in parallel for speed optimization.
            int uploadCount = 0;
            var tasks = data.Select(async item =>
            {
                var response = await _awsService.UploadAsync(Path.GetFileNameWithoutExtension(jsonFile.FileName).ToLower().Trim(), JsonConvert.SerializeObject(item));
                if (response.IsSuccessStatusCode)
                    uploadCount++;
            });
            await Task.WhenAll(tasks);

            return Ok(new 
            { 
                Message = $"{uploadCount} items uploaded successfully."
            });
        }
    }
}
