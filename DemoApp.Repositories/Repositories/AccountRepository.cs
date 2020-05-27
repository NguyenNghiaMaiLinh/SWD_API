using DemoApp.Core.Data.Enity;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Repositories.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private DataContext _dataContext;
        public AccountRepository(IDbFactory dbFactory, IServiceProvider serviceProvider, DataContext dataContext) : base(dbFactory, serviceProvider)
        {
            _dataContext = dataContext;
        }
    }
}
