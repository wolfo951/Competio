using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public SessionController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public ActionResult<User> Login([FromBody] LoginInfo login)
        {
            var foundUser = _context.Users.Where(User => User.Username == login.UserName && User.Password == login.Password).FirstOrDefault();
            if(foundUser == null)
                return Unauthorized();

            var newToken = Authorization.Authorization.GenerateToken(foundUser, _configuration);
            if (newToken == null)
                return BadRequest();

            return Ok(newToken);
        }

        [HttpDelete]
        public async Task<ActionResult> SingOut()
        {
            return Ok();
        }
    }
}
