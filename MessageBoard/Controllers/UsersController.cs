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
  }
}