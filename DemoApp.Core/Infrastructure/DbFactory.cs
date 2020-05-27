using DemoApp.Core.Data.Enity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Core.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DataContext dbContext;

        public DataContext Init()
        {
            return dbContext ?? (dbContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
