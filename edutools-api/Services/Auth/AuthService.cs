using edutools_api.Models.Auth;
using edutools_api.Services.Jwt;
using edutools_api.store.Edutools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edutools_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        public readonly EdutoolsContext _DbContext;
        public readonly IJwtService _JwtService;
        public AuthService(
            EdutoolsContext dbContext,
            IJwtService jwtService)
        {
            _DbContext = dbContext;
            _JwtService = jwtService;
        }

        

        public async Task<LoginResponseDTO> Login(LoginRequestDTO login)
        {
            // Validacion de Input
            if (login == null) throw new Exception("Login inválido");
            // Usuario Existe?
            var user = await _DbContext.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
            // Contraseña coincide?
            if (user == null || user.Password != login.Password) throw new Exception("Login inválido");
            return new LoginResponseDTO
            {
                At = _JwtService.CreateJwtToken(user.Email)
            };
            // Generar AT y RT
        }

        public async Task<bool> SignIn(SignInRequestDTO signIn)
        {
            // Validacion Input
            if (signIn == null) return false;
            // Insertar registro
            await _DbContext.AddAsync(new User
            {
                Name = signIn.Name,
                Email = signIn.Email,
                Password = signIn.Password
            });
            await _DbContext.SaveChangesAsync();
            // Enviar email para activar cuenta
            // Retornar bool
            return true;
        }
    }
}
