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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            //Reset control value if any
            ResetControlValues();

            String citizendistrictID = selectedDistrictID.Value;
            String citizendistrictName = selectedDistrictName.Value;
            int citizenAge = Convert.ToInt32(selectedAge.Value);
            VaccineSlot vaccineSlot = new VaccineSlot();
            DataTable appoitmentData = vaccineSlot.ReportViewer(citizendistrictID, citizendistrictName, citizenAge);
            if (appoitmentData != null && appoitmentData.Rows.Count > 0)
            {
                //Sort data
                DataView dataView = appoitmentData.DefaultView;
                dataView.Sort = "session_date asc";
                appoitmentData = dataView.ToTable();
                //Bind data
                GridView1.DataSource = appoitmentData;
                GridView1.DataBind();
                // Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                ShowSuccessMessage(citizendistrictName, citizenAge);
            }
            else
                ShowNoDataMessage(citizendistrictName, citizenAge);
        }

        private void ShowSuccessMessage(String citizendistrictName, int citizenAge)
        {
            lblMessage.Text = string.Format("Report generated successfully for [District: {0} & Age: {1}] - at timestamp: {2}.", citizendistrictName, citizenAge, DateTime.Now);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        private void ShowNoDataMessage(String citizendistrictName, int citizenAge)
        {
            lblMessage.Text = string.Format("No data to show for [District: {0} & Age: {1}] - at timestamp: {2}.", citizendistrictName, citizenAge, DateTime.Now);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        private void ResetControlValues()
        {
            lblMessage.Text = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
}