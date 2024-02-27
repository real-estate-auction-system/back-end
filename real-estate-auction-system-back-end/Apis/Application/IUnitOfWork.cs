using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {

        public IAccountRepository AccountRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}
