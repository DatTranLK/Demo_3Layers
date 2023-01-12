using Enities;
using Enities.Models;
using Microsoft.AspNetCore.Http;
using Repositories.IRepository;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public async Task<ServiceResponse<Member>> Authentication(IdToken idToken)
        {
            var mem = await _authenticationRepository.Authentication(idToken);
            return new ServiceResponse<Member>
            { 
                Data = mem,
                Message = "Successfully",
                Success = true,
                StatusCode = 200
            };
        }
    }
}
