using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
