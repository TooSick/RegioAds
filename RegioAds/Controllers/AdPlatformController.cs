using Microsoft.AspNetCore.Mvc;
using RegioAds.Application.Abstractions;

namespace RegioAds.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AdPlatformController : ControllerBase
    {
        private readonly IAdPlatformService _adPlatformService;


        public AdPlatformController(IAdPlatformService adPlatformService)
        {
            _adPlatformService = adPlatformService;
        }


        /// <response code="200">Data reloaded succesfuly</response>
        /// <response code="400">Invalid platform name</response>
        /// <response code="404">File with data not found</response>
        /// <response code="500">Unexpected exception</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Reload()
        {
            await _adPlatformService.ReloadTreeAsync();
            return Ok();
        }

        /// <response code="200">Platforms found</response>
        /// <response code="400">Invalid platform name</response>
        /// <response code="404">File with data not found</response>
        /// <response code="500">Unexpected exception</response>
        [HttpGet("[action]")]
        public async Task<IActionResult> FindPlatforms(string location)
        {
            var platforms = await _adPlatformService.FindPlatformsByLocationAsync(location);
            return Ok(platforms);
        }
    }
}
