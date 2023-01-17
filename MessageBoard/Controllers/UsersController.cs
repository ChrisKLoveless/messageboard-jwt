using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public UsersController(MessageBoardContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get(string name)
    {
      IQueryable<User> query = _db.Users.AsQueryable();
      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      User user = await _db.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
      _db.Users.Add(user);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, User user)
    {
      if (user.UserId != id)
      {
        return BadRequest();
      }

      _db.Users.Update(user);

      try 
      {
        await _db.SaveChangesAsync();
      } 
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(user.UserId))
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, User user)
    {
      if (!UserExists(user.UserId))
      {
        return NotFound();
      }
      _db.Users.Remove(user);
      await _db.SaveChangesAsync();
      
      return NoContent();
    }

    private bool UserExists(int id)
      {
          return _db.Users.Any(e => e.UserId == id);
      }
  }
}