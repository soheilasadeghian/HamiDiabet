using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace testwebapi
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId != null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            // Change authentication ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            var userId = long.Parse(context.Ticket.Identity.FindFirst(ClaimTypes.UserData).Value);

            var db = new DataAccessDataContext();
            var dt = new DateTime();
            dt = DateTime.Now;

            var user = db.userTbls.Single(c => c.ID == userId);
            user.lastActivityDate = dt;
            db.SubmitChanges();


            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {  
            // how to get additional parameters
            //var form = await context.Request.ReadFormAsync();
            //var key1 = form["my-very-special-key1"];

            var db = new DataAccessDataContext();

            var user = db.userTbls.SingleOrDefault(c => c.userName == context.UserName && c.password == context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "نام کاربری یا کلمه عبور صحیح نمی باشد");
                context.Rejected();
                return;
            }

            var identity = setClaimsIdentity(context, user);
            context.Validated(identity);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            context.Validated();
            return Task.FromResult(0);
        }

        private ClaimsIdentity setClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, userTbl user)
        { 
            var identity = new ClaimsIdentity(authenticationType: "JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            // to invalidate the token
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, user.SerialNumber));

            // custom data
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.ID.ToString()));

            //var roles = user.Roles;
            //foreach (var role in roles)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, role));
            //}
            return identity;
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var db = new DataAccessDataContext();
            var user = db.userTbls.Single(c => c.ID == long.Parse(context.Identity.FindFirst(ClaimTypes.UserData).Value));
            user.hashAccessToken = GetSha256Hash(context.AccessToken);
            db.SubmitChanges();
            
            return base.TokenEndpointResponse(context);
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
