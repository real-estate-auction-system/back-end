using Application;
using Application.Repositories;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IRealEstateRepository _realEstateRepository;

        public UnitOfWork(AppDbContext dbContext,
            IAccountRepository accountRepository, IRealEstateRepository realEstateRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _realEstateRepository = realEstateRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;

        public IRealEstateRepository RealEstateRepository => _realEstateRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
