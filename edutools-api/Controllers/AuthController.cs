

using edutools_api.Models;
using edutools_api.Models.Auth;
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
        public IJwtService _JwtService { get; set; }
        public EdutoolsContext _EdutoolsContext { get; set; }
        public AuthController(
            EdutoolsContext edutoolsContext,
            IJwtService jwtService)
        {
            _EdutoolsContext = edutoolsContext;
            _JwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public async Task<object> SignIn(SignInRequestDTO signIn)
        {
            try
            {
                // Crear usuario
                await _EdutoolsContext.AddAsync(new User
                {
                    Name = signIn.Name,
                    Email = signIn.Email,
                    Password = signIn.Password
                });
                await _EdutoolsContext.SaveChangesAsync();

                // Dale token
                //return Ok(new
                //{
                //    At = _JwtService.CreateJwtToken(user.Email),
                //});
                // Enviar correo con link de confirmacion 
                return Ok();
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
            var user = await _EdutoolsContext.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
            if (user == null || user.Password != login.Password) return Unauthorized("Usuario incorrecto");
            return Ok(new
            {
                At = _JwtService.CreateJwtToken(user.Email)
            });
        }


    }
}
