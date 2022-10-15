using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planitly.Backend.Services;

namespace Planitly.Backend.Controllers
{
    [Route("location")]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{ip}")]
        public async Task<ActionResult<LocationFromIpResponse?>> GetLocatioFromIp(string ip)
        {
            return await _locationService.GetLocationFromIp(ip);
        }
    }
}