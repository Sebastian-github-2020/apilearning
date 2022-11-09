using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apilearning.Controllers
{
    /// <summary>
    /// 账户api
    /// </summary>
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("api/account")]
    public class AccountControler : ControllerBase
    {
        public accountContext _db { get; } = null!;
        /// <summary>
        /// 注入 数据库上下文
        /// </summary>
        /// <param name="db">数据库上下文</param>
        public AccountControler(accountContext db) {
            _db = db;
        }

        /// <summary>
        /// 查询所有账户
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<List<MyAccount>>> GetAccount() {
            return await _db.MyAccounts.ToListAsync();
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
