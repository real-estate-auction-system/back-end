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

        public async Task<JwtResponse> LoginAsync(UserLoginDTO userObject)
        {
            var user = await _unitOfWork.AccountRepository.GetUserByUserNameAndPasswordHash(userObject.UserName, userObject.Password.Hash());
            return new JwtResponse
            {
                token = user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime()),
                role = user.RoleId
            };
           
        }

        public async Task RegisterAsync(UserRegisterDTO userObject)
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
                Password = userObject.Password.Hash(),
                DoB = userObject.DoB,
                Email = userObject.Email,
                FullName = userObject.FullName,
                Gender = userObject.Gender,
                Phone = userObject.Phone,
                RoleId = 2,
            };

            await _unitOfWork.AccountRepository.AddAsync(newAccount);
            await _unitOfWork.SaveChangeAsync();
        
        }

        public async Task<Pagination<AccountResponse>> GetAccounts(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _unitOfWork.AccountRepository.ToPagination(pageIndex, pageSize);
                List<AccountResponse> items = new List<AccountResponse>();
                foreach (Account account in response.Items)
                {
                    items.Add(_mapper.Map<AccountResponse>(account));
                }
                var pagination = new Pagination<AccountResponse>()
                {
                    Items = items,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = response.TotalItemsCount,
                };
                return pagination;
            }
            catch (Exception ex)
            {
                throw new Exception("Get list account error!");
            }
        }

        public async Task<AccountResponse> GetAccountById(int id)
        {
            try
            {
                var response = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new Exception($"Not found account with id {id.ToString()}");
                }
                return _mapper.Map<AccountResponse>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Get account by id error!");
            }
        }

        public async Task<AccountResponse> UpdateAccount(int id, UpdateAccountRequest request)
        {
            try
            {
                Account a = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (a == null)
                {
                    throw new Exception($"Not found account with id {id.ToString()}");
                }
                var existingUserName = _unitOfWork.AccountRepository.CheckUserNameExited(request.UserName);
                if (existingUserName.Equals(true))
                {
                    throw new Exception("UserName has already been taken");
                }

                _mapper.Map<UpdateAccountRequest, Account>(request, a);
                _unitOfWork.AccountRepository.Update(a);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AccountResponse>(a);
            }
            catch (Exception ex)
            {
                throw new Exception("Update account error!");
            }
        }

        public async Task<List<AccountResponse>> GetAllAccounts()
        {
            try
            {
                var response = await _unitOfWork.AccountRepository.GetAllAsync();
                List<AccountResponse> items = new List<AccountResponse>();
                if (response == null)
                {
                    throw new Exception("List account is empty!");
                }
                else
                {
                    foreach (var a in response)
                    {
                        items.Add(_mapper.Map<AccountResponse>(a));
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get list account error!");
            }
        }
    }
}
