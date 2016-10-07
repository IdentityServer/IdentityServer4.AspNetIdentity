// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerIdentityBuilderExtensions
    {
        public static IdentityBuilder AddIdentityServerUserClaimsPrincipalFactory(this IdentityBuilder builder)
        {
            var interfaceType = typeof(IUserClaimsPrincipalFactory<>);
            interfaceType = interfaceType.MakeGenericType(builder.UserType);

            var classType = typeof(UserClaimsFactory<,>);
            classType = classType.MakeGenericType(builder.UserType, builder.RoleType);

            builder.Services.AddScoped(interfaceType, classType);

            return builder;
        }
    }
}
