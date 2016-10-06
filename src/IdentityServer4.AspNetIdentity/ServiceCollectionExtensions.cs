// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerAspNetIdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServerUserClaimsPrincipalFactory<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
        {
            return services.AddTransient<IUserClaimsPrincipalFactory<TUser>, UserClaimsFactory<TUser, TRole>>();
        }
    }
}
