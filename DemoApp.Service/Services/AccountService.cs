
using AutoMapper;
using DemoApp.Core.Configs;
using DemoApp.Core.Constants;
using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;
using DemoApp.Service.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DemoApp.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IAccountRepository accountRepository, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _repository = accountRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public BaseViewModel<AccountViewPage> getInfo()
        {
            var entity = _repository.GetById(_repository.GetUsername());
            if (entity == null)
            {
                return new BaseViewModel<AccountViewPage>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = ErrMessageConstants.ACCOUNT_NOTFOUND,
                    Code = ErrMessageConstants.NOTFOUND,
                    Data = null
                };
            }
            return new BaseViewModel<AccountViewPage>
            {
                StatusCode = HttpStatusCode.OK,
                Description = null,
                Code = MessageConstants.SUCCESS,
                Data = _mapper.Map<AccountViewPage>(entity)
            };
        }

        public BaseViewModel<TokenViewModel> Login(LoginViewModel user)
        {
            var entity = _repository.GetById(user.Username);
            if (entity == null)
            {
                return new BaseViewModel<TokenViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = ErrMessageConstants.ACCOUNT_NOTFOUND,
                    Code = ErrMessageConstants.ACCOUNT_NOTFOUND,
                    Data = null
                };

            }
            if (!SaltHashPassword.Verify(entity.SaltPassword, entity.HashPassword, user.Password))
            {
                return new BaseViewModel<TokenViewModel>
                {
                    StatusCode = HttpStatusCode.PreconditionFailed,
                    Description = ErrMessageConstants.INVALID_ACCOUNT,
                    Code = ErrMessageConstants.INVALID_ACCOUNT,
                    Data = null
                };
            }
            var result = authenticate(entity);
            return new BaseViewModel<TokenViewModel>
            {
                StatusCode = HttpStatusCode.OK,
                Description = null,
                Code = MessageConstants.SUCCESS,
                Data = result
            };
        }

        public BaseViewModel<TokenViewModel> Register(RegisterViewModel user)
        {

            var check = _repository.GetById(user.Username);
            if (check != null)
            {
                return new BaseViewModel<TokenViewModel>()
                {
                    Data = null,
                    Code = MessageConstants.FAILURE,
                    Description = ErrMessageConstants.ACCOUNT_EXISTED,
                    StatusCode = HttpStatusCode.PreconditionFailed
                };
            }
            var entity = new Account
            {
                Username = user.Username,
                Fullname = user.FullName,
                Email = user.Email,
                Avartar = user.Avartar,
                Phone = user.Phone

            };
            var temp = new SaltHashPassword(user.Password);
            entity.SaltPassword = temp.Salt;
            entity.HashPassword = temp.Hash;
            entity.Role = Role.User;
            entity.IsDelete = false;

            _repository.Add(entity);
            Save();
            var result = authenticate(entity);
            return new BaseViewModel<TokenViewModel>()
            {
                Data = result,
                Code = MessageConstants.SUCCESS,
                Description = null,
                StatusCode = HttpStatusCode.Created
            };
        }


        private TokenViewModel authenticate(Account account)
        {

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Fullname),
                     new Claim(ClaimTypes.Role, account.Role),
                     new Claim (Constant.CLAIM_USERNAME, account.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenViewModel
            {
                Access_token = tokenHandler.WriteToken(token),
                Expires_in = DateTime.UtcNow.AddMinutes(30),
                Roles = account.Role,
                Avartar = account.Avartar,
                FullName = account.Fullname
            };
        }
        private void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
