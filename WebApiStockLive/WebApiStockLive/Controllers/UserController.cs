using Domain.Enums;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApiStockLive.Dtos;
using WebApiStockLive.Services;

namespace WebApiStockLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration pConfig)
        {
            _config = pConfig;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto pUserLogin)
        {
            try
            {
                var user = new User();

                if (user == null || user.Status == StatusEnum.Inactive)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(_config, user);


                return Ok(new
                {
                    user = user,
                    token = token
                });

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated()
        {
            return string.Format("Autenticado - {0}", User.Identity.Name);
        }
    }
}
