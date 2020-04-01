using Gestor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Core.Domain.Contracts.Services
{
    public interface IAccountService
    {
        Task<Account> Save(Account account);
    }
}
