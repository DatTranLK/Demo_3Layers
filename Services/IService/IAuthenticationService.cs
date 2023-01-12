using Enities;
using Enities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<Member>> Authentication(IdToken idToken);
    }
}
