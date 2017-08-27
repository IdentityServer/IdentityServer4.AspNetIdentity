﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace IdentityServer4.AspNetIdentity
{
    public class ProfileService<TUser> : IProfileService
        where TUser : class
    {
        private readonly IUserClaimsPrincipalFactory<TUser> _claimsFactory;
        private readonly UserManager<TUser> _userManager;

        public ProfileService(UserManager<TUser> userManager,
            IUserClaimsPrincipalFactory<TUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var requestedClaims = context.RequestedClaimTypes.ToList();
            var claims = principal.Claims.ToList();
            context.IssuedClaims = claims.FindAll(c => requestedClaims.Contains(c.Type));
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
