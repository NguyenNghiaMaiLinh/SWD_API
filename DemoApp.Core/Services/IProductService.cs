using DemoApp.Core.ViewModel;

namespace DemoApp.Core.Services
{
    public interface IProductService
    {
        BaseViewModel<PagingResult<ProductViewPage>> getAllProduct(BasePagingRequestViewModel request);
    }
}
