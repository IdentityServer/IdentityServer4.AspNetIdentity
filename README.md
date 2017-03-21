# IdentityServer4.AspNetIdentity

ASP.NET Core Identity integration support for IdentityServer4.

Add the following NuGet package to your application: `IdentityServer4.AspNetIdentity`

This repos contains extensions for IdentityServer to easily integate with ASP.NET Core Identity. You simply add the `UseAspNetIdentity`method when configuring IdentityServer:

```csharp
// Adds IdentityServer
services.AddIdentityServer()
    .AddInMemoryScopes(Config.GetScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddAspNetIdentity<ApplicationUser>();
```

You can find a detailed walk-through for ASP.NET Core Identity integration [here](https://identityserver4.readthedocs.io/en/dev/quickstarts/6_aspnet_identity.html).
