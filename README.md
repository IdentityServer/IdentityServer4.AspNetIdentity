# IdentityServer4.AspNetIdentity

ASP.NET Core Identity integration support for IdentityServer4.

Add the following package to your project.json

```json
"IdentityServer4.AspNetIdentity": "1.0.0-rc1"
```

This repos contains extensions for IdentityServer to easily integate with ASP.NET Core Identity. You simply add the `UseAspNetIdentity`method when configuring IdentityServer:

```csharp
// Adds IdentityServer
services.AddIdentityServerQuickstart()
    .AddInMemoryScopes(Config.GetScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddAspNetIdentity<ApplicationUser>();
```
