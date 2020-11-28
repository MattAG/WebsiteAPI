using Microsoft.AspNetCore.Mvc;
using Website.Data.Entities;
using Website.Services.Services;

namespace Website.API.Controllers
{
    /// <summary>
    /// Handles authentication requests.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("[controller]")]
    [ApiController]
    public sealed class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Gets the authentication service.
        /// </summary>
        /// <value>
        /// The authentication service.
        /// </value>
        private AuthenticationService AuthenticationService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationController(AuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Authentication token or 401.</returns>
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            IActionResult result = new UnauthorizedResult();

            if (AuthenticationService.TryAuthenticateUser(user, out string token))
            {
                result = Ok(token);
            }

            return result;
        }
    }
}
