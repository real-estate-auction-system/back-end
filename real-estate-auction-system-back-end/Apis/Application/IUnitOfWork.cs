using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {

        public IAccountRepository AccountRepository { get; }

        public IRealEstateRepository RealEstateRepository { get; }

        public INewsRepository NewsRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}
