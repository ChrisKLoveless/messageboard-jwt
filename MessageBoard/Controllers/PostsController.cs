using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostsController : ControllerBase
  {
    private readonly MessageBoardContext _db;

    public PostsController(MessageBoardContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> Get(int userId, int threadId)
    {
      IQueryable<Post> query = _db.Posts.AsQueryable();
      if (userId != null)
      {
        query = query.Where(po => po.UserId == userId);
      }
      if (threadId != null)
      {
        query = query.Where(po => po.ThreadsId == threadId);
      }

      return await query.ToListAsync();
    }

    // GET: api/Posts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
      Post post = await _db.Posts.FindAsync(id);

      if (post == null)
      {
        return NotFound();
      }

      return post;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Post(Post post)
    {
      _db.Posts.Add(post);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
    }

    // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _db.Posts.Update(post);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        private bool PostExists(int id)
        {
            return _db.Posts.Any(e => e.PostId == id);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            Post post = await _db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();

            return NoContent();
        }
  }
}