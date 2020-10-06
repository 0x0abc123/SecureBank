﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SecureBank.Authorization;
using SecureBank.Ctf.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SecureBank.Helpers.Authorization
{
    public class UserExtensions : IUserExtensions
    {
        private readonly ICookieService _cookieService;

        private readonly CtfOptions _ctfOptions;

        public UserExtensions(ICookieService cookieService, IOptions<CtfOptions> ctfOptions)
        {
            _cookieService = cookieService;

            _ctfOptions = ctfOptions.Value;
        }

        public string GetRole(HttpContext context)
        {
            Claim claim = ((ClaimsIdentity)context.User.Identity).FindFirst(CookieConstants.ROLE_CALIM_TYPE);
            if(claim == null && _ctfOptions.CtfChallengeOptions.MissingAuthentication)
            {
                IEnumerable<Claim> claims = _cookieService.GetClaims(context);

                claim = claims
                    .Where(x => x.Type == CookieConstants.ROLE_CALIM_TYPE)
                    .SingleOrDefault();
            }

            return claim?.Value;
        }

        public string GetUserName(HttpContext context)
        {
            Claim claim = ((ClaimsIdentity)context.User.Identity).FindFirst(CookieConstants.USERNAME_CALIM_TYPE);
            if (claim == null && _ctfOptions.CtfChallengeOptions.MissingAuthentication)
            {
                IEnumerable<Claim> claims = _cookieService.GetClaims(context);

                claim = claims
                    .Where(x => x.Type == CookieConstants.USERNAME_CALIM_TYPE)
                    .SingleOrDefault();
            }

            return claim?.Value;
        }

        public bool IsAuthenticated(HttpContext context)
        {
            Claim claim = ((ClaimsIdentity)context.User.Identity).FindFirst(CookieConstants.AUTHENTICATED_CALIM_TYPE);
            return claim != null;
        }
    }
}
