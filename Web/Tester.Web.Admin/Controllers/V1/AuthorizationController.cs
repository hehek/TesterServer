using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exception;
using Tester.Auth.Contracts;
using Tester.Dto;
using Tester.Dto.Users;
using Tester.Web.Admin.Controllers.Base;

namespace Tester.Web.Admin.Controllers.V1
{
    public class AuthorizationController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthorizationController([NotNull] IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseDto<Guid>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> Authorization([FromBody] AuthenticateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new {message = "Login or password is incorrect"});

            try
            {
                var token = await _authService.Authenticate(model.Login, model.Password).ConfigureAwait(false);
                SetTokenToCookie(token);
                return Ok(token);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        private void SetTokenToCookie(string token)
        {
            HttpContext.Response.Cookies.Append("access_token", token);
        }
    }
}