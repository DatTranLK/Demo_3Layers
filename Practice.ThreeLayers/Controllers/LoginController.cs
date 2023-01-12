using Enities;
using Enities.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IService;
using System;
using System.Threading.Tasks;

namespace Practice.ThreeLayers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Member>>> Authentication(IdToken idToken)
        { 
            var res = await _authenticationService.Authentication(idToken);
            return Ok(res);
        }
    }
}
