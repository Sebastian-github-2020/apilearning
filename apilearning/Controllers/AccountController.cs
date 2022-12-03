using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apilearning.Tools;
using Microsoft.AspNetCore.Authorization;
// home
namespace apilearning.Controllers
{
    /// <summary>
    /// 账户api
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : CommonController
    {
        public accountContext _db { get; } = null!;
        /// <summary>
        /// 注入 数据库上下文
        /// </summary>
        /// <param name="db">数据库上下文</param>
        public AccountController(accountContext db) {
            _db = db;
        }

        /// <summary>
        /// 查询所有账户
        /// </summary>
        /// <returns></returns>
        [HttpGet("accounts")]
        [Authorize(Roles = "guest")]
        public async Task<ActionResult<ApiResult<List<MyAccount>>>> GetAccount() {
            var user = HttpContext.User;
            Console.WriteLine(user);
            //包一下 返回自定义的数据结构
            ApiResult<List<MyAccount>> a;
            try
            {
                var b = await _db.MyAccounts.ToListAsync();
                a = CommonResponse(b, "查询成功", ApiResultCode.Success);
            }
            catch(Exception e)
            {

                a = CommonResponse<List<MyAccount>>(null, "查询异常", ApiResultCode.Failed, e);
            }

            return Ok(a);
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<MyAccount>> AddAccount(MyAccount account) {
            _db.MyAccounts.Add(account);
            await _db.SaveChangesAsync();
            return CreatedAtAction("account", new { id = account.Id }, account);
        }
    }
}
