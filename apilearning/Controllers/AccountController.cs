using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// home
namespace apilearning.Controllers
{
    /// <summary>
    /// 账户api
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private accountContext Db { get; }
        /// <summary>
        /// 注入 数据库上下文
        /// </summary>
        /// <param name="db">数据库上下文</param>
        public AccountController(accountContext db) {
            Db = db;
        }

        /// <summary>
        /// 查询所有账户
        /// </summary>
        /// <returns></returns>
        [HttpGet("accounts")]
        public async Task<ActionResult<List<MyAccount>>> GetAccount() {
            return await Db.MyAccounts.ToListAsync();
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<MyAccount>> AddAccount(MyAccount account) {
            Db.MyAccounts.Add(account);
            await Db.SaveChangesAsync();
            return CreatedAtAction("AddAccount", new { id = account.Id }, account);
        }
    }
}
