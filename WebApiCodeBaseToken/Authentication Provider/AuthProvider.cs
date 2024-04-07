using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApiCodeBaseToken.DAL;

namespace WebApiCodeBaseToken.Authentication_Provider
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if(ValidateUser.Login(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("username", context.UserName));
                if (context.UserName.StartsWith("Admin"))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                }
                

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
            return;
            }
        }
    }
}