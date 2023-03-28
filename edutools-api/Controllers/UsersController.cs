using edutools_api.Models;
using edutools_api.Services.Jwt;
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
        public UsersController(
            EdutoolsContext edutoolsContext, 
            IJwtService jwtService)
        {
            _EdutoolsContext = edutoolsContext;
            _JwtService = jwtService;
        }        
    }
}
