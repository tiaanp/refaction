using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace refactor_me.Token
{
    public class TokenProviders : IAuthenticationTokenProvider
    {


        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var loginid = context.Ticket.Properties.Dictionary["as:login_id"];

            if (string.IsNullOrEmpty(loginid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:loginRefreshTokenLifeTime");

            var token = new Domain.Tokens.Token()
            {
                Id = HashEncryption.GetHash(refreshTokenId),
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();



            context.SetToken(refreshTokenId);


        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = HashEncryption.GetHash(context.Token);


            //var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

            //if (refreshToken != null)
            //{
            //    //Get protectedTicket from refreshToken class
            //    context.DeserializeTicket(refreshToken.ProtectedTicket);
            //    var result = await _repo.RemoveRefreshToken(hashedTokenId);

            //}
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class HashEncryption
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
