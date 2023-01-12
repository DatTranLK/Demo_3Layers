using Enities;
using Enities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<Member> Authentication(IdToken idToken);
    }
}
