using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(dbContext,
                  timeService,
                  claimsService)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CheckUserNameExited(string username) => _dbContext.Accounts.AnyAsync(u => u.UserName == username);

        public async Task<Account> GetUserByUserNameAndPasswordHash(string username, string passwordHash)
        {
            var user = await _dbContext.Accounts
                .FirstOrDefaultAsync( record => record.UserName == username
                                        && record.Password == passwordHash);
            if(user is null)
            {
                throw new Exception("UserName & password is not correct");
            }


            return user;
        }
    }
}
