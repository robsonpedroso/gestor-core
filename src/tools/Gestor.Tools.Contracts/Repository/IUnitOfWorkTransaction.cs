using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gestor.Tools.Contracts.Repository
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        bool IsConnectionOpen { get; }

        Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
