using Gestor.Core.Application;
using Gestor.Core.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor.Controllers
{
    [ApiController, Route("v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountApplication accountApplication;

        public AccountController(AccountApplication accountApplication)
            => this.accountApplication = accountApplication;

        [HttpPost, Route("")]
        public async Task<IActionResult> Save(Account account)
        {
            var result = await accountApplication.Save(account);

            return Ok(result);
        }

        [HttpGet, Route("{id}"), Authorize("Bearer")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await accountApplication.Get(id);

            return Ok(result);
        }

        [HttpDelete, Route("{id}"), Authorize("Bearer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await accountApplication.Delete(id);

            return Ok();
        }
    }
}
