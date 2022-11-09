using Microsoft.AspNetCore.Mvc;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;

namespace apilearning.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public netsqlContext _context { get; } = null!;
        public UserController(netsqlContext dbContext) {
            _context = dbContext;
        }
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "getUsers")]
        public async Task<ActionResult<List<User>>> GetUsers() {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
