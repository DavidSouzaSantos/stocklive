using AutoMapper;
using Domain.Enums;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                //return Ok(await _userManager.FindByIdAsync(userId));
                return Ok(new { user = new UserDto()});
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        public UserController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);

                var userToReturn = _mapper.Map<UserDto>(user);

                if (result.Succeeded) return Created("GetUser", userToReturn);

                return BadRequest(result.Errors);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto pUserLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(pUserLogin.Username);

                if (user == null || user.Status == StatusEnum.Inactive)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var result = await _signInManager.CheckPasswordSignInAsync(user, pUserLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == pUserLogin.Username.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    var token = TokenService.GenerateToken(_config, appUser);

                    return Ok(new
                    {
                        user,
                        token
                    });
                }

                return Unauthorized();

            }
            catch (Exception ex)
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
