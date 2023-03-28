using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edutools_api.Services.Jwt
{
    public interface IJwtService
    {
        string CreateJwtToken(string email);
    }
}
