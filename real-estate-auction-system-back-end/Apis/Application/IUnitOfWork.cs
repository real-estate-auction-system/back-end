using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {

        public IAccountRepository AccountRepository { get; }

        public IRealEstateRepository RealEstateRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}
