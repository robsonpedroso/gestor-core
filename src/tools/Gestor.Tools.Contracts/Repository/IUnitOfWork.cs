using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor.Tools.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork Open();
        IUnitOfWorkTransaction BeginTransaction();
        object GetSession();
    }
}
