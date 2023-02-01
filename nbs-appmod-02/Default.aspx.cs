using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nbs_appmod_02
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Context.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                {
                    RedirectUri = "ReportViewer.aspx",
                }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }

        }
    }
}