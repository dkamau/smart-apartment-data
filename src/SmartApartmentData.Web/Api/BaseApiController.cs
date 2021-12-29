using Microsoft.AspNetCore.Mvc;

namespace SmartApartmentData.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
