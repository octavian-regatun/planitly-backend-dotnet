using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planitly.Backend.Models;
using Planitly.Backend.Services;

namespace Planitly.Backend.Controllers
{
    [Authorize]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpGet("current")]
        public ActionResult<User?> GetCurrentUser()
        {
            return _authService.GetLoggedUser(User);
        }
    }
}