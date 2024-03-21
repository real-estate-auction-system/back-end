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
        private readonly IRealEstateImageRepository _realEstateImageRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IRealtimeAuctionRepository _realtimeAuctionRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        public UnitOfWork(AppDbContext dbContext,
            IAccountRepository accountRepository, IRealEstateRepository realEstateRepository,
            IRealEstateImageRepository realEstateImageRepository, INewsRepository newsRepository,
            IAuctionRepository auctionRepository, IRealtimeAuctionRepository realtimeAuctionRepository, 
            IOrderRepository orderRepository, ITypeOfRealEstateRepository typeOfRealEstateRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _realEstateRepository = realEstateRepository;
            _newsRepository = newsRepository;
            _auctionRepository = auctionRepository;
            _realtimeAuctionRepository = realtimeAuctionRepository;
            _orderRepository = orderRepository;
            _realEstateImageRepository = realEstateImageRepository;
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;

        public IRealEstateRepository RealEstateRepository => _realEstateRepository;

        public INewsRepository NewsRepository => _newsRepository;

        public IAuctionRepository AuctionRepository => _auctionRepository;

        public IRealtimeAuctionRepository RealtimeAuctionRepository => _realtimeAuctionRepository;

        public IOrderRepository OrderRepository => _orderRepository;
        public IRealEstateImageRepository RealEstateImageRepository => _realEstateImageRepository;

        public ITypeOfRealEstateRepository TypeOfRealEstateRepository => _typeOfRealEstateRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
