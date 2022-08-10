using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public ActionResult CheckJwt()
        {
            return Ok();
        }

        [HttpPost("google")]
        public ActionResult Google([FromBody] PostGoogleForm body)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(body.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;

            var user = _authService.Authenticate(payload);

            return Ok(new
            {
                token = _jwtService.CreateJwt(user)
            });
        }

        public class PostGoogleForm
        {
            public string? tokenId { get; set; }
        }
    }
}