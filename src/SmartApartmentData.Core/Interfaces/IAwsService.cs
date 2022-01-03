using System.Net.Http;
using System.Threading.Tasks;

namespace SmartApartmentData.Core.Interfaces
{
    public interface IAwsService
    {
        Task<HttpResponseMessage> UploadAsync(string documentName, string data, int? id = null);
        Task<HttpResponseMessage> DeleteAsync(string documentName, int id);
        Task<HttpResponseMessage> DeleteAllAsync(string documentName);
        Task<HttpResponseMessage> SearchAsync(string searchTerm, string searchMarkets = "", int limit = 25);
    }
}
