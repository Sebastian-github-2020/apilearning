using Microsoft.AspNetCore.Mvc;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;
using apilearning.ModelDtos;
using AutoMapper;
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
        public IMapper _mapper { get; } // 注入automapper
        public UserController(netsqlContext dbContext, IMapper mapper) {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> Users() {
            var users = await _context.Users.ToListAsync();
            // 使用dto模型来输出
            List<UserDto> dtos = new List<UserDto>();
            // 老方法 使用遍历  将模型->dto模型
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
            // 使用AutoMapper来实现 对象转换  模型->dto模型
            var dtos = _mapper.Map<IEnumerable<UserDto>>(users);


            return Ok(dtos);
        }
    }
}
