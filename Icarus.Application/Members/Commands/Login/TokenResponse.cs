using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.Commands.Login;

public sealed class TokenResponse
{
    public TokenResponse(string token) => Token = token;

    public string Token { get; }
}
