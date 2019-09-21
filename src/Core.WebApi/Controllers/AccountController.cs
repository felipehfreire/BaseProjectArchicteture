using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Domain.Bus;
using Core.Domain.Notifications;
using CrossCutting.Core.Identity.Models;
using Eventos.IO.Infra.CrossCutting.Identity.Authorization;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Eventos.IO.Infra.CrossCutting.Identity.Models.AccountViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Core.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IMediatorHandler _mediator;

        public AccountController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             ILoggerFactory loggerFactory,
            TokenDescriptor tokenDescriptor,
            IMediatorHandler mediator
            ) : base(notifications,user,mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
             _mediator = mediator;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _tokenDescriptor = tokenDescriptor;

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, int version)
        {
            if (!ModelState.IsValid)
            {
                NotifyErrorInvalidModel();
                return await Response(model);
            }
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Senha);
            if (result.Succeeded)
            {
                if (!await IsValidOperation())
                {
                    await _userManager.DeleteAsync(user);
                    return await Response(model);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");
                var response = await GenerateUserToken(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return await Response(response);
               // return await Response(model);
            }
            NotifyError(result.ToString(), "Falha ao RealizarLogin!");
            return await Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyErrorInvalidModel();
                return await Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                //var response = GerarTokenUsuario(model);
                //return Response(response);
                var response = await GenerateUserToken(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return await Response(response);
            }

            NotifyError(result.ToString(), "Falha ao realizar o login");
            return await Response(model);
        }

        

        private async Task<object> GenerateUserToken(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            //var orgUser = _organizadorRepository.ObterPorId(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    //nome = orgUser.Nome,
                    //email = orgUser.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;

        }

        private static long ToUnixEpochDate(DateTime date)
     => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
