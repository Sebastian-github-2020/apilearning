using Microsoft.AspNetCore.Mvc;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;
using apilearning.ModelDtos;
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
        public async Task<ActionResult<List<UserDto>>> Users() {
            var users = await _context.Users.ToListAsync();
            // 使用dto模型来输出
            List<UserDto> dtos = new List<UserDto>();
            foreach(var user in users) {
                dtos.Add(new UserDto {
                    Id = user.Id,
                    Name = user.Name,
                    Salary = user.Salary,
                    BornDate = user.BornDate.ToLongDateString()
                });
            }
            return Ok(dtos);
        }
        /// <summary>
        /// post 查询用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("users")]
        public async Task<ActionResult> GetUsers() {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}
