using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }


        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("TestConnect");;
            return Ok();
        }

    }

}