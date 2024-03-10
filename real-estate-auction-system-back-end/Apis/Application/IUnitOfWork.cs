using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {

        public IAccountRepository AccountRepository { get; }

        public IRealEstateRepository RealEstateRepository { get; }

        public INewsRepository NewsRepository { get; }

        public IAuctionRepository AuctionRepository { get; }

        public IRealtimeAuctionRepository RealtimeAuctionRepository { get; }
        public IRealEstateImageRepository RealEstateImageRepository { get; }

        public IOrderRepository OrderRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
