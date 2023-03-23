using System;
using System.Web;
using Tabloid.Classes.Tools;
using System.Web.SessionState;
using Tabloid.Classes.Objects;

namespace tabloidProject
{
    public class Global : HttpApplication
    {
        protected void Application_BeginRequest()
        {

        }

        protected void Application_PostAuthorizeRequest()
        {
            if (Tabloid.Classes.Tools.OData.Tools.IsOdataRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }


        private void Application_Start(object sender, EventArgs e)
        {
            Tabloid.Classes.Tools.Application.Start();
        }

        private void Application_End(object sender, EventArgs e)
        {
            //  Code qui s'exécute à l'arrêt de l'application
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Common.WriteLog("Application_Error");
            Exception ex = Server.GetLastError();
            Common.WriteLog(ex.ToString());
        }

        private void Session_Start(object sender, EventArgs e)
        {

            var url = Sessions.Start(Request, HttpContext.Current);
            if (!string.IsNullOrEmpty(url)) HttpContext.Current.Response.Redirect(url);
        }

        //private static void SessionTimoutModule_SessionEnd(object sender, SessionEndedEventArgs e)
        //{
        //   object sessionObject = e.SessionObject;

        //    string val = (sessionObject == null) ? "[null]" : sessionObject.ToString();

        //}
        //http://mvolo.com/iis-70-twolevel-authentication-with-forms-authentication-and-windows-authentication/
        private void Session_End(object sender, EventArgs e)
        {
            Common.WriteLog($"Fin de session");
            var context = UserContext.loadUserContext(null, null, this.Session);

            if (context == null) Common.WriteLog($"Contexte null...");
            else
            if (context.CurrentUser == null) Common.WriteLog($"CurrentUser null...");
            else
            {
                Common.delTempRecord(context);
                Sessions.Stop(context.CurrentUser.Id, Session["verrou"], Session.SessionID, context.CurrentUser.FullUserName, context.CurrentUser.lastChatId);
            }
        }
    }
}