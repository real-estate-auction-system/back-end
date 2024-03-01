using Application;
using Application.Repositories;
using Infrastructures.Repositories;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly INewsRepository _newsRepository;

        public UnitOfWork(AppDbContext dbContext,
            IAccountRepository accountRepository, IRealEstateRepository realEstateRepository, INewsRepository newsRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _realEstateRepository = realEstateRepository;
            _newsRepository = newsRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;

        public IRealEstateRepository RealEstateRepository => _realEstateRepository;

        public INewsRepository NewsRepository => _newsRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
