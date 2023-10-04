This code includes a dependency on Duende IdentityServer.
This is an open source product with a reciprocal license agreement. If you plan to use Duende IdentityServer in production this may require a license fee.
To see how to use Azure Active Directory for your identity please see https://aka.ms/aspnetidentityserver
To see if you require a commercial license for Duende IdentityServer please see https://aka.ms/identityserverlicense



Add-Migration ScriptC -context FlatDbContext
Add-Migration ScriptD -context ApplicationDbContext
update-database -context ApplicationDbContext
update-database -context FlatDbContext

Ab12@gmail.com