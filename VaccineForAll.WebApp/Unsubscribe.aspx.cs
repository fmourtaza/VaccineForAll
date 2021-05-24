using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VaccineForAll.Libraries;

namespace VaccineForAll.WebApp
{
    public partial class Unsubscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            int returnValue = OperationsCRUD.Unsubscribe(email.Value);
            ShowSuccessMessage();
        }

        private void ShowSuccessMessage()
        {
            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "ShowMessage();", true);
        }
    }
}