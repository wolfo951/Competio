using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentRequestController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TournamentRequestController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("{RequestId}")]
        public async Task<ActionResult<TournamentRequest>> Get(int RequestId)
        {
            return await _context.TournamentRequests.FindAsync(RequestId);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TournamentRequest request)
        {
            var added = _context.TournamentRequests.AddAsync(request);
            await added;

            if (!added.IsCompletedSuccessfully)
            {
                return BadRequest(added.AsTask().Exception);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
