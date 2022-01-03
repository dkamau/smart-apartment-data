using Newtonsoft.Json;
using SmartApartmentData.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartApartmentData.App.Data
{
    public class SearchService
    {
        private readonly IAwsService _awsService;
        public SearchService(IAwsService awsService)
        {
            _awsService = awsService;
        }

        public async Task<SearchResult> Find(string searchTerm, string searchMarkets, int limit)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var response = await _awsService.SearchAsync(searchTerm, searchMarkets, limit);

                    if (response != null)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(responseBody);

                        return searchResult;
                    }
                }
            }
            catch(Exception) { }

            return null;
        }
    }
}
