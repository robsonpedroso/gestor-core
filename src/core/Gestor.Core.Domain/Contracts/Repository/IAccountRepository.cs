using Gestor.Core.Domain.Entities;
using Gestor.Tools.Contracts.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor.Core.Domain.Contracts.Repository
{
    public interface IAccountRepository : IRepository<Entities.Account>
    {
        Task<Account> GetLogin(Account account);
    }
}
