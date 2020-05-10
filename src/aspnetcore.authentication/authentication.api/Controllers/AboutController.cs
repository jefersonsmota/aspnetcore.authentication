using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace authentication.api.Controllers
{
    [Route("")]
    [ApiController]
    [AllowAnonymous]
    public class AboutController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Index()
        {
            return Ok(new { 
                Title = "Authentication Api",
                Description =  "API RESTful de usuários + login",
                Version = "1.0.0",
                Author = "Jefeson Mota"
            });
        }
    }
}