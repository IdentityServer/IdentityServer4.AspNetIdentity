// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddAspNetIdentity<TUser>(this IIdentityServerBuilder builder)
            where TUser : class
        {
            return builder.AddAspNetIdentity<TUser>("Identity.Application");
        }

        public static IIdentityServerBuilder AddAspNetIdentity<TUser>(this IIdentityServerBuilder builder, string authenticationScheme)
            where TUser : class
        {
            builder.Services.Configure<IdentityServerOptions>(options =>
            {
                options.Authentication.AuthenticationScheme = authenticationScheme;
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.AuthenticationScheme = authenticationScheme;
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;

                if (options.OnSecurityStampRefreshingPrincipal == null)
                {
                    options.OnSecurityStampRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
                }
            });

            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator<TUser>>();
            builder.AddProfileService<ProfileService<TUser>>();

            return builder;
        }
    }
}
