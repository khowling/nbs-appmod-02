using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Configuration;

[assembly: OwinStartup(typeof(nbs_appmod_02.Startup1))]

namespace nbs_appmod_02
{
    public class Startup1
    {
        private static string clientId = ConfigurationManager.AppSettings["AzureActiveDirectoryClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["AzureActiveDirectoryInstance"];
        private static string tenant = ConfigurationManager.AppSettings["AzureActiveDirectoryTenant"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutRedirectUri"];

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType); app.UseCookieAuthentication(new CookieAuthenticationOptions()); app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant),
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    RedirectUri = postLogoutRedirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = context =>
                        {
                            context.HandleResponse();
                            Console.WriteLine(context.Exception.Message);
                            return Task.FromResult(0);
                        }
                    }
                });

        }
    }
}
