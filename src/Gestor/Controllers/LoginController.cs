using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Gestor.Api.Security;
using Gestor.Core.Application;
using Gestor.Core.Domain.DTO;
using Gestor.Tools.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Gestor.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly AccountApplication accountApplication;
        private readonly TokenConfigurations tokenConfigurations;

        public LoginController(AccountApplication accountApplication,
            TokenConfigurations tokenConfigurations
            )
        {
            this.accountApplication = accountApplication;
            this.tokenConfigurations = tokenConfigurations;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Account account)
        {
            if (account != null && !String.IsNullOrWhiteSpace(account.Login))
            {
                var exists = await accountApplication.GetLogin(account);

                if (!exists.IsNull())
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(exists.Name.AsString() ?? string.Empty, "Login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim("email", exists.Email ?? string.Empty),
                            new Claim("usrid", exists.Id.AsString()  ?? string.Empty),
                            new Claim("name", exists.Name.AsString() ?? string.Empty)
                        }
                    );

                    DateTime createat = DateTime.Now;
                    DateTime expired = createat + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfigurations.Key)), SecurityAlgorithms.HmacSha256Signature),
                        Subject = identity,
                        NotBefore = createat,
                        Expires = expired
                    });
                    var token = handler.WriteToken(securityToken);

                    return Ok(new
                    {
                        authenticated = true,
                        created = createat.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = expired.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        message = "OK"
                    });
                }
            }

            return Ok(new
            {
                authenticated = false,
                message = "Usuário ou senha inválido"
            });
        }
    }
}