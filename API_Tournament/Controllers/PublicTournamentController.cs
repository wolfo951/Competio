using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicTournamentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PublicTournamentController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<TournamentController>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ActionResult<List<Tournament>>> Get()
        {
            return _context.Tournaments.Where(tournament => tournament.IsPrivate.HasValue && !tournament.IsPrivate.Value).ToList();
        }
    }
}
