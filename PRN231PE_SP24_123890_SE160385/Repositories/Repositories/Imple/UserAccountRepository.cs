using DataAccessLayer.DAOs;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Imple
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserAccountDAO userAccountDAO;
        public UserAccountRepository(UserAccountDAO _userAccountDAO)
        {
            userAccountDAO = _userAccountDAO;
        }
        public async Task<UserAccount> Login(string email, string password)
            => await userAccountDAO.Login(email, password);
    }
}
