using edutools_api.Models;
using edutools_api.services;
using edutools_api.services.Services;
using edutools_api.store.Edutools;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<object> CreateUser(NewUser user)
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
                return Ok(new
                {
                    At = _JwtService.CreateJwtToken(user.Email),
                });
            }
            catch (UniqueConstraintException ex)
            {
                return BadRequest(new
                {
                    Message = "Ya existe una cuenta con este correo."
                });
            }
        }


        
    }
}
