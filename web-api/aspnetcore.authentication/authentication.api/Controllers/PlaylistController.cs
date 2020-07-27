using authentication.domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace authentication.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController : ApiController
    {
        public PlaylistController(NotificationContext notificationContext) : base(notificationContext)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Response();
        }
    }
}
