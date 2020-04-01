using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gestor.Core.Domain.Contracts.Repository;
using Gestor.Core.Domain.Contracts.Services;
using Gestor.Core.Domain.Entities;
using Gestor.Tools.Utils.Extensions;

namespace Gestor.Core.Domain.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository) : base()
            => this.accountRepository = accountRepository;
        public async Task<Account> Save(Account account)
        {
            if (!Enum.IsDefined(typeof(TypeAccount), account.Type) || account.Type == TypeAccount.None)
                account.Type = TypeAccount.Client;

            var result = await accountRepository.Save(account);

            return result;
        }
    }
}
