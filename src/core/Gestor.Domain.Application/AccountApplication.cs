using Gestor.Core.Domain.Contracts.Repository;
using Gestor.Core.Domain.Contracts.Services;
using Gestor.Tools.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DO = Gestor.Core.Domain.Entities;
using DTO = Gestor.Core.Domain.DTO;

namespace Gestor.Core.Application
{
    public class AccountApplication : BaseApplication, IDisposable
    {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountService accountService;
        public AccountApplication(IAccountRepository accountRepository, IAccountService accountService) : base()
        {
            this.accountRepository = accountRepository;
            this.accountService = accountService;
        }

        public async Task<DTO.Account> Save(DTO.Account account)
        {
            account.Valid();

            var result = await accountService.Save(new DO.Account(account));

            account = new DTO.Account(result);

            return account;
        }

        public async Task<DTO.Account> Get(Guid id)
        {
            var result = await accountRepository.Get(id);

            if (result.IsNull())
                throw new ArgumentException($"Usuário {id} não encontrada.");

            return new DTO.Account(result);
        }

        public async Task<DTO.Account> GetLogin(DTO.Account account)
        {
            var result = await accountRepository.GetLogin(new DO.Account(account));

            if (result.IsNull())
                throw new ArgumentException($"Usuário não encontrada.");

            if (result.Login.IsNullOrWhiteSpace() || result.Password.IsNullOrWhiteSpace())
                throw new ArgumentException($"Usuário não tem permissão.");

            return account.Password == result.Password ? new DTO.Account(result) : null;
        }

        public async Task Delete(Guid id)
        {
            await Get(id);
            await accountRepository.Delete(id);
        }
    }
}
