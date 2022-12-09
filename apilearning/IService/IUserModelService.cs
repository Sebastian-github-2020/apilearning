using apilearning.Models;
namespace apilearning.IService {
    public interface IUserModelService {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="PageSize">每页数量，默认10</param>
        /// <param name="PageNum">页码，默认1</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetUsersAsync(int Id, int PageSize = 10, int PageNum = 1);

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="PageSize">每页数量，默认10</param>
        /// <param name="PageNum">页码，默认1</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetUserListAsync(int PageSize = 10, int PageNum = 1);
    }
}
