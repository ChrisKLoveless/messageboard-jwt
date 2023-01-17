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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Threads thread)
    {
      if (id != thread.ThreadsId)
      {
        return BadRequest();
      }

      _db.Threads.Update(thread);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ThreadExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool ThreadExists(int id)
    {
      return _db.Threads.Any(e => e.ThreadsId == id);
    }

    // DELETE: api/Threads/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteThread(int id)
    {
      Threads thread = await _db.Threads.FindAsync(id);
      if (thread == null)
      {
        return NotFound();
      }

      _db.Threads.Remove(thread);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}