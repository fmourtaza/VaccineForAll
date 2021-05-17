using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VaccineForAll.Libraries;

namespace VaccineForAll.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            String citizenEmail = email.Value;
            String citizendistrictID = selectedDistrictID.Value;
            String citizendistrictName = selectedDistrictName.Value;
            String citizenAge = selectedAge.Value;
            String citizenDoseChoice = selectedDose.Value;
            Citizen citizen = new Citizen(citizenEmail, citizendistrictID, citizendistrictName, citizenAge, citizenDoseChoice);
            int newID = OperationsCRUD.CreateRecord(citizen);
            Utilities.SendWelcomeMail(citizenEmail, "VaccineForAll - Let us get vaccinated!");
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