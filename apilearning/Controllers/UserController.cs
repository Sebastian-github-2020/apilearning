using Microsoft.AspNetCore.Mvc;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;
// office
namespace apilearning.Controllers {
    /// <summary>
    /// 用户
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        public netsqlContext _context { get; } = null!;
        public UserController(netsqlContext dbContext) {
            _context = dbContext;
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> Users(int id) {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        /// <summary>
        /// post 查询用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("users")]
        public async Task<ActionResult> GetUsers() {
            var users = await _context.Users.ToListAsync();
            return new JsonResult(users);
        }
    }
}
