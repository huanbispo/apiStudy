using System.Net;
using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Dtos;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto user, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid || user == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await service.FindByLogin(user);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
