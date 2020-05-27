using AutoMapper;
using DemoApp.Core.Configs;
using DemoApp.Core.Constants;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using DemoApp.Core.Services;
using DemoApp.Core.ViewModel;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DemoApp.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IProductRepository accountRepository, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _repository = accountRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public BaseViewModel<PagingResult<ProductViewPage>> getAllProduct(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<ProductViewPage>>();

            var data = _repository.getAllProduct(pageIndex, pageSize, request.Search).ToList();
            if (data == null || data.Count == 0)
            {
                result.StatusCode = HttpStatusCode.NotFound;
                result.Description = MessageConstants.NO_RECORD;
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {
                result.StatusCode = HttpStatusCode.OK;
                result.Description = null;
                result.Code = MessageConstants.SUCCESS;
                var pageSizeReturn = pageSize;
                if (data.Count < pageSize)
                {
                    pageSizeReturn = data.Count;
                }

                result.Data = new PagingResult<ProductViewPage>
                {
                    Results = _mapper.Map<IEnumerable<ProductViewPage>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = data.Count()
                };
            }

            return result;
        }
    }
}
