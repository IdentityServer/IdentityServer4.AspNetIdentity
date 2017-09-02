// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerIdentityBuilderExtensions
    {
        public static IdentityBuilder AddIdentityServer(this IdentityBuilder builder)
        {
            var factoryInterfaceType = typeof(IUserClaimsPrincipalFactory<>);
            factoryInterfaceType = factoryInterfaceType.MakeGenericType(builder.UserType);
            var factoryClassType = typeof(UserClaimsFactory<,>);
            factoryClassType = factoryClassType.MakeGenericType(builder.UserType, builder.RoleType);
            builder.Services.AddScoped(factoryInterfaceType, factoryClassType);

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            });

            builder.Services.Configure<SecurityStampValidatorOptions>(opts =>
            {
                opts.OnRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
            });

            builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, cookie =>
            {
                // we need to disable to allow iframe for authorize requests
                cookie.Cookie.SameSite = AspNetCore.Http.SameSiteMode.None;
            });

            return builder;
        }
    }
}
