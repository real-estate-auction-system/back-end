using Domain.Entities;

namespace Application.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<Account> GetUserByUserNameAndPasswordHash(string username, string passwordHash);

        Task<bool> CheckUserNameExited(string username);
    }
}
