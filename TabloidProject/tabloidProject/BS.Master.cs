
using System;
using System.Web.UI;
using Tabloid.Classes.Config;
using Tabloid.Classes.Data;
using Tabloid.Classes.Tools;

namespace tabloidProject
{
    public partial class BSMaster : MasterPage
    {
        private AntiXsrf _antiXsrf;

        protected void Page_Init(object sender, EventArgs e)
        {
            _antiXsrf = new AntiXsrf();
            _antiXsrf.Set(ViewState);

            var popupMode = Page.Request["mode"] == "popup";
            
            //change css in popup mode
            if (popupMode)
            {
                master_body.ID = "bodyPopup";
                masterPage.CssClass = "pagePopup";
                pnlMasterMain.CssClass = "mainPopup";
                return;
            }

            if (TabloidConfig.Config != null)
                if (Session["Verrou"] != null ||
                       !TabloidConfig.Config.TabloidConfigApp.UseLockDatabaseField)
                    Verrou.SupVerrou();//remove resiliant lock for this session
        }

        //protected void master_Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                
        //        ViewState[AntiXsrfUserNameKey] =
        //               Context.User.Identity.Name ?? String.Empty;
        //    }
        //    else
        //    {
        //        if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
        //            || (string)ViewState[AntiXsrfUserNameKey] !=
        //                 (Context.User.Identity.Name ?? String.Empty))
        //        {
        //            throw new InvalidOperationException("Validation of " +
        //                                "Anti-XSRF token failed.");
        //        }
        //    }
        //}

    }
}