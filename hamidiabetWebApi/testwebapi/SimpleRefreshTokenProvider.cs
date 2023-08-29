using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HamiDiabetWebApi
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context).RunSynchronously();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            var now = DateTime.UtcNow;
            var ownerUserId = context.Ticket.Identity.FindFirst(ClaimTypes.UserData).Value;

            var db = new DataAccessDataContext();

            var user = db.userTbls.Single(c => c.Id == long.Parse(ownerUserId));

            user.hashRefreshToken = GetSha256Hash(refreshTokenId);

            context.Ticket.Properties.IssuedUtc = now;
            context.Ticket.Properties.ExpiresUtc = now.AddMinutes(Convert.ToDouble(10));

            user.ticket = context.SerializeTicket();
            db.SubmitChanges();

            //delete all expire token
          

            context.SetToken(refreshTokenId);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context).RunSynchronously();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var db = new DataAccessDataContext();
            var hashedTokenId = GetSha256Hash(context.Token);
            var user = db.userTbls.SingleOrDefault(c => c.hashRefreshToken == hashedTokenId);
            if (user != null)
            {
                context.DeserializeTicket(user.ticket);
                //_tokenStoreService().DeleteToken(hashedTokenId);
            }
        }
        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}