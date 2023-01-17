using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ThreadsController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public ThreadsController(MessageBoardContext db)
    {
      _db = db;
    }
    
    // GET api/threads
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Threads>>> Get(string title)
    {
      IQueryable<Threads> query = _db.Threads.AsQueryable();
      return await query.ToListAsync();
    }

    // GET: api/Threads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Threads>> GetThreads(int id)
        {
            Threads threads = await _db.Threads.FindAsync(id);

            if (threads == null)
            {
                return NotFound();
            }

            return threads;
        }

        [HttpPost]
        public async Task<ActionResult<Threads>> Post(Threads threads)
        {
          _db.Threads.Add(threads);
          await _db.SaveChangesAsync();
          return CreatedAtAction(nameof(GetThreads), new { id = threads.ThreadsId }, threads);
        }
  }
}