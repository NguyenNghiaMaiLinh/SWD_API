using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DemoApp.Repositories.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private DataContext _dataContext;
        public ProductRepository(IDbFactory dbFactory, IServiceProvider serviceProvider, DataContext dataContext) : base(dbFactory, serviceProvider)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Product> getAllProduct(int? pageIndex, int? pageSize, string search)
        {
            if (search == null)
            {
                search = "";
            }
            var par1 = new SqlParameter("@PageIndex", pageIndex);
            var par2 = new SqlParameter("@PageSize", pageSize);
            var par4 = new SqlParameter("@Search", search);

            var result = _dataContext.Product.FromSql("getAllProduct @PageIndex, @PageSize, @Search", par1, par2, par4).ToList(); ;
            return result;
        }
    }
}
