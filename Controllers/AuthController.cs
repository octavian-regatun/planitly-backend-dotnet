using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Planitly.Backend.Services;

namespace Planitly.Backend.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IJwtService _jwtService;

        public AuthController(IAuthService authService, IConfiguration config, IJwtService jwtService)
        {
            this._authService = authService;
            this._jwtService = jwtService;
        }

        [HttpPost("google")]
        public ActionResult Google([FromBody] string tokenId)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;

            var user = _authService.Authenticate(payload);

            return Ok(new
            {
                token = _jwtService.CreateJwt(user)
            });
        }
    }
}