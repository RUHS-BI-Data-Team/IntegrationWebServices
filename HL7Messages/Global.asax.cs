using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HL7Messages
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string[] partsOfUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\".ToCharArray());
            string domainName = partsOfUserName[0];
            string userName = partsOfUserName[1];
            Session["CurrentUser"] = userName;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exc = Server.GetLastError();


            // For other kinds of errors give the user some information
            // but stay on the default page


            //Response.Write("<h2>Global Page Error</h2>\n");
            //Response.Write(
            //    "<p>" + exc.Message + "</p>\n");
            //// Clear the error from the server
            //Server.ClearError();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_AcquireRequestState(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.Handler is IRequiresSessionState)
            {
                System.Security.Principal.IPrincipal principal = default(System.Security.Principal.IPrincipal);
                principal = (System.Security.Principal.IPrincipal)Session["Intgn.Libraries.Security.Principal"];
                if (principal == null)
                {
                    if (User.Identity.IsAuthenticated && User.Identity is System.Web.Security.FormsIdentity)
                    {
                        // We should only get here when the session expires after
                        // we have logged in (have a valid FormsIdentity)
                        FormsAuthentication.SignOut();
                        Response.Redirect(Request.Url.PathAndQuery);
                    }
                    // didn't get a principal from Session, so
                    // set it to the unathenticated FWRPrincipal
                    Intgn.Libraries.Security.Principal.Logout();
                }
                else
                {
                    // use the principal from session
                    HttpContext.Current.User = principal;
                }

            }
        }
    }
}