using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        #region Field
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAccountService _accountService;
        #endregion

        #region Ctor

        public AccountController(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
        }

        #endregion

        #region Get Infomation

        [HttpGet("GetInfo")]
        public ActionResult<BaseViewModel<AccountViewPage>> GetInfo()
        {
            var entity = _accountService.getInfo();
            return Ok(entity);
        }
        #endregion
    }

}