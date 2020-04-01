using Gestor.Core.Domain.Contracts.Repository;
using Gestor.Core.Domain.Entities;
using Gestor.Core.Infra.Providers;
using Gestor.Tools.Contracts.Repository;
using Gestor.Tools.Utils.Extensions;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO = Gestor.Core.Domain.Entities;
using DTO = Gestor.Core.Domain.DTO;

namespace Gestor.Core.Infra.Repository
{
    public class AccountRepository : NHBaseRepository<DO.Account>, IAccountRepository
    {
        public AccountRepository(IUnitOfWork uow) : base(uow) { }

        public async Task<Account> GetLogin(Account account)
        {
            var result = Session.Query<DO.Account>()
                .FirstOrDefault(x => x.Login == account.Login);

            return result;
        }
    }
}