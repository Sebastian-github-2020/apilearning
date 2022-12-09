using apilearning.IService;
using apilearning.Models;
using apilearning.MyDbContext;
using Microsoft.EntityFrameworkCore;

namespace apilearning.ModelServices {
    public class UserModelService : IUserModelService {
        public netsqlContext Db { get; }


        public UserModelService(netsqlContext db) {
            Db = db;
        }
        public async Task<IEnumerable<User>> GetUserListAsync(int PageSize = 10, int PageNum = 1) {
            return await Db.Users.Skip((PageNum - 1) * PageSize).Take(PageSize).ToListAsync();
        }

        public Task<IEnumerable<User>> GetUsersAsync(int Id, int PageSize = 10, int PageNum = 1) {
            throw new NotImplementedException();
        }
    }
}
