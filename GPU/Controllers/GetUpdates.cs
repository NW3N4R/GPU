using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    [Route("up")]
    [ApiController]
    public class GetUpdates : ControllerBase
    {
        [HttpGet("props/")]
        public async Task<IActionResult> UpdateProperties([FromBody] WebProperties? prop)
        {
            if (prop != null)
            {
                await WebPropsServices.LoadProps(prop.id, prop.propType, true);
                return Ok("Commands For Single Row Captured!");
            }
            else
            {
                await WebPropsServices.LoadProps();
                return Ok("Commands For Whole Props Captured!");

            }
        }
        [HttpGet("usrs/")]
        public async Task<IActionResult> UpdateUsrs()
        {
            await ManagerServices.LoadManagers();
            return Ok("Capture!");
        }
    }
}
