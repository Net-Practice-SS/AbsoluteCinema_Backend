using AbsoluteCinema.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExceptionFilter]
    public abstract class BaseController : ControllerBase
    {
    }
}
