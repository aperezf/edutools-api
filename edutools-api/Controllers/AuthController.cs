

using edutools_api.Models;
using edutools_api.Models.Auth;
using edutools_api.Services.Auth;
using edutools_api.Services.Jwt;
using edutools_api.store.Edutools;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace edutools_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IJwtService _JwtService;
        public readonly IAuthService _AuthService;
        public AuthController(
            IJwtService jwtService,
            IAuthService authService)
        {
            _JwtService = jwtService;
            _AuthService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public async Task<object> SignIn(SignInRequestDTO signIn)
        {
            try
            {
                if (await _AuthService.SignIn(signIn))
                {
                    return Ok();
                }
                return BadRequest(new
                {
                    Message = "Ha ocurrido un error al inscribir al usuario."
                });
            }
            catch (UniqueConstraintException)
            {
                return BadRequest(new
                {
                    Message = "Ya existe una cuenta con este correo."
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<object> Login(LoginRequestDTO login)
        {
            if (login == null) return BadRequest();
            return Ok(_AuthService.Login(login));
        }


    }
}
