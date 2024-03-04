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
        private readonly IAuctionRepository _auctionRepository;
        private readonly IRealtimeAuctionRepository _realtimeAuctionRepository;
        public UnitOfWork(AppDbContext dbContext,
            IAccountRepository accountRepository, IRealEstateRepository realEstateRepository, INewsRepository newsRepository,
            IAuctionRepository auctionRepository, IRealtimeAuctionRepository realtimeAuctionRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _realEstateRepository = realEstateRepository;
            _newsRepository = newsRepository;
            _auctionRepository = auctionRepository;
            _realtimeAuctionRepository = realtimeAuctionRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;

        public IRealEstateRepository RealEstateRepository => _realEstateRepository;

        public INewsRepository NewsRepository => _newsRepository;

        public IAuctionRepository AuctionRepository => _auctionRepository;

        public IRealtimeAuctionRepository RealtimeAuctionRepository => _realtimeAuctionRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
