using Application.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(UserLoginDTO userObject);
        Task RegisterAsync(UserLoginDTO userObject);
    }
}
