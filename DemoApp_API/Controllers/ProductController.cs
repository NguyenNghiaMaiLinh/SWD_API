using AutoMapper;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IServiceProvider serviceProvider)
        {
            _productService = serviceProvider.GetRequiredService<IProductService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

        }

        #region get All Product
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<BaseViewModel<PagingResult<ProductViewPage>>> getAllProduct([FromQuery] BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();

            var result = _productService.getAllProduct(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        #endregion
    }
}