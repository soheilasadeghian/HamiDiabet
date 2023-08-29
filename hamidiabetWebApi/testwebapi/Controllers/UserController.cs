using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HamiDiabetWebApi.ClassCollection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HamiDiabetWebApi.Controllers
{
    [RoutePrefix("USER")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("SignUp")]
        public async Task<IHttpActionResult> GetSignUp()
        {
            var body = (await Request.Content.ReadAsFormDataAsync());

            string name = body["name"];
            string family = body["family"];
            long cityId = 0;
            string mobile = body["mobile"];
            string password = body["password"];
            bool subscribeNewsletter = true;

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(ClassCollection.Message.nameInvalid);
            }
            if (string.IsNullOrEmpty(family))
            {
                return BadRequest(ClassCollection.Message.familyInvalid);
            }
            if(!long.TryParse(body["cityId"],out cityId))
            {
                return BadRequest(ClassCollection.Message.cityInvalid);
            }
            if (string.IsNullOrEmpty(mobile))
            {
                return BadRequest(ClassCollection.Message.mobileInvalid);
            }
            if (!ClassCollection.Method.IsMobile(mobile))
            {
                return BadRequest(ClassCollection.Message.mobileInvalid);
            }
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest(ClassCollection.Message.passwordInvalid);
            }
            if (password.Length < 5 || password.Length >= 250)
            {
                return BadRequest(ClassCollection.Message.passwordInvalid);
            }
            if (!bool.TryParse(body["subscribeNewsletter"], out subscribeNewsletter))
            {
                return BadRequest(ClassCollection.Message.subscribnewsletterInvalid);
            }
            

            var db = new DataAccessDataContext();
            DateTime dt = new DateTime();
            dt = DateTime.Now;

            var userExist = db.userTbls.Any(c => c.mobile == mobile);
            if (userExist == true)
                return BadRequest(ClassCollection.Message.mobileExist);

            var user = new userTbl();
            user.name = name;
            user.family = family;
            user.mobile = mobile;
            user.password = Method.md5(password);
            user.registerDate = dt;
            user.lastActivityDate = dt;
            user.isConfirm = false;
            user.status = 0;
            user.roleId = 1;
            user.subscribeNewsletter = subscribeNewsletter;
            user.cityId = 1;
            user.serialNumber = Guid.NewGuid().ToString("N");
            db.userTbls.InsertOnSubmit(user);
            db.SubmitChanges();

            return Ok(ClassCollection.Message.successfull);
        }

        [JwtAuthorize]
        [Route("SignOut")]
        [HttpPost]
        public IHttpActionResult Signout()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            var db = new DataAccessDataContext();

            var user = db.userTbls.Single(x => x.Id == long.Parse(userId));
            user.hashAccessToken = "";
            user.hashRefreshToken = "";
            user.ticket = "";
            db.SubmitChanges();

            return Ok(ClassCollection.Message.successfull);
        }
    }



}


