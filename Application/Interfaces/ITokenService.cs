using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
        public interface ITokenService
        {
            string CreateJWTToken(IdentityUser user, List<string> roles);
        }

    
}
