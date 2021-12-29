using Microsoft.Extensions.Configuration;
using SmartApartmentData.Core.Interfaces;
using SmartApartmentData.Infrastructure.Configs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartmentData.Infrastructure.Services
{
    public class AwsService : IAwsService
    {
        private string domain;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AwsService(IConfiguration configuration)
        {
            _configuration = configuration;

            AwsOpenSearch awsOpenSearch = _configuration.GetSection("AwsOpenSearch").Get<AwsOpenSearch>();
            domain = awsOpenSearch.Domain;

            _httpClient = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes($"{awsOpenSearch.Username}:{awsOpenSearch.Password}");
            var header = new AuthenticationHeaderValue(
                       "Basic", Convert.ToBase64String(byteArray));
            _httpClient.DefaultRequestHeaders.Authorization = header;
        }

        /// <summary>
        /// Uploads data to an OpenSearch Service domain
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UploadAsync(string documentName, string data, int? id = null)
        {
            string uri = $"{domain}/{documentName}/_doc";
            if (id != null)
                uri = $"{domain}/{documentName}/_doc/{id}";

            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(uri, content);

            return result;
        }

        /// <summary>
        /// Delete a single item from OpenSearch document
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string documentName, int id)
        {
            string uri = $"{domain}/{documentName}/_doc/{id}";

            var result = await _httpClient.DeleteAsync(uri);

            return result;
        }

        /// <summary>
        /// Deletes entire document from OpenSearch 
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAllAsync(string documentName)
        {
            string uri = $"{domain}/{documentName}/_doc";

            var result = await _httpClient.DeleteAsync(uri);

            return result;
        }

        public async Task<HttpResponseMessage> SearchAsync(string documentName, string searchTerm)
        {
            string uri = $"{domain}/{documentName}/_search?q={searchTerm.Trim()}&pretty=true";

            var result = await _httpClient.GetAsync(uri);

            return result;
        }
    }
}
