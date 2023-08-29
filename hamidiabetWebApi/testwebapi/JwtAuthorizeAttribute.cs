using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace HamiDiabetWebApi
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
       
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (skipAuthorization(actionContext))
            {
                return;
            }
            string accessToken = null;
            try
            {
                accessToken = actionContext.Request.Headers.Authorization.Parameter;
            }
            catch
            {
                // null token
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }
            if (string.IsNullOrWhiteSpace(accessToken) ||
                accessToken.Equals("undefined", StringComparison.OrdinalIgnoreCase))
            {
                // null token
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                // this is not our issued token
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                // this is not our issued token
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }

            var db = new DataAccessDataContext();
            var user = db.userTbls.Single(c => c.Id == long.Parse(userId));

            var serialNumber = user.serialNumber;
            if (serialNumber != serialNumberClaim.Value)
            {
                // user has changed its password/roles/stat/IsActive
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }
            

            if (!IsValidToken(accessToken, user))
            {
                // this is not our issued token
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }

            base.OnAuthorization(actionContext);
        }
        private bool IsValidToken(string accessToken, userTbl user)
        {
            var accessTokenHash = GetSha256Hash(accessToken);
            var userToken = user.hashAccessToken == accessTokenHash;
           
            return userToken;//userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        }
        private static bool skipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
        private string GetSha256Hash(string input)
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