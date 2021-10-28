using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackendChallange.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await context.Users.FindAsync(id);
        //    if (user == null) return NotFound();

        //    return user;
        //}
    }
}