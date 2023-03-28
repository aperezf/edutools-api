using edutools_api.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edutools_api.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO login);
        Task<bool> SignIn(SignInRequestDTO signIn);
    }
}
