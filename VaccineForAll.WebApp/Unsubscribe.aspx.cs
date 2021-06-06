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
            if (returnValue == 1)
                ShowSuccessMessage();
            else
                ShowNotSuccessMessage();
        }

        private void ShowSuccessMessage()
        {
            lblMessage.Visible = true;
            lblMessage.Text = "You have been  removed successfully from the mailing list.<br>Thanks for being with us and Stay Safe!";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "ShowMessage();", true);
        }

        private void ShowNotSuccessMessage()
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Oups! We could not find any entry with this email id!";
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "ShowNotMessage();", true);
        }
    }
}