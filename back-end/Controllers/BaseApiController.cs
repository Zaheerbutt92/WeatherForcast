using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        //Any base actions will go here... 
        //i.e activity logger for users
        //also other common methods
        //It will be the base controller 
    }
}