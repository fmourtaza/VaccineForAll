using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VaccineForAll.Libraries;

namespace VaccineForAll.WebApp
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            GridView1.Font.Size = FontUnit.XSmall;
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            if (GridView1 != null && GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //Reset control value if any
            ResetControlValues();

            String citizendistrictID = selectedDistrictID.Value;
            String citizendistrictName = selectedDistrictName.Value;
            int citizenAge = Convert.ToInt32(selectedAge.Value);
            String citizenDoseChoice = selectedDose.Value;
            VaccineSlot vaccineSlot = new VaccineSlot();
            DataTable appoitmentData = vaccineSlot.ReportViewer(citizendistrictID, citizendistrictName, citizenAge, citizenDoseChoice);
            if (appoitmentData != null && appoitmentData.Rows.Count > 0)
            {
                //Sort data
                DataView dataView = appoitmentData.DefaultView;
                dataView.Sort = "session_date asc";
                appoitmentData = dataView.ToTable();
                //Bind data
                GridView1.DataSource = appoitmentData;
                GridView1.DataBind();
                //Sorting variables
                ViewState["dirState"] = appoitmentData;
                ViewState["sortdr"] = "Asc";
                // Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                ShowSuccessMessage(citizendistrictName, citizenAge, citizenDoseChoice);
            }
            else
                ShowNoDataMessage(citizendistrictName, citizenAge, citizenDoseChoice);
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
            Utilities.SendWelcomeMail(citizenEmail, Credentials.MailSubject);
            ShowSuccessMessageOnSubmit(); ;
        }

        private void ShowSuccessMessage(String citizendistrictName, int citizenAge, String citizenDoseChoice)
        {
            var istDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string message = string.Format("Report generated successfully for [District: {0} - Age: {1} - Dose: {2}] at timestamp: {3}.", citizendistrictName, citizenAge, citizenDoseChoice, istDateTime.ToString("F"));
            lblMessage.Text = message;
            ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "Alert", "ShowSearchHideLabel();", true);
            ShowNotification(message, UpdatePanel3);
        }

        private void ShowSuccessMessageOnSubmit()
        {
            string message = string.Format("Form has been submitted successfully. You shall get a Welcome Mail soon !");
            lblMessage.Text = message;
            ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "Alert", "ShowMessageOnSubmit();", true);
            ShowNotification(message, UpdatePanel3);
        }

        private void ShowNoDataMessage(String citizendistrictName, int citizenAge, String citizenDoseChoice)
        {
            var istDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string message = string.Format("No data to show for [District: {0} - Age: {1} - Dose: {2}] at timestamp: {3}.", citizendistrictName, citizenAge, citizenDoseChoice, istDateTime.ToString("F"));
            lblMessage.Text = message;
            ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "Alert", "HideLabel();", true);
            ShowNotification(message, UpdatePanel3);
        }

        private void ShowNotification(string message, UpdatePanel updatePanelControl)
        {
            ScriptManager.RegisterStartupScript(updatePanelControl, updatePanelControl.GetType(), "Alert100", " alert('" + message + "')", true);
        }

        private void ResetControlValues()
        {
            lblMessage.Text = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    //dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    //ViewState["sortdr"] = "Desc";

                    DataView dataView = dtrslt.DefaultView;
                    dataView.Sort = e.SortExpression + " Desc";
                    dtrslt = dataView.ToTable();
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    //dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    //ViewState["sortdr"] = "Asc";

                    DataView dataView = dtrslt.DefaultView;
                    dataView.Sort = e.SortExpression + " Asc";
                    dtrslt = dataView.ToTable();
                    ViewState["sortdr"] = "Asc";
                }

                //GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                ////Attribute to hide column in Phone.
                //GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                ////Adds THEAD and TBODY to GridView.
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                GridView1.DataSource = dtrslt;
                GridView1.DataBind();
            }
        }

    }
}