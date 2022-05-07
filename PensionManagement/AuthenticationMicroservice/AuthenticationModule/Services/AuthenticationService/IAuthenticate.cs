using AuthenticationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationModule.Services.AuthenticationService
{
    public interface IAuthenticate
    {
        TokenDetail Authentication(string userName, string Password);
    }
}
