using Enities;
using Enities.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Repositories.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly FstoreDBContext _dbContext;
        private readonly IConfiguration _configuration;


        public AuthenticationRepository(FstoreDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<Member> Authentication(IdToken idToken)
        {
            Member user = new Member();
            FirebaseToken decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                        .VerifyIdTokenAsync(idToken.Name);
            Console.WriteLine("Decoded token" + decodedToken);
            string uid = decodedToken.Uid;
            var authUser = new FirebaseAuthProvider(new FirebaseConfig(_configuration.GetSection("API_key").Value));
            var auth = authUser.GetUserAsync(idToken.Name);

            user.MemberId = 12;

            user.CompanyName = auth.Result.DisplayName;
            user.Email = auth.Result.Email;
            return user;
        }
    }
}
