# Running two factor code on IdentityServer4.AspNetIdentity
Ensure that not existing database of that name exists as we need to have the email address for the QR Code as it is normally used. Not sure why the email is required, but Google specs the code to look like: AuthenticatorUriFormat  = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6" is in the ManageController.cs file and called like this string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode("TestQR"),  <-- make this something you are
                _urlEncoder.Encode(email),     <-- this is why email is added
                unformattedKey);               <-- this is a random number (key)

You need to start the host with the /seed argument one time, for example: …\identityserver4.aspnetidentity\src\Host> dotnet run /seed
The javascript qrcode.js must be installed in the js directory under wwwroot from https://davidshimjs.github.io/qrcodejs/
The SiteScript.js contains the script to call makecode() in the qrcode.js.
Start Host in visual studio with IIS, or wherever it is convenient at other times.
Navigate to http://localhost:5000/Account/login
Login as alice or bob as instructed
Click on dropdown by the username {Alice or Bob Smith} in the page header
Select “Manage (2FA)”
Select “Add Authentication App”
