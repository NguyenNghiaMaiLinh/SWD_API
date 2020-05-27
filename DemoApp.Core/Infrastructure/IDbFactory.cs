using DemoApp.Core.Data.Enity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Core.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DataContext Init();
    }
}
