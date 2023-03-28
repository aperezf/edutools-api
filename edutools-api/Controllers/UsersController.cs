using edutools_api.Models;
using edutools_api.services.Services;
using edutools_api.store.Edutools;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace edutools_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public IJwtService _JwtService { get; set; }
        public EdutoolsContext _EdutoolsContext { get; set; }
        public UsersController(EdutoolsContext edutoolsContext, IJwtService jwtService)
        {
            _EdutoolsContext = edutoolsContext;
            _JwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> CreateUser(CreateUserDTO user)
        {
            try
            {
                // Crear usuario
                await _EdutoolsContext.AddAsync(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
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
        public async Task<object> Login(LoginDTO login)
        {
            if (login == null) return BadRequest();
            var user = await _EdutoolsContext.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
            if (user == null) return Unauthorized("Usuario incorrecto");
            if (user.Password != login.Password) return Unauthorized("Usuario incorrecto");
            return Ok(new
            {
                At = _JwtService.CreateJwtToken(user.Email)
            });
        }


        
    }
}
