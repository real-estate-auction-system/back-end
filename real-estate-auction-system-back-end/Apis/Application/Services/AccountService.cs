using Application;
using Application.Commons;
using Application.Interfaces;
using Application.Utils;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructures.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _configuration= configuration;
        }

        public async Task<string> LoginAsync(UserLoginDTO userObject)
        {
            var user = await _unitOfWork.AccountRepository.GetUserByUserNameAndPasswordHash(userObject.UserName, userObject.Password.Hash());
            return user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime());
        }

        public async Task RegisterAsync(UserLoginDTO userObject)
        {
            // check username exited
            var isExited = await _unitOfWork.AccountRepository.CheckUserNameExited(userObject.UserName);

            if(isExited)
            {
                throw new Exception("Username exited please try again");
            }

            var newAccount = new Account
            {
                UserName = userObject.UserName,
                Password = userObject.Password.Hash()
            };

            await _unitOfWork.AccountRepository.AddAsync(newAccount);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
