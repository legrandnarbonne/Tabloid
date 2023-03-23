using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tabloid.Classes.Config;
using Tabloid.Classes.Data;

namespace tabloidProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var popupMode = Page.Request["mode"] == "popup";

            //change css in popup mode
            if (popupMode)
            {
                master_body.ID = "bodyPopup";
                masterPage.CssClass = "pagePopup";
                pnlMasterMain.CssClass = "mainPopup";
                return;
            }

            //add menu

            if (TabloidConfigMenu.ConfigMenu != null)
            {
                var mn = (Menu)TabloidConfigMenu.ConfigMenu.TopMenu;
                mn.EnableViewState = false;
                mn.ViewStateMode = ViewStateMode.Disabled;
                var level1Style = new MenuItemStyle { CssClass = "level1" };
                mn.LevelMenuItemStyles.Add(level1Style);

                if (Page.Master != null) Page.Master.FindControl("MenuHolder").Controls.Add(mn);
            }
            else
            {
                var l = new Label { Text = TabloidConfigMenu.LastError };
                if (Page.Master != null) Page.Master.FindControl("MenuHolder").Controls.Add(l);
            }

            if (TabloidConfig.Config != null)
                if (Session["Verrou"] != null ||
                       !TabloidConfig.Config.TabloidConfigApp.UseLockDatabaseField)
                    Verrou.SupVerrou();//remove resiliant lock for this session
        }
    }
}