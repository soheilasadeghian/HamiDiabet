using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using System.Web.Routing;

[assembly: OwinStartup(typeof(HamiDiabetWebApi.App_Start.Startup))]

namespace HamiDiabetWebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);


        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/user/signin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //AccessTokenExpireTimeSpan = new TimeSpan(0,1,0),
                Provider = new SimpleAuthorizationServerProvider(),
                AccessTokenFormat = new CustomJwtFormat("localhost"),
                RefreshTokenProvider=new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                     {
                        new SymmetricKeyIssuerSecurityTokenProvider("localhost","QkU0QUMwNUNBODEyRDlGNTY0QTc3RUQ1MkE1NTY4RTQ4QzlDMDA3MTE1QTE2NEYyRUFFM0QzRjQzREQzNDVFMA==")
                     }
            });

        }
    }
}
