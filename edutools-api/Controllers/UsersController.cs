using edutools_api.Models;
using edutools_api.store.Edutools;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edutools_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        public EdutoolsContext _EdutoolsContext { get; set; }
        public UsersController(EdutoolsContext edutoolsContext)
        {
                this._EdutoolsContext = edutoolsContext;
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

                return Ok(true);
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
