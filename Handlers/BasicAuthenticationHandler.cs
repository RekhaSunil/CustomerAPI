using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CustomerAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CustomerAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {


        readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IUserService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }
       
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //if(!Request.Headers.ContainsKey("Authorization"))
            //    return AuthenticateResult.Fail("Authorization was not found");
            //try
            //{
            //    var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            //    var bytes = Convert.FromBase64String(authenticationHeader.Parameter);
            //    string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
            //    string Username = credentials[0];
            //    string Password = credentials[1];
            //    if (Username != "admin" && Password != "admin")
            //    {
            //        return AuthenticateResult.Fail("Invalid username or password");
            //    }
            //    else
            //    {
            //        var claims = new[]
            //        {
            //            new Claim(ClaimTypes.Name,Username)
            //        };

            //        var identity = new ClaimsIdentity(claims, Scheme.Name);
            //        var principal = new ClaimsPrincipal(identity);
            //        var ticket = new AuthenticationTicket(principal, Scheme.Name);
            //        // return AuthenticateResult.Success(ticket);
            //        return AuthenticateResult.Success(ticket);

            //    }
            //}
            //catch (Exception)
            //{

            //    return AuthenticateResult.Fail("Error occured");
            //}
            //return AuthenticateResult.Fail("Need to implement");
            string username = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                if (!_userService.ValidateCredentials(username, password))
                    throw new ArgumentException("Invalid credentials");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
