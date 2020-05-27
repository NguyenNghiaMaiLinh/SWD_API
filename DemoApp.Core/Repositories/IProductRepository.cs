using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using System.Collections.Generic;

namespace DemoApp.Core.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> getAllProduct(int? pageIndex, int? pageSize, string search);
    }
}
