using Microsoft.AspNetCore.Mvc;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;
using apilearning.ModelDtos;
using AutoMapper;
using apilearning.Tools;
using System.Collections.Generic;
// office
namespace apilearning.Controllers {
    /// <summary>
    /// 用户
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : CommonController {

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
        ///  查询用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("users")]
        [HttpGet("users1")]
        public async Task<ActionResult<ApiResult<IEnumerable<UserDto>>>> GetUsers() {
            ApiResult<IEnumerable<UserDto>>? a;
            try {
                var users = await _context.Users.ToListAsync();
                // 使用AutoMapper来实现 对象转换  模型->dto模型
                IEnumerable<UserDto>? dtos = _mapper.Map<IEnumerable<UserDto>>(users);
                a = GenActionResultGenericEx<IEnumerable<UserDto>>(dtos, "查询成功");
            } catch(Exception e) {
                a = GenActionResultGenericEx<IEnumerable<UserDto>>(null, "查询异常", ApiResultCode.Failed, e);
            }
            return Ok(a);
        }

        /// <summary>
        /// 通过id 查询用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="message">用户message</param>
        /// <returns></returns>
        [HttpPost("getuser")]
        public async Task<ActionResult<ApiResult<UserDto>>> QueryUser(int id, [FromRoute] string message = "查询成功") {
            ApiResult<UserDto>? a;
            try {
                User? u = await _context.Users.FindAsync(id);
                // automappe映射
                UserDto? udto = _mapper.Map<UserDto>(u);
                // 封装到统一的返回类
                a = GenActionResultGenericEx(udto, message, ApiResultCode.Success);
            } catch(Exception e) {

                a = GenActionResultGenericEx<UserDto>(null, "查询成功", ApiResultCode.Success, e);
            }


            return Ok(a);
        }
    }
}
