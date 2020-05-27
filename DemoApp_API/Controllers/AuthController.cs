using AutoMapper;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        #region Field
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAccountService _accountService;
        #endregion

        #region Ctor

        public AuthController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
        }

        #endregion

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<BaseViewModel<TokenViewModel>> Login([FromBody]LoginViewModel request)
        {
            var entity = _accountService.Login(request);

            return Ok(entity);

        }

        #region Register

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<BaseViewModel<TokenViewModel>> Register([FromBody]RegisterViewModel request)
        {
            var entity = _accountService.Register(request);
            return Ok(entity);
        }
        #endregion

       
    }
}