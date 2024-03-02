using Application.Commons;
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
        Task<JwtResponse> LoginAsync(UserLoginDTO userObject);
        Task RegisterAsync(UserRegisterDTO userObject);
    }
}
