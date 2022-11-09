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
    [Route("[controller]")]
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

        [HttpPost(Name = "accounts")]
        public async Task<ActionResult<List<MyAccount>>> GetAccount() {
            return await _db.MyAccounts.ToListAsync();
        }
    }
}
