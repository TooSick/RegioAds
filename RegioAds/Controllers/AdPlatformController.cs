using Microsoft.AspNetCore.Mvc;
using RegioAds.Application.Abstractions;

namespace RegioAds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdPlatformController : ControllerBase
    {
        private readonly IAdPlatformService _adPlatformService;


        public AdPlatformController(IAdPlatformService adPlatformService)
        {
            _adPlatformService = adPlatformService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Reload()
        {
            await _adPlatformService.ReloadTreeAsync();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FindPlatforms(string location)
        {
            var platforms = await _adPlatformService.FindPlatformsByLocationAsync(location);
            return Ok(platforms);
        }
    }
}
